using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RolledBackProject.Context;
using RolledBackProject.Models;

namespace RolledBackProject.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly RolledBackContext _db;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, RolledBackContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet(Name = "RolleBackTransaction")]
        public async Task RolleBackTransaction()
        {
            using(var tran = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    await _db.People.AddAsync(new Models.Person()
                    {
                        String = "ali1"
                    });
                    await _db.SaveChangesAsync();

                    var query = await _db.People.SingleAsync(x => x.String == "ali1");
                    await _db.People.AddAsync(new Models.Person()
                    {
                        String = "ali2"
                    });
                    await _db.SaveChangesAsync();
                    throw new Exception();
                    await _db.People.AddAsync(new Models.Person()
                    {
                        String = "ali3"
                    });
                    await _db.SaveChangesAsync();
                    await tran.CommitAsync();

                }
                catch (Exception e )
                {
                    await tran.RollbackAsync();
                    throw e;
                }

            }

               


        }


        [HttpGet(Name = "ChangeTracker")]
        public async Task ChangeTracker()
        {
            await _db.People.AddAsync(new Models.Person()
            {
                String = "ali4"
            });
            await _db.People.AddAsync(new Models.Person()
            {
                String = "ali5"
            });

            var getPeronsFromChangeTracker =
                _db.ChangeTracker.Entries().Where(x => x.Entity is Person && x.State is EntityState.Added);

            var t =(Person) getPeronsFromChangeTracker.First().Entity;
        }




    }
}