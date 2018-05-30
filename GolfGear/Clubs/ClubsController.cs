using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace GolfGear.Controllers
{
    [Route("api/[controller]")]
    public class ClubsController : Controller
    {
        private IDocumentStore DocumentStore { get; }

        public ClubsController(IDocumentStore documentStore)
        {
            this.DocumentStore = documentStore;
        }

        // GET api/values
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
