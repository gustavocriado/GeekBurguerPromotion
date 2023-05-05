using GeekBurguer_Promotion_Infrastructure.Context;
using GeekBurguer_Promotion_Infrastructure.Repository.Default;
using GeekBurguer_Promotion_Infrastructure.Repository.Promotion.Interface;
using GeekBurguer_Promotion_Model.Promotion;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer_Promotion_Infrastructure.Repository.Promotion
{
    public class PromotionRepository : BaseRepository<PromotionModel>, IPromotionRepository
    {
        public PromotionRepository(GeekBurguerContext db) : base(db)
        {

        }


        public async Task<List<PromotionModel>> FindAll() => await _db.Promotions.Where(x => !x.Desabilitado).ToListAsync();

        public async Task<PromotionModel> FindByStoreName(string storeName) => await _db.Promotions.Where(x => !x.Desabilitado && x.StoreName.Equals(storeName)).FirstOrDefaultAsync();

        public async Task<PromotionModel> FindByID(Guid id) => await _db.Promotions.Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
