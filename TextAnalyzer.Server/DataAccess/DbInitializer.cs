namespace TextAnalyzer.Server.DataAccess
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
