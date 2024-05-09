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
    public partial class EditContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int companyID = Convert.ToInt32(Request.QueryString["ContactID"]);
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("ReadContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                }
            }
        }

        protected void SaveContact(object sender, EventArgs e)
        {
            int contactID = Convert.ToInt32(Request.QueryString["ContactID"]);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateContact", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactID", contactID);
                cmd.Parameters.AddWithValue("@ContactName", txtContactName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@Street", (txtStreet.Text));
                cmd.Parameters.AddWithValue("@City", (txtCity.Text));
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("ManageContacts.aspx");
        }
    }
}