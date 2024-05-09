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
    public partial class AddCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void SaveCompany(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("CreateCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@Street", (txtStreet.Text));
                cmd.Parameters.AddWithValue("@City", (txtCity.Text));
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("Home.aspx");
        }
    }
}