using GeekBurguer_Promotion_Service.Contracts.Output;

namespace GeekBurguer_Promotion_Service.Product.Interface
{
    public interface IProductService
    {
        ProductResponse GetProductById(int id);
    }
}
