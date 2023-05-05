using AutoMapper;
using GeekBurguer_Promotion_Model.Promotion;
using GeekBurguer_Promotion_Service.Contracts.Input;
using GeekBurguer_Promotion_Service.Contracts.Output;

namespace GeekBurguer_Promotion_Service.Promotion.AutoMapper
{
    public class PromotionMapper : Profile
    {
        public PromotionMapper() : this("MyProfile")
        {

        }
        public PromotionMapper(string profileName) : base(profileName)
        {
            MapperPromotion();
        }

        public void MapperPromotion()
        {
            CreateMap<PromotionModel, PromotionResponse>();
            CreateMap<PromotionRequest, PromotionModel>();
        }
    }
}
