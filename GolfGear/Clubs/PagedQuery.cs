namespace GolfGear.Clubs
{
    public abstract class PagedQuery
    {
        public int PageSize { get; set; } = 50;

        public int Skip { get; set; } = 0;
    }
}