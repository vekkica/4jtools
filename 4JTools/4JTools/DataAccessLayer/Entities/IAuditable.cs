namespace _4JTools.DataAccessLayer.Entities
{
    public interface IAuditable
    {
        DateTime Created { get; set; }

        DateTime Modified { get; set; }
    }
}
