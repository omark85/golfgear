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

            using (var session = this.DocumentStore.OpenSession()){
                if (!session.Advanced.LoadStartingWith<GolfClub>("GolfClubs").Any()){
                    var callawayRogue = new GolfClub{
                        Type = ClubType.Driver,
                        Make = "Callaway",
                        Loft = 10.5m,
                        Shaft = new Shaft {
                            Type = ShaftType.Graphite,
                            Make = "Project X",
                            Model = "HZRDUS Yellow",
                            Flex = Flex.Stiff,
                            Weight = 65
                        }
                    };

                    session.Store(callawayRogue);
                    session.SaveChanges();
                }
            }
        }

        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<GolfClub>> Get()
        {
            using (var session = this.DocumentStore.OpenAsyncSession()){
                return await session.Advanced.LoadStartingWithAsync<GolfClub>("GolfClubs");
            }
        }
    }
}
