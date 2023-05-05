using GeekBurguer_Promotion_Service.Contracts.Input;
using GeekBurguer_Promotion_Service.Contracts.Output;

namespace GeekBurguer_Promotion_Service.Promotion.Interface
{
    public interface IPromotionService
    {
        Task<PromotionResponse> GetByStoreName(string StoreName);

        Task<List<PromotionResponse>> GetAll();

        Task Create(PromotionRequest model);

        Task Update(PromotionRequest model);

        Task Delete(Guid Id);
    }
}
