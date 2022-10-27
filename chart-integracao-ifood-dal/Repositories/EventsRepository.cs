using chart_integracao_ifood_dal.Repositories;
using chart_integracao_ifood_infrastructure.Entities;
using chart_integracao_ifood_infrastructure.Models.Common;
using chart_integracao_ifood_infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace chart_integracao_ifood_infrastructure.Repositories
{
    public class EventsRepository : BaseRepository, IEventsRepository
    {
        public EventsRepository(IDbContextFactory<AppDbContext> dbContextFactory, IHealthLogService healthLogService) : base(dbContextFactory, healthLogService)
        {
        }

        public Result<Events> Get(string id)
        {
            try
            {
                var dbContext = GetDbContext();

                var item = dbContext.Events.FirstOrDefault(x => x.Id.Equals(id));

                if (item == null)
                    return Result<Events>.Erro("Evento não encontrado no banco de dados");

                return Result<Events>.Ok(item);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<Events>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }
        public Result<IEnumerable<Events>> GetUnknownEvents()
        {
            try
            {
                var dbContext = GetDbContext();

                var events = dbContext.Events.Where(x => x.Acknowledged == false).Where(x => x.Processed == false);

                if (events == null || !events.Any())
                    return Result<IEnumerable<Events>>.Erro("Não há eventros desconhecidos");

                return Result<IEnumerable<Events>>.Ok(events.ToList());
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<IEnumerable<Events>>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }

        public Result<IEnumerable<Events>> GetProcessedEvents()
        {
            try
            {
                var dbContext = GetDbContext();

                var events = dbContext.Events.Where(x => x.Acknowledged == true).Where(x => x.Processed == true).Where(x => x.Sended == false);

                if (events == null || !events.Any())
                    return Result<IEnumerable<Events>>.Erro("Não há processados");

                return Result<IEnumerable<Events>>.Ok(events.ToList());
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<IEnumerable<Events>>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }

        public Result UpdateEvents(IEnumerable<Events> events)
        {
            var dbContext = GetDbContext();

            foreach (Events evento in events)
            {
                dbContext.Update(evento);
            }

            try
            {
                dbContext.SaveChanges();
                return Result.Ok();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<Events>.Erro("Falha ao atualizar evento no banco de dados");
            }
            catch (Exception e)
            {
                return Result<Events>.Erro(e.Message);
            }
        }
        public Result<IEnumerable<Events>> GetUnprocessedEvents()
        {
            try
            {
                var dbContext = GetDbContext();

                var events = dbContext.Events.Where(x => x.Acknowledged == true).Where(x => x.Processed == false);

                if (events == null || !events.Any())
                    return Result<IEnumerable<Events>>.Erro("Não há eventos desconhecidos");

                return Result<IEnumerable<Events>>.Ok(events.ToList());
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<IEnumerable<Events>>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }

        public Result<IEnumerable<Events>> GetEventsById(string[] eventsIds)
        {
            var dbContext = GetDbContext();
            try
            {
                var eventos = dbContext.Events.Where(x => eventsIds.Contains(x.Id)).ToList();
              
                if (eventos == null || !eventos.Any())
                    return Result<IEnumerable<Events>>.Erro("Não há eventos desconhecidos");

                return Result<IEnumerable<Events>>.Ok(eventos.ToList());
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<IEnumerable<Events>>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }
        public Result<Events> Save(Events events)
        {
            var dbContext = GetDbContext();
            
            dbContext.Events.Add(events);

            try
            {
                int resultCount = dbContext.SaveChanges();

                if (resultCount == 0)
                    return Result<Events>.Erro("Falha ao salvar evento no banco de dados");

                return Result<Events>.Ok(events);
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<Events>.Erro("Falha ao salvar evento no banco de dados");
            }
            catch (Exception e)
            {
                return Result<Events>.Erro(e.Message);
            }
            
        }

        public Result UpdateUnprocessedEvent(string eventId)
        {
                var dbContext = GetDbContext();
            
                var evente = dbContext.Events.Where(x => x.Id == eventId).Where(x => x.Processed == false).FirstOrDefault();

                if (evente != null)
                {
                    evente.Processed = true;
                }
                try
                {
                    dbContext.Update(evente);
                    dbContext.SaveChanges();
                    return Result.Ok();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    return Result<Events>.Erro("Falha ao atualizar evento no banco de dados");
                }
                catch (Exception e)
                {
                    return Result.Erro(e.Message);
                }
            
        }

        public Result<IEnumerable<Events>> GetEventsOlderThan(int days)
        {
            try
            {
                var dbContext = GetDbContext();
                
                DateTime today = DateTime.Now;
                IEnumerable<Events> events = dbContext.Events.Where(x => x.CreatedAt <= today.AddDays(-days));

                if (events == null || !events.Any())
                    return Result<IEnumerable<Events>>.Erro("Não há eventros na data solicitadas");

                return Result<IEnumerable<Events>>.Ok(events);
               
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                return Result<IEnumerable<Events>>.Erro("Falha ao consultar eventos no banco de dados");
            }
        }

        public Result PurgeEvents(IEnumerable<Events> events)
        {
            var dbContext = GetDbContext();
            
            if (events != null || events.Any())
            {
                foreach (Events evento in events)
                {
                    dbContext.Remove(evento);
                }
                try
                {
                    dbContext.SaveChanges();
                    return Result.Ok();
                }
                catch (Microsoft.Data.SqlClient.SqlException ex)
                {
                    return Result.Erro("Falha ao remover eventos no banco de dados");
                }
                catch (Exception e)
                {
                    return Result.Erro(e.Message);
                }
            }
            return Result.Ok();
            
        }
    }
}
