using Infrastructure.Data;

namespace NetArchTest
{
    public class Sample
    {
        private readonly AppDbContext _context;

        public Sample(AppDbContext context)
        {
            _context = context;
        }
    }
}
