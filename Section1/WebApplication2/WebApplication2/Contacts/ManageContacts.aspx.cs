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
    public partial class ManageContacts : System.Web.UI.Page
    {   
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            [System.Web.Services.WebMethod]
            public static bool DeleteContact(int contactId)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["testproject1Entities"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteContact", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ContactId", contactId);
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
                    SqlCommand cmd = new SqlCommand("ReadContacts", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    gvCompanies.DataSource = dr;
                    gvCompanies.DataBind();
                }
            }

            protected void AddContact(object sender, EventArgs e)
            {
                Response.Redirect("AddContacts.aspx");
            }

            protected void EditContact(object sender, EventArgs e)
            {
                LinkButton lnkEdit = (LinkButton)sender;
                int ContactId = Convert.ToInt32(lnkEdit.CommandArgument);
                Response.Redirect("EditContacts.aspx?ContactID=" + ContactId);
            }
        }
    }