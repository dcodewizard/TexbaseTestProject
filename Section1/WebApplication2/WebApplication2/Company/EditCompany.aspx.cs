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
    public partial class EditCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int companyID = Convert.ToInt32(Request.QueryString["CompanyID"]);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ReadCompanies", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                }
            }
        }

        protected void SaveCompany(object sender, EventArgs e)
        {
            int companyID = Convert.ToInt32(Request.QueryString["CompanyID"]);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateCompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyID", companyID);
                cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                cmd.Parameters.AddWithValue("@Street", (txtStreet.Text));
                cmd.Parameters.AddWithValue("@City", (txtCity.Text)); 
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("Home.aspx");
        }
    }
}