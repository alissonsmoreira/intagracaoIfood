using chart_integracao_ifood_infrastructure.Constants;
using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace chart_integracao_ifood_dal.Repositories
{
    public class BaseRepository
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
        public BaseRepository(IDbContextFactory<AppDbContext> dbContextFactory, IHealthLogService healthLogService)
        {
            _dbContextFactory = dbContextFactory;

            if (!_dbContextFactory.CreateDbContext().Database.CanConnect())
            {
                healthLogService.AddHealthLog(HealthLogCodes.DATABASE_CONNECTION_ERROR);
            }
            else
            {
                healthLogService.RemoveHealthLog(HealthLogCodes.DATABASE_CONNECTION_ERROR);
            }
        }

        protected AppDbContext GetDbContext()
        {
            return _dbContextFactory.CreateDbContext();
        }
    }
}
