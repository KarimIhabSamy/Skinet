
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using System.Collections.Generic;
using API.Errors;
using API.Helpers;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        public ProductsController(
          IGenericRepository<Product> productsRepo,
          IGenericRepository<ProductBrand> productBrandRepo,
          IGenericRepository<ProductType> productTypeRepo
          ,     IMapper mapper)     
        {
               _productsRepo = productsRepo;
               _productTypeRepo = productTypeRepo;
               _productBrandRepo = productBrandRepo;
               _mapper=mapper;
               
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
         [FromQuery] ProductSpecParams productParams )
        {
            //return "this will return list of products";
            //var products = await _repo.GetProductAsync();
            var spec = new ProductsWithTypesAndBrandsSpecification(
           productParams
            );
            var countSpec = new ProductsWithFiltersForCountSpecification(productParams);
            var TotalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper.Map<IReadOnlyList<Product>
            ,IReadOnlyList<ProductToReturnDto>>(products);
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIdex,productParams.PageSize
            ,TotalItems,data));
            // return products.Select(product => new ProductToReturnDto
            // {
            //     id = product.id,
            //     Name=product.Name,
            //     Description=product.Description,
            //     PictureUrl=product.PictureUrl,
            //     Price=product.Price,
            //     ProductBrand=product.ProductBrand.Name,
            //     ProductType=product.ProductType.Name
                
            // }).ToList();
        }
        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse) ,StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int ID)
        {
          var spec = new ProductsWithTypesAndBrandsSpecification(ID);
           // return await _repo.GetProductByIdAsync(ID);
          var product = await _productsRepo.GetEntityWithSpec(spec);
          if(product == null ) 
          return NotFound(new ApiResponse(404));
        return _mapper.Map<Product,ProductToReturnDto>(product);
        //   return new  ProductToReturnDto
        //     {
        //         id = product.id,
        //         Name=product.Name,
        //         Description=product.Description,
        //         PictureUrl=product.PictureUrl,
        //         Price=product.Price,
        //         ProductBrand=product.ProductBrand.Name,
        //         ProductType=product.ProductType.Name
                
        //     };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProudtBrands()
        {
            //return Ok( await _repo.GetProductBrandsAsync());
            return Ok( await _productBrandRepo.ListAllAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProudtTypes()
        {
           // return Ok( await _repo.GetProductTypesAsync());
            return Ok( await _productTypeRepo.ListAllAsync());
        }
    }
}