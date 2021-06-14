using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Data;

namespace ThinkBridge.Shop.Services
{
    /// <summary>
    /// Manufacturer service
    /// </summary>
    public partial class ManufacturerService : IManufacturerService
    {
        #region Fields
        private readonly IRepository<Manufacturer> _manufacturerRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductManufacturer> _productManufacturerRepository;
        #endregion

        #region Ctor
        public ManufacturerService(
            IRepository<Manufacturer> manufacturerRepository,
            IRepository<Product> productRepository,
            IRepository<ProductManufacturer> productManufacturerRepository
           )
        {

            _manufacturerRepository = manufacturerRepository;
            _productRepository = productRepository;
            _productManufacturerRepository = productManufacturerRepository;
        }
        #endregion

        #region Methods
       

        /// <summary>
        /// Gets a manufacturer
        /// </summary>
        /// <param name="manufacturerId">Manufacturer identifier</param>
        /// <returns>Manufacturer</returns>
        public async Task<Manufacturer> GetManufacturerById(int manufacturerId)
        {
            if (manufacturerId == 0)
                return null;
           return await _manufacturerRepository.GetById(manufacturerId);
        }

        /// <summary>
        /// Inserts a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        public async Task InsertManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

           await _manufacturerRepository.Insert(manufacturer);
        }

        /// <summary>
        /// Updates the manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

           await _manufacturerRepository.Update(manufacturer);

        }
        /// <summary>
        /// Deletes a manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer</param>
        public async Task DeleteManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            manufacturer.Deleted = true;
            await UpdateManufacturer(manufacturer);
        }

        /// <summary>
        /// Deletes a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        public async Task DeleteProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException(nameof(productManufacturer));

           await _productManufacturerRepository.Delete(productManufacturer);

        }


        /// <summary>
        /// Gets a product manufacturer mapping collection
        /// </summary>
        /// <param name="productId">Product identifier</param>
        /// <returns>Product manufacturer mapping collection</returns>
        public virtual IList<ProductManufacturer> GetProductManufacturersByProductId(int productId)
        {
            if (productId == 0)
                return new List<ProductManufacturer>();

            var query = from pm in _productManufacturerRepository.Table
                        join m in _manufacturerRepository.Table on pm.ManufacturerId equals m.Id
                        where pm.ProductId == productId &&
                            !m.Deleted && m.Published
                        orderby pm.DisplayOrder, pm.Id
                        select pm;

            query = query.Distinct().OrderBy(pm => pm.DisplayOrder).ThenBy(pm => pm.Id);

            return query.ToList();
        }

        /// <summary>
        /// Gets a product manufacturer mapping 
        /// </summary>
        /// <param name="productManufacturerId">Product manufacturer mapping identifier</param>
        /// <returns>Product manufacturer mapping</returns>
        public async Task<ProductManufacturer> GetProductManufacturerById(int productManufacturerId)
        {
            if (productManufacturerId == 0)
                return null;

            return await  _productManufacturerRepository.GetById(productManufacturerId);
        }

        /// <summary>
        /// Inserts a product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        public async Task InsertProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException(nameof(productManufacturer));

           await _productManufacturerRepository.Insert(productManufacturer);
           
        }

        /// <summary>
        /// Updates the product manufacturer mapping
        /// </summary>
        /// <param name="productManufacturer">Product manufacturer mapping</param>
        public async Task UpdateProductManufacturer(ProductManufacturer productManufacturer)
        {
            if (productManufacturer == null)
                throw new ArgumentNullException(nameof(productManufacturer));

            await _productManufacturerRepository.Update(productManufacturer);
        }

        public List<Manufacturer> GetAllManufacturer()
        {
            return _manufacturerRepository.Table.Where(x => x.Published && !x.Deleted).ToList();
        }

        #endregion
    }
}