using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class LOC_ContactController : Controller
    {
        
            private IConfiguration Configuration;

        public object ContactID { get; private set; }

        public LOC_ContactController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region Index
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_SelectAll";
            SqlDataReader objSDR = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(objSDR);
            //return View("LOC_CountryList", dt);
            return View("LOC_ContactList", dt);
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");

            //============================//

            //dropdown 4
            SqlConnection conn4 = new SqlConnection(str);
            conn4.Open();
            //Prepare Command
            SqlCommand cmd4 = conn4.CreateCommand();
            cmd4.CommandType = CommandType.StoredProcedure;
            cmd4.CommandText = "PR_LOC_City_SelectForDropDown";
            DataTable dt4 = new DataTable();
            SqlDataReader objSDR4 = cmd4.ExecuteReader();
            dt4.Load(objSDR4);
            conn4.Close();
            List<LOC_CityDropDownModel> list4 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr in dt4.Rows)
            {
                LOC_CityDropDownModel vlst = new LOC_CityDropDownModel();
                vlst.CityID = Convert.ToInt32(dr["CityID"]);
                vlst.CityName = (string)dr["CityName"];
                list4.Add(vlst);
            }
            ViewBag.CityList = list4;

            //============================//

            //  //dropdown 3
            SqlConnection conn3 = new SqlConnection(str);
            conn3.Open();
            //Prepare Command
            SqlCommand cmd3 = conn3.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "PR_LOC_State_SelectForDropDown";
            DataTable dt3 = new DataTable();
            SqlDataReader objSDR3 = cmd3.ExecuteReader();
            dt3.Load(objSDR3);
            conn3.Close();
            List<LOC_StateDropDownModel> list3 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dr in dt3.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateID = Convert.ToInt32(dr["StateID"]);
                vlst.StateName = (string)dr["StateName"];
                list3.Add(vlst);
            }
            ViewBag.StateList = list3;

            //============================//

            //dropdown 2
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
            List<LOC_CountryDropDownModel> list2 = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt2.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName = (string)dr["CountryName"];
                list2.Add(vlst);
            }
            ViewBag.CountryList = list2;

            //============================//

            //dropdown 1
            //prepare conection
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            //Prepare Command
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();
            List<MST_ContactCategoryDropDownModel> list1 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                MST_ContactCategoryDropDownModel vlst = new MST_ContactCategoryDropDownModel();
                vlst.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                vlst.ContactCategory = (string)dr["ContactCategory"];
                list1.Add(vlst);
            }
            ViewBag.ContactCategoryList = list1;
            ///==========================================//
            #region select by pk
            if (ContactID != null)
            {

                DataTable dt = new DataTable();

                //prepare conection
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                //Prepare Command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_CON_Contact_SelectByPK";
                cmd.Parameters.Add("ContactID", SqlDbType.Int).Value = ContactID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                if (dt.Rows.Count > 0)
                {
                    LOC_ContactModel modelLOC_Contact = new LOC_ContactModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        modelLOC_Contact.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_Contact.StateID = Convert.ToInt32(dr["StateID"]);
                        modelLOC_Contact.CityID = Convert.ToInt32(dr["CityID"]);
                        modelLOC_Contact.ContactCategoryID = Convert.ToInt32(dr["ContactCategoryID"]);
                        modelLOC_Contact.ContactName = (string)dr["ContactName"];
                        modelLOC_Contact.Pincode = Convert.ToInt32(dr["Pincode"]);
                        modelLOC_Contact.MobileNumber = (string)(dr["MobileNumber"]);
                        modelLOC_Contact.AlternativeMobile = (string)(dr["AlternativeMobile"]);
                        modelLOC_Contact.Email = (string)(dr["Email"]);
                        modelLOC_Contact.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                        modelLOC_Contact.AnniversaryDate = Convert.ToDateTime(dr["AnniversaryDate"]);
                        modelLOC_Contact.TypeOfProfession = (string)(dr["TypeOfProfession"]);
                        modelLOC_Contact.CompanyName = (string)(dr["CompanyName"]);
                        modelLOC_Contact.Designation = (string)(dr["Designation"]);
                        modelLOC_Contact.LinkedIn = (string)(dr["LinkedIn"]);
                        modelLOC_Contact.Twitter = (string)(dr["Twitter"]);
                        modelLOC_Contact.Insta = (string)(dr["Insta"]);
                    }
                    return View("LOC_ContactAddEdit", modelLOC_Contact);
                }
            }
            #endregion
            return View("LOC_ContactAddEdit");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(LOC_ContactModel modelLOC_Contact)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            //Prepare Command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_Contact.ContactID == 0)
            {
                
                cmd.CommandText = "PR_CON_Contact_Insert";
                
            }
            else
            {
                cmd.CommandText = "PR_CON_Contact_UpdateByPK";
                cmd.Parameters.AddWithValue("@ContactID", SqlDbType.Int).Value = modelLOC_Contact.ContactID;

            }
            
            cmd.Parameters.AddWithValue("@CountryID", SqlDbType.Int).Value = modelLOC_Contact.CountryID;
            cmd.Parameters.AddWithValue("@ContactCategoryID", SqlDbType.Int).Value = modelLOC_Contact.ContactCategoryID;
            cmd.Parameters.AddWithValue("@StateID", SqlDbType.Int).Value = modelLOC_Contact.StateID;
            cmd.Parameters.AddWithValue("@CityID", SqlDbType.Int).Value = modelLOC_Contact.CityID;
            cmd.Parameters.AddWithValue("@ContactName", SqlDbType.VarChar).Value = modelLOC_Contact.ContactName;
            cmd.Parameters.AddWithValue("@Pincode", SqlDbType.Int).Value = modelLOC_Contact.Pincode;
            cmd.Parameters.AddWithValue("@Mobile", SqlDbType.VarChar).Value = modelLOC_Contact.MobileNumber;
            cmd.Parameters.AddWithValue("@AlternativeMobile", SqlDbType.VarChar).Value = modelLOC_Contact.AlternativeMobile;
            cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = modelLOC_Contact.Email;
            cmd.Parameters.AddWithValue("@Linkedln", SqlDbType.VarChar).Value = modelLOC_Contact.LinkedIn;
            cmd.Parameters.AddWithValue("@Instagram", SqlDbType.VarChar).Value = modelLOC_Contact.Insta;
            cmd.Parameters.AddWithValue("@Twitter", SqlDbType.VarChar).Value = modelLOC_Contact.Twitter;
            cmd.Parameters.AddWithValue("@TypeOfProfession", SqlDbType.VarChar).Value = modelLOC_Contact.TypeOfProfession;
            cmd.Parameters.AddWithValue("@CompanyName", SqlDbType.VarChar).Value = modelLOC_Contact.CompanyName;
            cmd.Parameters.AddWithValue("@Designation", SqlDbType.VarChar).Value = modelLOC_Contact.Designation;
            cmd.Parameters.AddWithValue("@BirthDate", SqlDbType.Date).Value = modelLOC_Contact.BirthDate;
            cmd.Parameters.AddWithValue("@AnniversaryDate", SqlDbType.Date).Value = modelLOC_Contact.AnniversaryDate;


            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_Contact.ContactID == 0)
                {
                    TempData["ContactInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["ContactInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return RedirectToAction("Index");
        }
        #endregion

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
                sdmlst.StateName = dr3["StateName"].ToString();
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


        [HttpPost]
        public IActionResult DropdownByState(int StateID)
        {
            #region DropDown city
            string str2 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_City_SelectionForDropDownByStateID";
            cmd2.Parameters.AddWithValue("@StateID", StateID);
            DataTable dt3 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt3.Load(sdr2);
            conn2.Close();
            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr3 in dt3.Rows)
            {
                LOC_CityDropDownModel sdmlst = new LOC_CityDropDownModel();
                sdmlst.CityID = Convert.ToInt32(dr3["CityID"]);
                sdmlst.CityName = (string)dr3["CityName"];
                list2.Add(sdmlst);
            }
            //var is undefined datatype and must be initialised 

            var vModel = list2;

            return Json(vModel);
            #endregion
        }
        public IActionResult DropdownForCity()
        {
            #region DropDown city
            string str2 = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_City_SelectForDropDown";

            DataTable dt3 = new DataTable();
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            dt3.Load(sdr2);
            conn2.Close();
            List<LOC_CityDropDownModel> list2 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dr3 in dt3.Rows)
            {
                LOC_CityDropDownModel sdmlst = new LOC_CityDropDownModel();
                sdmlst.CityID = Convert.ToInt32(dr3["CityID"]);
                sdmlst.CityName = (string)dr3["CityName"];
                list2.Add(sdmlst);
            }
            //var is undefined datatype and must be initialised 

            var vModel = list2;

            return Json(vModel);
            #endregion
        }

        #region delete
        public IActionResult Delete(int ContactID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactId", ContactID);
            cmd.ExecuteNonQuery();


            return RedirectToAction("Index");
        }
        #endregion
    }
}

