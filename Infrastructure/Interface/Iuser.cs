using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JP_Core.Entities;

namespace Infrastructure.Interface
{
    public interface Iuser
    {
        public bool login(string userName,string userPassword);
        public bool postJobAd(string jobTitle, string nofvacancies,string starttime,string endtime, string jobType, string jobLevel, string description);
        public bool postNewProduct(string productTitle, string url, string tools, string tech, string desc,string imageName);
        public bool editProduct(string id,string productTitle, string url, string tools, string tech, string desc,string imageName);
        public bool saveChanges(string id,string jobTitle, string nofvacancies,string starttime,string endtime, string jobType, string jobLevel, string description);
        public List<job_entity> getActiveJobAds();
        public List<job_entity> getJobAdsHistory();
        public bool deActivateAd(string id);
        public bool reActivateAd(string id);
        public bool deletePost(string id);
        public bool deleteProduct(string id);
        public int getTotalAds();
        public int getActiveAds();
        public List<product_entity> getAllProducts();
    }
}
