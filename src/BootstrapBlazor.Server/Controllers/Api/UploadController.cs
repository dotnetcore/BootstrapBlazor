using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebConsole.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> Post([FromServices]IWebHostEnvironment env, [FromForm]IFormCollection files)
        {
            var respons = new List<UploadFile>();
            foreach (var file in files.Files)
            {
                var uploadFile = new UploadFile();
                uploadFile.OriginFileName = file.Name;
                uploadFile.Size = file.Length;
                uploadFile.FileName = $"{DateTime.Now:yyyyMMddHHmmss}-{uploadFile.OriginFileName}";

                var webSiteUrl = $"images{Path.DirectorySeparatorChar}uploader{Path.DirectorySeparatorChar}";
                var filePath = Path.Combine(env.WebRootPath, webSiteUrl);
                if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

                filePath = Path.Combine(filePath, uploadFile.FileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    try
                    {
                        await file.CopyToAsync(fs);
                    }
                    catch (Exception ex)
                    {
                        uploadFile.Code = 1;
                        uploadFile.Error = ex.Message;
                    }
                }
                uploadFile.PrevUrl = $"{webSiteUrl}{uploadFile.FileName}";
                respons.Add(uploadFile);
            }
            return new JsonResult(respons);
        }

        /// <summary>
        /// 删除头像按钮调用
        /// </summary>
        /// <returns></returns>
        [HttpDelete()]
        public bool Delete([FromServices]IWebHostEnvironment env, [FromBody]string file)
        {
            var webSiteUrl = $"images{Path.DirectorySeparatorChar}uploader{Path.DirectorySeparatorChar}";
            var filePath = Path.Combine(env.WebRootPath, webSiteUrl);
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            filePath = Path.Combine(filePath, file);
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch { }
            }
            return true;
        }
    }
}
