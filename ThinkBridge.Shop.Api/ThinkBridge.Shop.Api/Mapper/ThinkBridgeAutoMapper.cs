using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Api.ViewModel;
using ThinkBridge.Shop.Api.ViewModel.Catalog;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Api.Mapper
{
    public class ThinkBridgeAutoMapper : Profile
    {
        public ThinkBridgeAutoMapper() 
        {
            CreateCatalogMaps();
        }
        protected virtual void CreateCatalogMaps()
        {
            //products
            CreateMap<Product, ProductModel>()
                  .ForMember(model => model.ProductPictures, options => options.Ignore())
                .ForMember(model => model.ProductCategories, options => options.Ignore())
                .ForMember(model => model.ProductManufacturers, options => options.Ignore());

            CreateMap<ProductModel, Product>()
                .ForMember(entity => entity.CreatedOnUtc, options => options.Ignore())
                .ForMember(entity => entity.Deleted, options => options.Ignore())
                .ForMember(entity => entity.ProductCategories, options => options.Ignore())
                .ForMember(entity => entity.ProductManufacturers, options => options.Ignore())
                .ForMember(entity => entity.ProductPictures, options => options.Ignore())
                .ForMember(entity => entity.UpdatedOnUtc, options => options.Ignore());


            CreateMap<ProductPicture, ProductPictureModel>()
                .ForMember(model => model.PictureUrl, options => options.Ignore());
            CreateMap<ProductPictureModel,ProductPicture >()
              .ForMember(model => model.Picture, options => options.Ignore())
             .ForMember(model => model.Product, options => options.Ignore());

            CreateMap<ProductManufacturer, ProductManufacturerModel>()
               .ForMember(model => model.Name, options => options.Ignore());
            CreateMap<ProductManufacturerModel, ProductManufacturer>()
              .ForMember(model => model.Manufacturer, options => options.Ignore())
             .ForMember(model => model.Product, options => options.Ignore());

            CreateMap<ProductCategory, ProductCategoryModel>()
              .ForMember(model => model.Name, options => options.Ignore());
            CreateMap<ProductCategoryModel, ProductCategory>()
              .ForMember(model => model.Category, options => options.Ignore())
             .ForMember(model => model.Product, options => options.Ignore());

            CreateMap<Category, CategoryModel>().ForMember(x => x.PictureUrl, y => y.Ignore());
            CreateMap<CategoryModel, Category>()
              .ForMember(model => model.CreatedOnUtc, options => options.Ignore())
             .ForMember(model => model.UpdatedOnUtc, options => options.Ignore());

            CreateMap<Manufacturer, ManufacturerModel>().ForMember(x => x.PictureUrl, y => y.Ignore());
            CreateMap<ManufacturerModel, Manufacturer>()
              .ForMember(model => model.CreatedOnUtc, options => options.Ignore())
             .ForMember(model => model.UpdatedOnUtc, options => options.Ignore());


        }
        
    }
}
