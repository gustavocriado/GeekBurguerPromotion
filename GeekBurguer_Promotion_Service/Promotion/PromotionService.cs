using AutoMapper;
using GeekBurguer_Promotion_Infrastructure.Queue.Interface;
using GeekBurguer_Promotion_Infrastructure.Repository.Promotion.Interface;
using GeekBurguer_Promotion_Model.Promotion;
using GeekBurguer_Promotion_Service.Contracts.Enums;
using GeekBurguer_Promotion_Service.Contracts.Input;
using GeekBurguer_Promotion_Service.Contracts.Output;
using GeekBurguer_Promotion_Service.Product.Interface;
using GeekBurguer_Promotion_Service.Promotion.Interface;
using GeekBurguer_Promotion_Service.Promotion.Utils;

namespace GeekBurguer_Promotion_Service.Promotion
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productServe;
        private readonly IServiceBus _serviceBus;

        public PromotionService
        (
            IPromotionRepository promotionRepository,
            IMapper mapper,
            IProductService productServe,
            IServiceBus serviceBus
        )
        {
            _promotionRepository = promotionRepository;
            _productServe = productServe;
            _serviceBus = serviceBus;
            _mapper = mapper;
        }

        public async Task<PromotionResponse> GetByStoreName(string StoreName)
        {
            var response = _mapper.Map<PromotionResponse>(await _promotionRepository.FindByStoreName(StoreName));

            if (response == null)
                return null;

            var products = Util.ConverterStringToListInt(response.ProductsId);

            foreach (var product in products)
                response.Products.Add(_productServe.GetProductById(product));

            return response;
        }

        public async Task Create(PromotionRequest model)
        {
            var response = _mapper.Map<PromotionModel>(model);
            response = FixCreate(response);
            await _promotionRepository.Add(response);

            var promotion = _mapper.Map<PromotionResponse>(response);
            promotion.PromotionState = PromotionStateEnum.Added;

            var products = Util.ConverterStringToListInt(response.ProductsId);
            foreach (var product in products)
                promotion.Products.Add(_productServe.GetProductById(product));

            await _serviceBus.SendAsync(promotion, "geekburguerpromotion");
        }

        public async Task<List<PromotionResponse>> GetAll()
        {
            var response = _mapper.Map<List<PromotionResponse>>(await _promotionRepository.FindAll());

            if (response == null)
                return null;

            foreach (var promotion in response)
            {
                var products = Util.ConverterStringToListInt(promotion.ProductsId);
                foreach (var product in products)
                    promotion.Products.Add(_productServe.GetProductById(product));
            }
            return response;
        }

        public async Task Update(PromotionRequest model)
        {
            var response = _mapper.Map<PromotionModel>(model);
            response = FixUpdate(response);
            _promotionRepository.Update(response);

            var promotion = _mapper.Map<PromotionResponse>(response);
            promotion.PromotionState = PromotionStateEnum.Modified;

            var products = Util.ConverterStringToListInt(response.ProductsId);
            foreach (var product in products)
                promotion.Products.Add(_productServe.GetProductById(product));

            await _serviceBus.SendAsync(promotion, "geekburguerpromotion");
        }

        public async Task Delete(Guid Id)
        {
            var response = await _promotionRepository.FindByID(Id);

            var promotion = _mapper.Map<PromotionResponse>(response);
            promotion.PromotionState = PromotionStateEnum.Deleted;

            await _serviceBus.SendAsync(promotion, "geekburguerpromotion");

            response.Desabilitado = true;
            _promotionRepository.Update(response);
        }

        private PromotionModel FixCreate(PromotionModel promotionModel)
        {
            promotionModel.Criacao = DateTime.UtcNow;
            promotionModel.Modificacao = DateTime.UtcNow;
            promotionModel.Desabilitado = false;
            promotionModel.Justificativa = null;

            return promotionModel;
        }

        private PromotionModel FixUpdate(PromotionModel promotionModel)
        {
            promotionModel.Modificacao = DateTime.UtcNow;
            return promotionModel;
        }
    }
}
