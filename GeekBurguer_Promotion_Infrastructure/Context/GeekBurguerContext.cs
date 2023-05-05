using GeekBurguer_Promotion_Model.Promotion;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer_Promotion_Infrastructure.Context
{
    public class GeekBurguerContext : DbContext
    {
        public GeekBurguerContext(DbContextOptions<GeekBurguerContext> options) : base(options)
        {
            Database.SetCommandTimeout(TimeSpan.FromSeconds(60));
        }

        public DbSet<PromotionModel> Promotions { get; set; }
    }
}
