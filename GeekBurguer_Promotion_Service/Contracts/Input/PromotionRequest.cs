using GeekBurguer_Promotion_Service.Contracts.SwaggerExclude;
using System.ComponentModel.DataAnnotations;

namespace GeekBurguer_Promotion_Service.Contracts.Input
{
    public class PromotionRequest
    {
        [SwaggerExclude]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? StoreName { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "a Imagem é obrigatória.")]
        public string? Image { get; set; }

        public string? ProductsId { get; set; }

        [Required(ErrorMessage = "O Preço é obrigatório.")]
        public decimal Price { get; set; }
    }
}
