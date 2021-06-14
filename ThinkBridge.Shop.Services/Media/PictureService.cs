using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Domain.Catalog;
using ThinkBridge.Shop.Core.Domain.Media;
using ThinkBridge.Shop.Data;
using ThinkBridge.Shop.Services.FileHelper;

namespace ThinkBridge.Shop.Services.Media
{
    /// <summary>
    /// Picture service
    /// </summary>
    public partial class PictureService : IPictureService
    {
        #region Fields      
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IFileHelperService _fileHelperService;
        private readonly IRepository<ProductPicture> _productPictureRepository;
        #endregion
        #region Ctor

        public PictureService(IFileHelperService fileHelperService, 
            IRepository<Picture> pictureRepository,
            IRepository<ProductPicture> productPictureRepository)
        {
            _fileHelperService = fileHelperService;
            _pictureRepository = pictureRepository;
            _productPictureRepository = productPictureRepository;
        }

        #endregion

        #region Utilities
        public virtual async Task<Picture> InsertPicture(Picture picture)
        {
            await _pictureRepository.Insert(picture);
            return picture;
        }
        public virtual byte[] GetDownloadBits(IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                     fileStream.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                }
            }
        }
        public string SavePictureInFile(int pictureId, byte[] pictureBinary, string extension)
        {
            var fileName = $"{pictureId:0000000}_0{extension}";
            var filepath = GetPictureLocalPath(fileName);
            _fileHelperService.WriteAllBytes(filepath, pictureBinary);
            return "/Upload/" + fileName;
        }

        protected virtual string GetPictureLocalPath(string fileName)
        {
            return _fileHelperService.GetAbsolutePath("upload", fileName);
        }
        public async Task<Picture> UpdatePicture(Picture picture)
        {
            await _pictureRepository.Update(picture);
            return picture;
        }
        public async Task<Picture> GetPictureById(int picId)
        {
           return await _pictureRepository.GetById(picId);
        }
        public string GetPictureUrl(int Id)
        {
            var picture = _pictureRepository.GetById(Id).Result;
            if (picture != null)
            {
                var fileName = $"{picture.Id:0000000}_0{picture.Extension}";
                return "/Upload/" + fileName;
            }
            else
                return string.Empty;
        }

        public async Task DeletePicture(int Id)
        {
            var picture = await _pictureRepository.GetById(Id);
            await _pictureRepository.Delete(picture);
            var fileName = $"{picture.Id:0000000}_0{picture.Extension}";
            _fileHelperService.DeleteFile(GetPictureLocalPath(fileName));
        }
       
        public virtual IList<Picture> GetPicturesByProductId(int productId)
        {
            if (productId == 0)
                return new List<Picture>();

            var query = from p in _pictureRepository.Table
                        join pp in _productPictureRepository.Table on p.Id equals pp.PictureId
                        orderby pp.DisplayOrder, pp.Id
                        where pp.ProductId == productId
                        select p;

            return query.ToList();
        }

        public async Task<Picture> GetPicture(int Id)
        {
            return await _pictureRepository.GetById(Id);
        }
        #endregion
    }
}