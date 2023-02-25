using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class MST_ContactCategoryController : Controller
    {
        private IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            DataTable dt = new DataTable();
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_ContactCategory_SelectAll";
            SqlDataReader objSDR = cmd.ExecuteReader();

            dt.Load(objSDR);
            //return View("LOC_CountryList", dt);
            return View("MST_ContactCategoryList", dt);
        }
        public IActionResult Add(int? ContactCategoryID)
        {

            #region Select by pk
            if (ContactCategoryID != null)
            {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                //prepare conection
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                //Prepare Command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                cmd.Parameters.Add("CountryID", SqlDbType.Int).Value = ContactCategoryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
                foreach (DataRow dr in dt.Rows)
                {
                    modelMST_ContactCategory.ContactCategoryID = Convert.ToInt32(dr["CountryID"]);
                    modelMST_ContactCategory.ContactCategory = (string)dr["ContactCategory"];
                     }
                return View("LOC_CountryAddEdit", modelMST_ContactCategory);
            }
            #endregion
            return View("LOC_CountryAddEdit");
        }


        #region delete

        public IActionResult Delete(int ContactCategoryID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_MST_ContactCategory_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactCategoryId", ContactCategoryID);
            cmd.ExecuteNonQuery();


            return RedirectToAction("Index");
        }
        #endregion
    }
}
