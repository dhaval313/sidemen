using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication3.Models;
using System.Data.SqlClient;


namespace WebApplication3.Controllers
{
    public class LOC_CountryController : Controller
	{
		private IConfiguration Configuration;
        private object modelLOC_Country;

        public LOC_CountryController(IConfiguration _configuration)
		{
			Configuration = _configuration;
		}
        #region SelectAll
        public IActionResult Index()
		{
			string str = this.Configuration.GetConnectionString("myConnectionString");	
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectAll";
            SqlDataReader objSDR= cmd.ExecuteReader();
			DataTable dt = new DataTable();
            dt.Load(objSDR);
			//return View("LOC_CountryList", dt);
			return View("LOC_CountryList", dt);
		}
        #endregion

        #region add
        public IActionResult Add(int? CountryID) {

            #region Select by pk
            if (CountryID != null) {
                string str = this.Configuration.GetConnectionString("myConnectionString");
                //prepare conection
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                //Prepare Command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                cmd.Parameters.Add("CountryID",SqlDbType.Int).Value =CountryID;
                DataTable dt = new DataTable();
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {            
                    modelLOC_Country.CountryID = Convert.ToInt32(dr["CountryID"]);
                    modelLOC_Country.CountryName = (string)dr["CountryName"];
                    modelLOC_Country.CountryCode = (string)dr["CountryCode"];
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
    
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            #endregion
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int CountryID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_DeleteByPK";
            cmd.Parameters.AddWithValue("@CountryId", CountryID);
            cmd.ExecuteNonQuery();


            return RedirectToAction("Index");
        }
        #endregion

        #region Insert
        [HttpPost] 
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if(modelLOC_Country.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(),FilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                    string fileNameWithPath = Path.Combine(path,modelLOC_Country.File.FileName);
                modelLOC_Country.PhotoPath = FilePath.Replace("wwwroot\\", "/")+"/"+modelLOC_Country.File.FileName;
                using(var stream = new FileStream(fileNameWithPath,FileMode.Create))
                {
                    modelLOC_Country.File.CopyTo(stream);
                }
            }
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            //Prepare Command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_Country.CountryID == 0)
            {
                cmd.CommandText = "PR_LOC_Country_Insert";       
            }
            else
            {
                cmd.CommandText = "PR_LOC_Country_UpdateByPK";
                cmd.Parameters.AddWithValue("@CountryID", SqlDbType.Int).Value = modelLOC_Country.CountryID;                             
            }
            cmd.Parameters.AddWithValue("@CountryName", SqlDbType.VarChar).Value = modelLOC_Country.CountryName;
            cmd.Parameters.AddWithValue("@CountryCode", SqlDbType.VarChar).Value = modelLOC_Country.CountryCode;
            cmd.Parameters.AddWithValue("@PhotoPath", SqlDbType.NVarChar).Value = modelLOC_Country.PhotoPath;
            cmd.Parameters.AddWithValue("@CreationDate", SqlDbType.Date).Value = modelLOC_Country.CreationDate;
            cmd.Parameters.AddWithValue("@ModificationDate", SqlDbType.Date).Value = modelLOC_Country.ModificationDate;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery())) {
                if (modelLOC_Country.CountryID == 0)
                {
                    TempData["CountryInsertMsg"] = "Reacord Inserted Successfully";
                }
                else
                {
                    TempData["CountryInsertMsg"] = "Reacord Updated Successfully";
                }
            }
            conn.Close();
            return View("LOC_CountryAddEdit");
        }
        #endregion

        #region Filter Records
        public IActionResult Filter(string? CountryName = null, string? CountryCode = null)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_Country_SelectByCountryNameCode";

            if (CountryName == null)
            {
                CountryName = "";
            }
            if (CountryCode == null)
            {
                CountryCode = "";
            }
            //cmd.Parameters.AddWithValue("@CountryName", CountryName);
            //cmd.Parameters.AddWithValue("@CountryCode", CountryCode);

            cmd.Parameters.Add("@CountryName", SqlDbType.NVarChar).Value = CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.NVarChar).Value = CountryCode;
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();//ExecuteReader()-Read All Data       EecuteScalar-Single Row and Single Column      ExecuteNonQuery()-Insert Update Delete
            dt.Load(sdr);
            conn.Close();
            return View("LOC_CountryList", dt);
        }
        #endregion

    }
}
