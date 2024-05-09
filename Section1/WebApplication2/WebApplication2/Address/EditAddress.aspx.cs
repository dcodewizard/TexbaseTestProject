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
    public partial class EditAddress : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int addressID = Convert.ToInt32(Request.QueryString["AddressID"]);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ReadAddress", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                }
            }
        }

        protected void SaveAddress(object sender, EventArgs e)
        {
            int addressID = Convert.ToInt32(Request.QueryString["AddressID"]);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateAddress", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AddressID", addressID);
                cmd.Parameters.AddWithValue("@Street", (txtStreet.Text));
                cmd.Parameters.AddWithValue("@City", (txtCity.Text));
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("ManageAddress.aspx");
        }
    }
}