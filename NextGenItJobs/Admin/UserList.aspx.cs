using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace NextGenItJobs.Admin
{
    public partial class UserList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
        SqlCommand cmd;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["admin"] == null)
            {
                Response.Redirect("../User/Login.aspx");
            }
            if (!IsPostBack)
            {
                showUsers();
            }
        }

        private void showUsers()
        {
            string query = string.Empty;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
            query = @"Select Row_Number() over(Order by (Select 1)) as [Sr.No], UserId, Name, Email, Mobile, Country from [User]";

            cmd = new SqlCommand(query, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            showUsers();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int userId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                cmd = new SqlCommand("DELETE FROM [User] WHERE UserId = @id", con);
                cmd.Parameters.AddWithValue("@id", userId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "User Deleted Sucessfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Cannot Delete This Record!";
                    lblMsg.CssClass = "alert alert-danger";
                }                
                GridView1.EditIndex = -1;
                showUsers();
            }
            catch (Exception ex)
            {                
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            finally
            {
                con.Close();
            }
        }
    }
}