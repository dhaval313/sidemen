using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;
using System.Collections.Generic;
using System.Collections;

namespace WebApplication3.Controllers
{
    public class LOC_CityController : Controller
    {
        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_City_SelectAll";
            SqlDataReader objSDR = cmd.ExecuteReader();

            dt.Load(objSDR);

            //return View("LOC_CountryList", dt);
            return View("LOC_CityList",dt);
        }
        public IActionResult Add(int? CityID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            //Prepare Command
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_State_SelectForDropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();
            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = (string)dr["StateName"];
                list.Add(vlst);
            }
            ViewBag.StateList = list;


            SqlConnection conn2 = new SqlConnection(str);
            conn2.Open();
            //Prepare Command
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_Country_SelectForDropDown";
            DataTable dt2 = new DataTable();
            SqlDataReader objSDR2 = cmd2.ExecuteReader();
            dt2.Load(objSDR2);
            conn2.Close();
            List<LOC_CountryDropDownModel> list1 = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = (string)dr["CountryName"];
                list1.Add(vlst);
            }
            ViewBag.CountryList = list1;

            #region select by pk
            if (CityID != null)
            {

                DataTable dt = new DataTable();

                //prepare conection
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                //Prepare Command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_City_SelectByPK";
                cmd.Parameters.Add("CityID", SqlDbType.Int).Value = CityID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                if (dt.Rows.Count > 0)
                {
                    LOC_CityModel modelLOC_City = new LOC_CityModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_City.CityID = Convert.ToInt32(dr["CityID"]);
                        modelLOC_City.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_City.CityName = (string)dr["CityName"];
                        modelLOC_City.StateID = Convert.ToInt32(dr["StateID"]);
                        modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_CityAddEdit",modelLOC_City);
                }
            }
            #endregion

            return View("LOC_CityAddEdit");
        }
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            //Prepare Command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_City.CityID == 0)
            {
                cmd.CommandText = "PR_LOC_City_Insert";
                cmd.Parameters.AddWithValue("@CreationDate", SqlDbType.Date).Value = modelLOC_City.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_City_UpdateByPK";
                cmd.Parameters.AddWithValue("@CityID", SqlDbType.Int).Value = modelLOC_City.CityID;
                

            }

            cmd.Parameters.AddWithValue("@CountryID", SqlDbType.Int).Value = modelLOC_City.CountryID;
            cmd.Parameters.AddWithValue("@StateID", SqlDbType.Int).Value = modelLOC_City.StateID;
            cmd.Parameters.AddWithValue("@CityName", SqlDbType.VarChar).Value = modelLOC_City.CityName;
            cmd.Parameters.AddWithValue("@ModificationDate", SqlDbType.Date).Value = modelLOC_City.ModificationDate;
            
            if (Convert.ToBoolean(cmd.ExecuteNonQuery())) 
            {
                if (modelLOC_City.CityID == 0)
                {
                    TempData["CityInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CityInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return Index();
        }

        [HttpPost]
        public IActionResult DropdownByCountry(int CountryID)
        {
            #region DropDown State
            string str2 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_State_SelectionForDropDownByCountryID";
            cmd2.Parameters.AddWithValue("@CountryID", CountryID);
            DataTable dt3 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt3.Load(sdr2);
            conn2.Close();
            List<LOC_StateDropDownModel> list2 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr3 in dt3.Rows)
            {
                LOC_StateDropDownModel sdmlst = new LOC_StateDropDownModel();
                sdmlst.StateID = Convert.ToInt32(dr3["StateID"]);
                sdmlst.StateName = (string)dr3["StateName"];
                list2.Add(sdmlst);
            }
            //var is undefined datatype and must be initialised 
           
            var vModel = list2;
         
            return Json(vModel);
            #endregion
        }
        public IActionResult DropdownForState()
        {
            #region DropDown State
            string str2 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_State_SelectForDropDown";
           
            DataTable dt3 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt3.Load(sdr2);
            conn2.Close();
            List<LOC_StateDropDownModel> list2 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr3 in dt3.Rows)
            {
                LOC_StateDropDownModel sdmlst = new LOC_StateDropDownModel();
                sdmlst.StateID = Convert.ToInt32(dr3["StateID"]);
                sdmlst.StateName = (string)dr3["StateName"];
                list2.Add(sdmlst);
            }
            //var is undefined datatype and must be initialised 

            var vModel = list2;

            return Json(vModel);
            #endregion
        }


        #region Delete
        public IActionResult Delete(int CityID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_City_DeleteByPK";
            cmd.Parameters.AddWithValue("@CityId", CityID);
            cmd.ExecuteNonQuery();


            return RedirectToAction("Index");
        }
        #endregion
    }
}
