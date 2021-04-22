using LeaveBook.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveBook.Repositories
{
    public class SharedRepository : ISharedRepository
    {
        private readonly ApplicationDbContext _ctx;
        private readonly ILogger<SharedRepository> _logger;
        public SharedRepository(ILogger<SharedRepository> logger, ApplicationDbContext context)
        {
            _ctx = context;
            _logger = logger;
        }
        public async void Add<T>(T entity) where T : class
        {
            try
            {
                await _ctx.AddAsync(entity);
                _logger.LogInformation("Entity saved to db...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            try
            {
                _ctx.Remove(entity);
                _logger.LogInformation("Entity deleted from db...");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<bool> SaveAll()
        {
            try
            {
                return await _ctx.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
