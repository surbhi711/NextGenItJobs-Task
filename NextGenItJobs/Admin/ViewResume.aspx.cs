﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace NextGenItJobs.Admin
{
    public partial class ViewResume : System.Web.UI.Page
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
                ShowAppliedJob();
            }
        }

        private void ShowAppliedJob()
        {
            string query = string.Empty;
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
            query = @"Select Row_Number() over(Order by(Select 1)) as [Sr.No],aj.AppliedJobId,j.CompanyName,aj.JobId,j.Title,u.Mobile,
                        u.Name,u.Email,u.Resume from AppliedJobs aj
                        inner join [User] u on aj.UserId = u.UserId
                        inner join Jobs j on aj.JobId = j.JobId";

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
            ShowAppliedJob();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.Rows[e.RowIndex];
                int applliedJobId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                cmd = new SqlCommand("DELETE FROM AppliedJobs WHERE AppliedJobId = @id", con);
                cmd.Parameters.AddWithValue("@id", applliedJobId);
                con.Open();
                int r = cmd.ExecuteNonQuery();
                if (r > 0)
                {
                    lblMsg.Text = "Resume Deleted Sucessfully!";
                    lblMsg.CssClass = "alert alert-success";
                }
                else
                {
                    lblMsg.Text = "Cannot Delete This Record!";
                    lblMsg.CssClass = "alert alert-danger";
                }                
                GridView1.EditIndex = -1;
                ShowAppliedJob();
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to View Job Details";
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(GridViewRow row in GridView1.Rows)
            {
                if(row.RowIndex == GridView1.SelectedIndex)
                {
                    HiddenField jobId = (HiddenField)row.FindControl("hdnJobId");
                    Response.Redirect("JobList.aspx?id="+jobId.Value);
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                    row.ToolTip = "Click To Select this Row";
                }
            }
        }
    }
}