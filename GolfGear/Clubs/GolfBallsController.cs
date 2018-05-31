using System.Collections.Generic;
using System.Threading.Tasks;
using GolfGear.Clubs.Model;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace GolfGear.Clubs
{
    [Route("api/[controller]")]
    public class GolfBallsController : Controller
    {
        private readonly IDocumentStore documentStore;

        public GolfBallsController(IDocumentStore documentStore)
        {
            this.documentStore = documentStore;
        }
        
        [HttpGet]
        public Task<IEnumerable<GolfBall>> GetAll()
        {
            using (var session = this.documentStore.OpenAsyncSession())
            {
                return session.Advanced.LoadStartingWithAsync<GolfBall>("GolfBalls");
            }
        }

        [HttpGet("{id}")]
        public Task<GolfBall> Get([FromRoute] string id)
        {
            using (var session = this.documentStore.OpenAsyncSession())
            {
                return session.LoadAsync<GolfBall>($"GolfBalls/{id}");
            }
        }

        [HttpPost]
        public async Task<GolfBall> Create([FromBody] GolfBall golfball)
        {
            using (var session = this.documentStore.OpenAsyncSession())
            {
                await session.StoreAsync(golfball);

                await session.SaveChangesAsync();

                return golfball;
            }
        }

        [HttpPut("{id}")]
        public async Task<GolfBall> Update([FromRoute] string id, [FromBody] GolfBall golfBall)
        {
            using (var session = this.documentStore.OpenAsyncSession())
            {
                var fromDb = await session.LoadAsync<GolfBall>($"GolfBalls/{id}");

                fromDb.Make = golfBall.Make;
                fromDb.Model = golfBall.Model;

                await session.SaveChangesAsync();

                return fromDb;
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] string id)
        {
            using (var session = this.documentStore.OpenAsyncSession())
            {
                session.Delete($"GolfBalls/{id}");
                
                await session.SaveChangesAsync();
            }
        }
    }
}