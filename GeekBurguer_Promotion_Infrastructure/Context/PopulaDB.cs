using GeekBurguer_Promotion_Model.Promotion;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeekBurguer_Promotion_Infrastructure.Context
{
    public class PopulaDB
    {
        public static void IncluiDadosDB(IApplicationBuilder app)
        {
            IncluiDadosDB(app.ApplicationServices.GetRequiredService<GeekBurguerContext>());
        }
        public static void IncluiDadosDB(GeekBurguerContext context)
        {
            context.Database.Migrate();
            if (!context.Promotions.Any())
            {
                context.Promotions.AddRange(
                    new PromotionModel(Guid.NewGuid(), "Paulista", "Darth Bacon", "img_db.jpg", "1111", Convert.ToDecimal("10.20"), DateTime.UtcNow.AddHours(-3), DateTime.UtcNow.AddHours(-3), false),
                     new PromotionModel(Guid.NewGuid(), "Morumbi", "Darth Cheese", "img1.png", "1112", Convert.ToDecimal("52.40"), DateTime.UtcNow.AddHours(-3), DateTime.UtcNow.AddHours(-3), false));

                context.SaveChanges();
            }
        }
    }
}
