using Infrastructure.Interface;
using Job_Portal.Models;
using JP_Core.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private IConfiguration Configuration;
        private Iuser _user;
        private string loginId;
        private readonly string connString;
        private static DateTime starttime;
        private IWebHostEnvironment _whe;
        public UserAuthController(IConfiguration _configuration, Iuser user, IWebHostEnvironment whe)
        {
            _user = user;
            Configuration = _configuration;
            _whe = whe;
            connString = this.Configuration.GetConnectionString("db_connect");
        }
        [HttpPost("userAuth")]
        public bool userAuth(user_entity obj)
        {
            loginId = obj.userName;
            starttime = DateTime.Now;
            return _user.login(obj.userName, obj.userPass);
        }
        [HttpGet("getTimer")]
        public string getTimer()
        {
            return (DateTime.Now - starttime).ToString(@"hh\:mm\:ss");
        }

        [HttpPost("postJobAd")]
        public bool postJobAd(job_entity obj)
        {
            bool response = _user.postJobAd(obj.title, obj.vacancies, obj.starttime, obj.endtime, obj.type, obj.level, obj.description);
            return response;
        }
        [HttpPost("saveChanges")]
        public bool saveChanges(job_entity obj)
        {
            bool response = _user.saveChanges(obj.id, obj.title, obj.vacancies, obj.starttime, obj.endtime, obj.type, obj.level, obj.description);
            return response;
        }
        [HttpGet("getActiveJobAds")]
        public List<job_entity> getActiveJobAds()
        {
            return _user.getActiveJobAds();
        }
        [HttpGet("getJobAdsHistory")]
        public List<job_entity> getJobAdsHistory()
        {
            return _user.getJobAdsHistory();
        }
        [HttpPost("deActivateAd")]
        public bool deActivateAd(job_entity obj)
        {
            return _user.deActivateAd(obj.id);
        }
        [HttpPost("reActivateAd")]
        public bool reActivateAd(job_entity obj)
        {
            return _user.reActivateAd(obj.id);
        }
        [HttpPost("deletePost")]
        public bool deletePost(job_entity obj)
        {
            return _user.deletePost(obj.id);
        }
        [HttpGet("getTotalAds")]
        public int getTotalAds()
        {
            return _user.getTotalAds();
        }
        [HttpGet("getActiveAds")]
        public int getActiveAds()
        {
            return _user.getActiveAds();
        }


        ////////   Product related Functions   ///////////
        
        [HttpPost("postNewProduct")]
        public bool postNewProduct(product_entity obj)
        {
            string[] imageName = obj.image.Split("\\");
            bool response = _user.postNewProduct(obj.productTitle, obj.url, obj.tools, obj.tech, obj.desc, imageName[imageName.Length - 1]);
            return response;
        }
        [HttpPost("editProduct")]
        public bool editProduct(product_entity obj)
        {
            string[] imageName = obj.image.Split("\\");
            bool response = _user.editProduct(obj.id,obj.productTitle, obj.url, obj.tools, obj.tech, obj.desc, imageName[imageName.Length - 1]);
            return response;
        }
        [HttpGet("getAllProducts")]
        public List<product_entity> getAllProducts()
        {
            return _user.getAllProducts();
        }
        [HttpPost("deleteProduct")]
        public bool deleteProduct(job_entity obj)
        {
            return _user.deleteProduct(obj.id);
        }


        ////////   Save Image to db  //////////

        [HttpPost("imageSaver")]
        public async Task<bool> imageSaver()
        {
            try
            {
                var Files = Request.Form.Files;
                foreach (IFormFile source in Files)
                {
                    string fileName = source.Name;
                    string imagePath = GetActualPath(fileName);
                    if (System.IO.File.Exists(imagePath))
                        System.IO.File.Delete(imagePath);
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await source.CopyToAsync(stream);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }
        public string GetActualPath(string FileName)
        {
            return Path.Combine(_whe.WebRootPath + "\\Uploads\\", FileName);
        }

    }
}
