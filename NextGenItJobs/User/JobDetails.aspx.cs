﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace NextGenItJobs.User
{
    public partial class JobDetails : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt, dt1;
        public string jobTitle = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            if(Request.QueryString["id"] != null)
            {
                showJobDetails();
                DataBind();
            }
            else
            {
                Response.Redirect("JobListing.aspx");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void showJobDetails()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
            string query = @"SELECT * FROM Jobs WHERE JobId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id",Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            DataList1.DataSource = dt;
            DataList1.DataBind();
            jobTitle = dt.Rows[0]["Title"].ToString();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if(e.CommandName == "ApplyJob")
            {
                if(Session["user"] != null)
                {
                    try
                    {
                        con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                        string query = @"INSERT INTO AppliedJobs VALUES(@JobId,@UserId)";
                        cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
                        cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if(r > 0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Job Applied Successfull!";
                            lblMsg.CssClass = "alert alert-success";
                            showJobDetails();
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Cannot Applied the job Please try after sometime!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    catch(Exception ex)
                    {
                        Response.Write("<script>alert('"+ ex.Message +"');</script>");
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (Session["user"] != null)
            {
                LinkButton btnApplyJob = e.Item.FindControl("lbApplyJob") as LinkButton;
                if(isApplied())
                {
                    btnApplyJob.Enabled = false;
                    btnApplyJob.Text = "Applied";
                }
                else
                {
                    btnApplyJob.Enabled = true;
                    btnApplyJob.Text = "Apply Now";
                }
            }
        }

        bool isApplied()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
            string query = @"SELECT * FROM AppliedJobs WHERE UserId = @UserId AND JobId = @JobId";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@UserId", Session["userId"]);
            cmd.Parameters.AddWithValue("@JobId", Request.QueryString["id"]);
            sda = new SqlDataAdapter(cmd);
            dt1 = new DataTable();
            sda.Fill(dt1);
            if(dt1.Rows.Count==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";

            if (string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.jpg";
            }
            else
            {
                url1 = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }
    }
}