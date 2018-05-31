using GolfGear.Clubs.Model;

namespace GolfGear.Clubs
{
    public class ClubQuery : PagedQuery
    {
        public ClubType ClubType { get; set; }
        
        public Flex Flex { get; set; }

        public decimal Loft { get; set; }

        public string Make { get; set; }

        public string ShaftMake { get; set; }
    }
}