using Core.Entities;
namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecifications<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParam)
        : base ( x=>
               (!productParam.BrandId.HasValue || x.ProductBrandID == productParam.BrandId) &&
               (!productParam.TypeId.HasValue || x.ProductTypeID == productParam.TypeId)
        )
        {
            AddInclude(x  => x.ProductType);
            AddInclude( x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParam.PageSize * (productParam.PageIdex - 1),productParam.PageSize);
            if(!string.IsNullOrEmpty(productParam.sort))
            {
                switch(productParam.sort)
                {
                   case "priceAsc":
                       AddOrderBy(p => p.Price);
                       break;
                   case "priceDesc":
                      AddOrderByDescending(p=> p.Price);
                      break;
                  default:
                    AddOrderBy(n => n.Name);
                    break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.id == id)
        {
            AddInclude(x  => x.ProductType);
            AddInclude( x => x.ProductBrand);
        }
    }
}