using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecifications<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productParams)  : base(x => 
            (!productParams.BrandId.HasValue || x.ProductBrandID == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeID == productParams.TypeId))
        {
            
        }
        
    }
}