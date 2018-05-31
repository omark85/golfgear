using System.Collections.Generic;
using System.Threading.Tasks;
using GolfGear.Clubs.Model;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace GolfGear.Clubs
{
    [Route("api/[controller]")]
    public class ClubsController : Controller
    {
        private IDocumentStore DocumentStore { get; }

        public ClubsController(IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore;
        }

        [HttpGet]
        public async Task<IEnumerable<GolfClub>> Get()
        {
            using (var session = this.DocumentStore.OpenAsyncSession()){
                return await session.Advanced.LoadStartingWithAsync<GolfClub>("GolfClubs");
            }
        }

        [HttpGet("{id}")]
        public Task<GolfClub> Get(string id)
        {
            using (var session = this.DocumentStore.OpenAsyncSession()){
                return session.LoadAsync<GolfClub>($"GolfClubs/{id}");
            }
        }
    }
}
