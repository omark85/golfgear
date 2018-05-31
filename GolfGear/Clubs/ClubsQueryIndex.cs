using System.Linq;
using GolfGear.Clubs.Model;
using Raven.Client.Documents.Indexes;

namespace GolfGear.Clubs
{
    public class ClubsQueryIndex : AbstractIndexCreationTask<GolfClub>
    {
        public ClubsQueryIndex()
        {
            this.Map = clubs => from club in clubs
                                select new Result
                                {
                                    ClubType = club.Type,
                                    Loft = club.Loft,
                                    ShaftFlex = club.Shaft.Flex,
                                    Make = club.Make,
                                    ShaftMake = club.Shaft.Make
                                };
        }
        
        public class Result
        {
            public ClubType ClubType { get; set; }

            public decimal Loft { get; set; }

            public Flex ShaftFlex { get; set; }

            public string Make { get; set; }
            
            public string ShaftMake { get; set; }
        }
    }
}