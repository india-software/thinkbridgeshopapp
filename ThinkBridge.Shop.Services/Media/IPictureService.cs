using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkBridge.Shop.Core.Domain.Media;

namespace ThinkBridge.Shop.Services.Media
{
    /// <summary>
    /// Picture service interface
    /// </summary>
    public partial interface IPictureService
    {

        /// <summary>
        /// Insert picture into table
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        Task<Picture> InsertPicture(Picture picture);
        /// <summary>
        /// Update picture
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        Task<Picture> UpdatePicture(Picture picture);
        /// <summary>
        /// Get Picture
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Picture> GetPicture(int Id);
        /// <summary>
        /// Delete picture
        /// </summary>
        /// <param name="picture"></param>
        Task DeletePicture(int Id);
        /// <summary>
        /// Get the bits of picture to write in memory
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        byte[] GetDownloadBits(IFormFile file);
        /// <summary>
        /// Save picture in file, We can also implement based on configuration to save image in file or db
        /// </summary>
        /// <param name="pictureId"></param>
        /// <param name="pictureBinary"></param>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        string SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType);
        string GetPictureUrl(int Id);

    }
}