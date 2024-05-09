using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel.Design;

namespace WebApplication2
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        [System.Web.Services.WebMethod]
        public static bool DeleteCompany(int companyId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteCompany", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CompanyID", companyId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        protected void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("ReadCompanies", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                gvCompanies.DataSource = dr;
                gvCompanies.DataBind();
            }
        }

        protected void AddCompany(object sender, EventArgs e)
        {
            Response.Redirect("AddCompany.aspx");
        }

        protected void EditCompany(object sender, EventArgs e)
        {
            LinkButton lnkEdit = (LinkButton)sender;
            int companyID = Convert.ToInt32(lnkEdit.CommandArgument);
            Response.Redirect("EditCompany.aspx?CompanyID=" + companyID);
        }
    }
}