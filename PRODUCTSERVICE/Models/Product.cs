using System.ComponentModel.DataAnnotations.Schema;

namespace PRODUCTSERVICE.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string Description { get; set; }= string.Empty;
        public string Image { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price {  get; set; }
    }
}
