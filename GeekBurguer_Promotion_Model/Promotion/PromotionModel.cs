namespace GeekBurguer_Promotion_Model.Promotion
{
    public class PromotionModel : Base, IAggregateRoot
    {
        public PromotionModel()
        {
        }
        public PromotionModel(Guid id, string storeName, string name, string image, string productsId, decimal price, DateTime criacao, DateTime modificacao, bool desabilitado)
        {
            Id = id;
            StoreName = storeName;
            Name = name;
            Image = image;
            ProductsId = productsId;
            Price = price;
            Criacao = criacao;
            Modificacao = modificacao;
            Desabilitado = desabilitado;
        }

        public string? StoreName { get; set; }

        public string? Name { get; set; }

        public string? Image { get; set; }

        public string? ProductsId { get; set; }

        public decimal Price { get; set; }
    }
}