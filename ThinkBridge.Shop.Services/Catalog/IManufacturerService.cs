using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Services
{
    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial interface IManufacturerService
    {
        /// <summary>
        /// Deletes a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        Task DeleteManufacturer(Manufacturer manufacturer);
        /// <summary>
        /// Gets a manufacturer
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Manufacturer</returns>
        Task<Manufacturer> GetManufacturerById(int manufacturerId);
        /// <summary>
        /// Get all Manufacturers
        /// </summary>
        /// <returns></returns>
        List<Manufacturer> GetAllManufacturer();

        /// <summary>
        /// Inserts a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        Task InsertManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Updates the manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        Task UpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Deletes a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        Task DeleteProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Gets a product manufacturer mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product manufacturer mapping collection</returns>
        IList<ProductManufacturer> GetProductManufacturersByProductId(int productId);

        /// <summary>
        /// Gets a product manufacturer mapping 
        /// </summary>
        /// <param name="productManufacturerId">Product manufacturer mapping identifier</param>
        /// <returns>Product manufacturer mapping</returns>
        Task<ProductManufacturer> GetProductManufacturerById(int productManufacturerId);

        /// <summary>
        /// Inserts a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        Task InsertProductManufacturer(ProductManufacturer productManufacturer);

        /// <summary>
        /// Updates the product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        Task UpdateProductManufacturer(ProductManufacturer productManufacturer);

    }
}