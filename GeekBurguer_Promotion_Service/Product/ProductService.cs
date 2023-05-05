using GeekBurguer_Promotion_Service.Contracts.Output;
using GeekBurguer_Promotion_Service.Product.Interface;

namespace GeekBurguer_Promotion_Service.Product
{
    public class ProductService : IProductService
    {
        public ProductResponse GetProductById(int id)
        {
            var response = new ProductResponse();
            switch (id)
            {
                case 1:
                    response.Id = 1;
                    response.Name = "Big Mac";
                    break;
                case 2:
                    response.Id = 2;
                    response.Name = "Quarterão";
                    break;
                case 3:
                    response.Id = 3;
                    response.Name = "McChicken";
                    break;
                default:
                    response.Id = 4;
                    response.Name = "Big Mac";
                    break;
            }

            return response;
        }
    }
}
