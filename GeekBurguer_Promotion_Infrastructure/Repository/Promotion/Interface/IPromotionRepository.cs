using GeekBurguer_Promotion_Infrastructure.Repository.Default.Interface;
using GeekBurguer_Promotion_Model.Promotion;

namespace GeekBurguer_Promotion_Infrastructure.Repository.Promotion.Interface
{
    public interface IPromotionRepository : IBaseRepository<PromotionModel>
    {
        Task<PromotionModel> FindByStoreName(string storeName);
        Task<List<PromotionModel>> FindAll();
        Task<PromotionModel> FindByID(Guid id);
    }
}
