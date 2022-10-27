using chart_integracao_ifood_infrastructure.Entities.Configuration;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace chart_integracao_ifood_tray
{
    public partial class Form1 : Form
    {
        private const string DEFAULT_OK_MESSAGE = "Status: Ativo";
        private const string SERVICE_NAME = "IntegradorChartFoodService";
        private const int SERVICE_CHECK_TIME = 5000;
        private bool _fechar = false;
        private Configs _configs;
        private ServiceCheck _serviceCheck;

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.BalloonTipText = DEFAULT_OK_MESSAGE;
            this.ShowInTaskbar = false;

            _configs = LoadConfigFile();

            WriteConfigs();

            //Iniciar verificação do serviço
            Task taskServiceVerification = new Task(new Action(StartServiceVerificationTimer));
            taskServiceVerification.Start();
        }

        #region Privates

        private void WriteConfigs()
        {
            txtPorta.Text = _configs.Port.ToString();
            txtConnectionString.Text = _configs.ConnectionString;
            txtIFoodClientId.Text = _configs.IFoodClientId;
            txtIFoodClientSecret.Text = _configs.IFoodClientSecret;
        }

        private Configs LoadConfigFile()
        {
            string filePath = GetFilePath();

            string fileName = GetFileName();

            return JsonConvert.DeserializeObject<Configs>(File.ReadAllText($"{filePath}{fileName}"));
        }

        private void SaveConfigFile()
        {
            var config = new Configs()
            {
                Port = int.Parse(txtPorta.Text),
                ConnectionString = txtConnectionString.Text,
                IFoodClientId = txtIFoodClientId.Text,
                IFoodClientSecret = txtIFoodClientSecret.Text
            };

            string filePath = GetFilePath();

            string fileName = GetFileName();

            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText($"{filePath}{fileName}", json);

            _configs = config;
        }

        private string GetFilePath()
        {
            return $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\ChartIntegracaoIfood";
        }

        private string GetFileName()
        {
            return $"\\config.json";
        }

        private void RestartService(int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(SERVICE_NAME);
            try
            {
                int millisec1 = Environment.TickCount;
                TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);

                // count the rest of the timeout
                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec2 - millisec1));

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
            }
            catch (Exception ex)
            {
            }
        }

        private void StartServiceVerificationTimer()
        {
            VerifyService();
            System.Timers.Timer timerMatching = new System.Timers.Timer(SERVICE_CHECK_TIME);
            timerMatching.Enabled = true;
            timerMatching.Elapsed += new ElapsedEventHandler(StartServiceVerificationTimerElapsed);
            timerMatching.Start();
        }

        private void StartServiceVerificationTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            VerifyService();
        }

        private void VerifyService()
        {
            _serviceCheck = new ServiceCheck(_configs.Port);

            var result = _serviceCheck.GetHealth();

            if (result.Success)
            {
                SetStatusOk();
            }
            else
            {
                SetStatusError(result.Message);
            }
        }

        private void SetStatusOk()
        {
            notifyIcon1.BalloonTipText = DEFAULT_OK_MESSAGE;

            UpdateStatusMenu(DEFAULT_OK_MESSAGE, true);
            UpdateLabelStatus(DEFAULT_OK_MESSAGE, true);
        }

        private void SetStatusError(string error)
        {
            var message = $"Status: {error}";
            notifyIcon1.BalloonTipText = message;

            UpdateStatusMenu(message, false);
            UpdateLabelStatus(message, false);
        }

        private void UpdateLabelStatus(string message, bool success)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke((MethodInvoker)delegate
                {
                    lblStatus.Text = message;
                    lblStatus.ForeColor = success ? Color.Green : Color.Red;
                });
            }
            else
            {
                lblStatus.Text = message;
                lblStatus.ForeColor = success ? Color.Green : Color.Red;
            }
        }

        private void UpdateStatusMenu(string message, bool success)
        {
            if (contextMenuStrip1.InvokeRequired)
            {
                contextMenuStrip1.Invoke((MethodInvoker)delegate
                {
                    var menuItem = contextMenuStrip1.Items.Find(statusToolStripMenuItem.Name, true).FirstOrDefault();

                    if (menuItem != null)
                    {
                        menuItem.Text = message;
                        menuItem.ForeColor = success ? Color.Green : Color.Red;
                    }
                });
            }
            else
            {
                var menuItem = contextMenuStrip1.Items.Find(statusToolStripMenuItem.Name, true).FirstOrDefault();

                if (menuItem != null)
                {
                    menuItem.Text = message;
                    menuItem.ForeColor = success ? Color.Green : Color.Red;
                }
            }
        }
        #endregion

        #region Eventos

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            DoubleClickTrayIcon();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoubleClickTrayIcon();
        }

        private void DoubleClickTrayIcon()
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;

            this.Activate();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fechar = true;
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            e.Cancel = !_fechar;
        }

        private void txtPorta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveConfigFile();
            RestartService(5000);
        }

        #endregion
    }
}
