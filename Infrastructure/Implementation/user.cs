using Infrastructure.Interface;
using JP_Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class user : Iuser
    {
        private IConfiguration Configuration;
        private readonly string connString;
        private readonly SqlConnection con;
        public user(IConfiguration _configuration)
        {
            Configuration = _configuration;
            connString = this.Configuration.GetConnectionString("db_connect");
            con = new SqlConnection(connString);
        }
        public bool login(string userName, string userPassword)
        {
            try
            {
                con.Open();
                string query = "select * from tbl_Credentials where userName = '" + userName + "' and userPass = '" + userPassword + "'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    con.Close();
                    return true;
                }
                con.Close();
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool postJobAd(string jobTitle, string nofvacancies, string starttime, string endtime, string jobType, string jobLevel, string description)
        {
            try
            {
                con.Open();
                string query = "insert into tbl_JobsData values('" + jobTitle + "'," + nofvacancies + ",'','" + jobType + "','" + jobLevel + "','" + description + "','Active','" + DateTime.Now + "','" + starttime + "','" + endtime + "')";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool saveChanges(string id, string jobTitle, string nofvacancies, string starttime, string endtime, string jobType, string jobLevel, string description)
        {
            try
            {
                con.Open();
                string query = "update tbl_JobsData set jTitle = '" + jobTitle + "',vacancies = " + nofvacancies + ",jType = '" + jobType + "',jLevel = '" + jobLevel + "',jDesc = '" + description + "',startTime = '" + starttime + "',endTime = '" + endtime + "' where jId = '" + id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public List<job_entity> getActiveJobAds()
        {
            List<job_entity> list = new List<job_entity>();
            try
            {
                job_entity obj;
                con.Open();
                string query = "select * from tbl_JobsData where jStatus = 'Active' order by jId desc";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    obj = new job_entity();
                    obj.id = sdr["jId"].ToString();
                    obj.title = sdr["jTitle"].ToString();
                    obj.vacancies = sdr["vacancies"].ToString();
                    obj.starttime = sdr["startTime"].ToString();
                    obj.endtime = sdr["endTime"].ToString();
                    obj.type = sdr["jType"].ToString();
                    obj.level = sdr["jLevel"].ToString();
                    obj.description = sdr["jDesc"].ToString();
                    obj.status = sdr["jStatus"].ToString();
                    obj.postTime = sdr["postTime"].ToString();
                    list.Add(obj);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                return list;
                throw;
            }
        }
        public List<job_entity> getJobAdsHistory()
        {
            List<job_entity> list = new List<job_entity>();
            try
            {
                job_entity obj;
                con.Open();
                string query = "select * from tbl_JobsData order by jStatus asc";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    obj = new job_entity();
                    obj.id = sdr["jId"].ToString();
                    obj.title = sdr["jTitle"].ToString();
                    obj.vacancies = sdr["vacancies"].ToString();
                    obj.starttime = sdr["startTime"].ToString();
                    obj.endtime = sdr["endTime"].ToString();
                    obj.type = sdr["jType"].ToString();
                    obj.level = sdr["jLevel"].ToString();
                    obj.description = sdr["jDesc"].ToString();
                    obj.status = sdr["jStatus"].ToString();
                    obj.postTime = sdr["postTime"].ToString();
                    list.Add(obj);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                return list;
                throw;
            }
        }
        public bool deActivateAd(string id)
        {
            try
            {
                con.Open();
                string query = "update tbl_JobsData set jStatus = 'Inactive' where jId='" + id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool reActivateAd(string id)
        {
            try
            {
                con.Open();
                string query = "update tbl_JobsData set jStatus = 'Active' where jId='" + id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool deletePost(string id)
        {
            try
            {
                con.Open();
                string query = "delete from tbl_JobsData where jId='" + id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public int getTotalAds()
        {
            int count = 0;
            try
            {
                con.Open();
                string query = "select count([SM-JobPortal].[dbo].[tbl_JobsData].jId) as counter from tbl_JobsData";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    count = int.Parse(sdr["counter"].ToString());
                }
                con.Close();
                return count;
            }
            catch (Exception)
            {
                return count;
                throw;
            }
        }
        public int getActiveAds()
        {
            int count = 0;
            try
            {
                con.Open();
                string query = "select count([SM-JobPortal].[dbo].[tbl_JobsData].jId) as counter from tbl_JobsData where jStatus = 'Active'";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                if (sdr.Read())
                {
                    count = int.Parse(sdr["counter"].ToString());
                }
                con.Close();
                return count;
            }
            catch (Exception)
            {
                return count;
                throw;
            }
        }

        ////////// Products related functions  ////////////
        public List<product_entity> getAllProducts()
        {
            List<product_entity> list = new List<product_entity>();
            try
            {
                product_entity obj;
                con.Open();
                string query = "select * from tbl_Products order by id desc";
                SqlCommand com = new SqlCommand(query, con);
                SqlDataReader sdr = com.ExecuteReader();
                while (sdr.Read())
                {
                    obj = new product_entity();
                    obj.id = sdr["id"].ToString();
                    obj.productTitle = sdr["Title"].ToString();
                    obj.url = sdr["Url"].ToString();
                    obj.tools = sdr["Tools"].ToString();
                    obj.tech = sdr["Tech"].ToString();
                    obj.desc = sdr["Description"].ToString();
                    obj.image = sdr["Image"].ToString();
                    obj.addedOn = sdr["AddedOn"].ToString();
                    list.Add(obj);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                return list;
                throw;
            }
        }
        public bool postNewProduct(string productTitle, string url, string tools, string tech, string desc, string imageName)
        {
            try
            {
                con.Open();
                string query = "insert into tbl_Products values('" + productTitle + "','" + url + "','" + tools + "','" + tech + "','" + desc + "','" + imageName + "','" + DateTime.Now + "')";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool editProduct(string id,string productTitle, string url, string tools, string tech, string desc, string imageName)
        {
            try
            {
                con.Open();
                string query = "update tbl_Products set Title = '" + productTitle + "', Url = '" + url + "',Tools = '" + tools + "', Tech = '" + tech + "', Description = '" + desc + "',Image = '" + imageName + "' where id = '"+id+"'";
                if (imageName == "" || imageName == null) {
                    query = "update tbl_Products set Title = '" + productTitle + "', Url = '" + url + "',Tools = '" + tools + "', Tech = '" + tech + "', Description = '" + desc + "' where id = '" + id + "'";
                }
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool deleteProduct(string id)
        {
            try
            {
                con.Open();
                string query = "delete from tbl_Products where id='" + id + "'";
                SqlCommand com = new SqlCommand(query, con);
                com.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
