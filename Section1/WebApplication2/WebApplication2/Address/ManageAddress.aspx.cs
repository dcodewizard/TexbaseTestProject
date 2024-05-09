using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class ManageAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        [System.Web.Services.WebMethod]
        public static bool DeleteAddress(int addressId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteAddress", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public static bool New(int AddressId)
        {
            Console.WriteLine(AddressId);
            return true;
        }
        protected void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ReadAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                gvCompanies.DataSource = dr;
                gvCompanies.DataBind();
            }
        }

        protected void AddAddress(object sender, EventArgs e)
        {
            Response.Redirect("AddAddress.aspx");
        }

        protected void EditAddress(object sender, EventArgs e)
        {
            LinkButton lnkEdit = (LinkButton)sender;
            int AddressId = Convert.ToInt32(lnkEdit.CommandArgument);
            Response.Redirect("EditAddress.aspx?AddressID=" + AddressId);
        }
    }
}
