using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace NextGenItJobs.User
{
    public partial class ResumeBuild : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader sdr;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if(!IsPostBack)
            {
                if(Request.QueryString["id"]!=null)
                {
                    showUserInfo();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void showUserInfo()
        {
            try
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                string query = "SELECT * FROM [User] WHERE UserId=@UserId";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);
                con.Open();
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    if (sdr.Read())
                    {
                        txtUsername.Text = sdr["UserName"].ToString();
                        txtFullName.Text = sdr["Name"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();
                        txtMobile.Text = sdr["Mobile"].ToString();
                        txtTenth.Text = sdr["TenthGrade"].ToString();
                        txtTwelth.Text = sdr["TwelfthGrade"].ToString();
                        txtGraduation.Text = sdr["GraduationGrade"].ToString();
                        txtPostGraduation.Text = sdr["PostGraduationGrade"].ToString();
                        txtPhd.Text = sdr["Phd "].ToString();
                        txtWork.Text = sdr["WorkOn"].ToString();
                        txtExperience.Text = sdr["Experience"].ToString();
                        txtAddress.Text = sdr["Address"].ToString();
                        ddlCountry.SelectedValue = sdr["Country"].ToString();
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "User Not Found";
                    lblMsg.CssClass = "alert alert-danger";
                }

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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null)
                {
                    string concatQuery = string.Empty;
                    string filePath = string.Empty;
                    //bool isValidToExecute = false;
                    bool isVaid = false;
                    con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                    if (fuResume.HasFile)
                    {
                        if (Utils.isValidExtensionResume(fuResume.FileName))
                        {
                            concatQuery = "Resume=@Resume,";
                            isVaid = true;
                        }
                        else
                        {
                            concatQuery = string.Empty;                            
                        }
                    }
                    else
                    {
                        concatQuery = string.Empty;
                    }
                    query = @"UPDATE [User] SET UserName=@UserName,Name=@Name,Email=@Email,Mobile=@Mobile,TenthGrade=@TenthGrade,TwelfthGrade=@TwelfthGrade,GraduationGrade=@GraduationGrade,PostGraduationGrade=@PostGraduationGrade,Phd=@Phd,WorkOn=@WorkOn,Experience=@Experience," + concatQuery + "Address=@Address,Country=@Country WHERE UserId=@UserId";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@UserName", txtUsername.Text.Trim());
                    cmd.Parameters.AddWithValue("@Name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenthGrade", txtTenth.Text.Trim());
                    cmd.Parameters.AddWithValue("@TwelfthGrade", txtTwelth.Text.Trim());
                    cmd.Parameters.AddWithValue("@GraduationGrade", txtGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@PostGraduationGrade", txtPostGraduation.Text.Trim());
                    cmd.Parameters.AddWithValue("@Phd", txtPhd.Text.Trim());
                    cmd.Parameters.AddWithValue("@WorkOn", txtWork.Text.Trim());
                    cmd.Parameters.AddWithValue("@Experience", txtExperience.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@UserId", Request.QueryString["id"]);

                    if (fuResume.HasFile)
                    {
                        if (Utils.isValidExtensionResume(fuResume.FileName))
                        {
                            Guid obj = Guid.NewGuid();
                            filePath = "Resumes/" + obj.ToString() + fuResume.FileName;
                            fuResume.PostedFile.SaveAs(Server.MapPath("~/Resumes/") + obj.ToString() + fuResume.FileName);
                            cmd.Parameters.AddWithValue("@Resume", filePath);
                            isVaid = true; 
                        }
                        else
                        {
                            concatQuery = string.Empty;
                            lblMsg.Visible = true;
                            lblMsg.Text = "Please Select .doc, .docx, .pdf File For Resume!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                    else
                    {
                        isVaid = true;
                    }
                    if(isVaid)
                    {
                        con.Open();
                        int r = cmd.ExecuteNonQuery();
                        if(r>0)
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Resume Details Updated Sucessfully!";
                            lblMsg.CssClass = "alert alert-success";
                        }
                        else
                        {
                            lblMsg.Visible = true;
                            lblMsg.Text = "Can Not Update the Records, Please Try After Somtimes!";
                            lblMsg.CssClass = "alert alert-danger";
                        }
                    }
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Cannot Update the Records, Please try <b>Relogin</b>!";
                    lblMsg.CssClass = "alert alert-danger";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> Username Already Exist, Try New One..";
                    lblMsg.CssClass = "alert alert-danger";                    
                }
                else
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }  
            catch(Exception ex)
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