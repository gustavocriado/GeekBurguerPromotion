using Newtonsoft.Json;

namespace GeekBurguer_Promotion_Model.Promotion
{
    public class Base
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; protected set; }
        public DateTime Criacao { get; set; }
        public DateTime? Modificacao { get; set; }
        public bool Desabilitado { get; set; }
        public string? Justificativa { get; set; }
    }
}
