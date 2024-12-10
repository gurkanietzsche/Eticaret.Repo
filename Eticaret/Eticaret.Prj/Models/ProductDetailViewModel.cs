using Eticaret.Prj.Entities;

namespace Eticaret.Prj.Models
{
    public class ProductDetailViewModel
    {
        public Product? Product { get; set; }
        public IEnumerable<Product>? RelatedProducts { get; set; }
    }
}
