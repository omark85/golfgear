using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("search")]
        public async Task<IEnumerable<GolfClub>> Query([FromQuery] ClubQuery query)
        {
            using (var session = this.DocumentStore.OpenAsyncSession())
            {
                var q = session.Query<ClubsQueryIndex.Result, ClubsQueryIndex>().AsQueryable();

                if (query.ClubType != ClubType.Unknown)
                {
                    q = q.Where(x => x.ClubType == query.ClubType);
                }

                if (!string.IsNullOrWhiteSpace(query.Make))
                {
                    q = q.Where(x => x.Make == query.Make);
                }

                if (!string.IsNullOrWhiteSpace(query.ShaftMake))
                {
                    q = q.Where(x => x.ShaftMake == query.ShaftMake);
                }

                if (query.Flex != Flex.Unknown)
                {
                    q = q.Where(x => x.ShaftFlex == query.Flex);
                }
                                
                return await q.Skip(query.Skip)
                              .Take(query.PageSize)
                              .OfType<GolfClub>()
                              .ToListAsync();
            }
        }
    }
}
