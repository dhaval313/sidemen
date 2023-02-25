using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class LOC_StateController : Controller
    {
        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
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
            cmd.CommandText = "PR_LOC_State_SelectAll";
            SqlDataReader objSDR = cmd.ExecuteReader();

            dt.Load(objSDR);
            //return View("LOC_CountryList", dt);
            return View("LOC_StateList",dt);
        }
        public IActionResult Add(int? StateID)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn1 = new SqlConnection(str);
            conn1.Open();
            //Prepare Command
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";           
            DataTable dt1 = new DataTable();
            SqlDataReader objSDR1 = cmd1.ExecuteReader();
            dt1.Load(objSDR1);
            conn1.Close();
            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryID = Convert.ToInt32(dr["CountryID"]);
                vlst.CountryName= (string)dr["CountryName"];
                list.Add(vlst);
            }
            ViewBag.CountryList = list;

            #region select by pk
            if (StateID != null)
            {

                DataTable dt = new DataTable();

                //prepare conection
                SqlConnection conn = new SqlConnection(str);
                conn.Open();
                //Prepare Command
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PR_LOC_State_SelectByPK";
                cmd.Parameters.Add("StateID", SqlDbType.Int).Value = StateID;
                SqlDataReader objSDR = cmd.ExecuteReader();
                dt.Load(objSDR);
                if (dt.Rows.Count > 0)
                {
                    LOC_StateModel modelLOC_State = new LOC_StateModel();
                    foreach (DataRow dr in dt.Rows)
                    {
                        modelLOC_State.StateID = Convert.ToInt32(dr["StateID"]);
                        modelLOC_State.CountryID = Convert.ToInt32(dr["CountryID"]);
                        modelLOC_State.StateName = (string)dr["StateName"];
                        modelLOC_State.StateCode = (string)(dr["StateCode"]);
                        modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("LOC_StateAddEdit", modelLOC_State);
                }
            }
            #endregion

            return View("LOC_StateAddEdit");
        }
        [HttpPost]
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            string str = this.Configuration.GetConnectionString("myConnectionString");
            //prepare conection
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            //Prepare Command
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            if (modelLOC_State.StateID == 0)
            {
                cmd.CommandText = "PR_LOC_State_Insert";
                cmd.Parameters.AddWithValue("@CreationDate", SqlDbType.Date).Value = modelLOC_State.CreationDate;
            }
            else
            {
                cmd.CommandText = "PR_LOC_State_UpdateByPK";
                cmd.Parameters.AddWithValue("@StateID", SqlDbType.Int).Value = modelLOC_State.StateID;

            }
            cmd.Parameters.AddWithValue("@CountryID", SqlDbType.Int).Value = modelLOC_State.CountryID;
            cmd.Parameters.AddWithValue("@StateName", SqlDbType.VarChar).Value = modelLOC_State.StateName;
            cmd.Parameters.AddWithValue("@StateCode", SqlDbType.VarChar).Value = modelLOC_State.StateCode;
            cmd.Parameters.AddWithValue("@ModificationDate", SqlDbType.Date).Value = modelLOC_State.ModificationDate;
            if (Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if (modelLOC_State.StateID == 0 )
                {
                    TempData["StateInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["StateInsertMsg"] = "Record Updated Successfully";
                }
            }
            conn.Close();
            return Index();
        }

        #region delete
        public IActionResult Delete(int StateID)
        {
            String str = this.Configuration.GetConnectionString("myConnectionString");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_DeleteByPK";
            cmd.Parameters.AddWithValue("@StateId", StateID);
            cmd.ExecuteNonQuery();


            return RedirectToAction("Index");
        }
        #endregion
    }
}
