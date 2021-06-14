using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ThinkBridge.Shop.Core.Domain;
using ThinkBridge.Shop.Core.Domain.Media;
using ThinkBridge.Shop.Services.FileHelper;
using ThinkBridge.Shop.Services.Media;

namespace ThinkBridge.Shop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    public partial class PictureController : ControllerBase
    {
        #region Fields

        private readonly IPictureService _pictureService;
        private readonly IFileHelperService _fileHelperService;

        #endregion

        #region Ctor

        public PictureController(IPictureService pictureService, IFileHelperService fileHelperService)
        {
            _pictureService = pictureService;
            _fileHelperService = fileHelperService;
        }

        #endregion

        #region Methods
        [HttpGet]
        public IActionResult Get()
        {            
            return Ok("Test");
        }


        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
            {
                return Ok(new
                {
                    success = false,
                    message = "No file uploaded",
                    downloadGuid = Guid.Empty
                });
            }

            var fileBinary = _pictureService.GetDownloadBits(httpPostedFile);

            var fileName = httpPostedFile.FileName;
            
            //remove path (passed in IE)
            fileName = _fileHelperService.GetFileName(fileName);

            var contentType = httpPostedFile.ContentType;

            var fileExtension = _fileHelperService.GetFileExtension(fileName);
            if (!string.IsNullOrEmpty(fileExtension))
                fileExtension = fileExtension.ToLowerInvariant();

            //contentType is not always available 
            //that's why we manually update it here
            //http://www.sfsu.edu/training/mimetype.htm
            if (string.IsNullOrEmpty(contentType))
            {
                switch (fileExtension)
                {
                    case ".bmp":
                        contentType = MimeTypes.ImageBmp;
                        break;
                    case ".gif":
                        contentType = MimeTypes.ImageGif;
                        break;
                    case ".jpeg":
                    case ".jpg":
                    case ".jpe":
                    case ".jfif":
                    case ".pjpeg":
                    case ".pjp":
                        contentType = MimeTypes.ImageJpeg;
                        break;
                    case ".png":
                        contentType = MimeTypes.ImagePng;
                        break;
                    case ".tiff":
                    case ".tif":
                        contentType = MimeTypes.ImageTiff;
                        break;
                    default:
                        break;
                }
            }
            var pic = new Picture
            {
                MimeType = contentType,
                Extension = fileExtension,
                UpdatedOnUtc = DateTime.UtcNow,
                CreatedOnUtc = DateTime.UtcNow,
                SeoFilename = fileName,
                AltAttribute = fileName,
                TitleAttribute = fileName
            };
            var picture = await _pictureService.InsertPicture(pic);
            var url = _pictureService.SavePictureInFile(picture.Id, fileBinary, fileExtension);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.
            return Ok(new { success = true, pictureId = picture.Id, imageUrl = url });
        }


        #endregion
    }
}