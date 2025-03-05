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
    public partial class JobListing : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        public int jobCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        private void showJobList()
        {
            if (dt == null)
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                string query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
            }
            DataList1.DataSource = dt;
            DataList1.DataBind();
            lbljobCount.Text = JobCount(dt.Rows.Count);
        }

        string JobCount(int count)
        {
            if(count > 1)
            {
                return "Total <b>"+ count +"</b> Jobs Found" ;
            }
            else if(count==1)
            {
                return "Total <b>" + count + "</b> Job Found";
            }
            else
            {
                return "No Job Found";
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue != "0")
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                string query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs WHERE Country = '" + ddlCountry.SelectedValue + "'";
                cmd = new SqlCommand(query, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange(); 
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        protected string GetImageUrl(Object url)
        {
            string url1 = "";

            if(string.IsNullOrEmpty(url.ToString()) || url == DBNull.Value)
            {
                url1 = "~/Images/No_image.jpg";
            }
            else
            {
                url1 = string.Format("~/{0}", url);
            }
            return ResolveUrl(url1);
        }

        protected static string RelativeDate(DateTime theDate)
        {
            Dictionary<long, string> thresholds = new Dictionary<long, string>();
            int minute = 60;
            int hour = 60 * minute;
            int day = 24 * hour;
            thresholds.Add(60, "{0} seconds ago");
            thresholds.Add(minute * 2, "a minute ago");
            thresholds.Add(45 * minute, "{0} minute ago");
            thresholds.Add(120 * minute, "an hour ago");
            thresholds.Add(day, "{0} hour ago");
            thresholds.Add(day * 2, "Yesterday");
            thresholds.Add(day * 30, "{0} day ago");
            thresholds.Add(day * 365, "{0} months ago");
            thresholds.Add(long.MaxValue, "{0} years ago");
            long since = (DateTime.Now.Ticks - theDate.Ticks) / 10000000;
            foreach(long threshold in thresholds.Keys)
            {
                if(since < threshold)
                {
                    TimeSpan t = new TimeSpan((DateTime.Now.Ticks - theDate.Ticks));
                    return string.Format(thresholds[threshold], (t.Days > 365 ? t.Days / 365 : (t.Days > 0 ? t.Days : (t.Hours > 0 ? t.Hours : (t.Minutes > 0 ? t.Minutes : (t.Seconds > 0 ? t.Seconds : 0)))))).ToString();
                }
            }
            return "";

        }

        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string jobType = string.Empty;
            jobType = selectedCheckBox();
            if (jobType!="")
            {
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                string query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs WHERE JobType IN ("+jobType+")";
                cmd = new SqlCommand(query,con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
            }
        }

        string selectedCheckBox()
        {
            string jobType = string.Empty;
            for(int i=0;i<CheckBoxList1.Items.Count;i++)
            {
                if(CheckBoxList1.Items[i].Selected)
                {
                    jobType += "'" + CheckBoxList1.Items[i].Text + "',"; //Full Time,Remote,
                }
            }
            return jobType = jobType.TrimEnd(',');
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonList1.SelectedValue !="0")
            {
                string postedDate = string.Empty;
                postedDate = selectedRadioButton();
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");
                string query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs WHERE Convert(DATE,CreateDate)" + postedDate + " ";
                cmd = new SqlCommand(query,con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            else
            {
                showJobList();
                RBSelectedColorChange();
            }
        }

        string selectedRadioButton()
        {
            string postedDate = string.Empty;
            DateTime date = DateTime.Today;
            if(RadioButtonList1.SelectedValue == "1")
            {
                postedDate = "= Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if(RadioButtonList1.SelectedValue == "2")
            {
                postedDate = " between Convert(DATE,'" + DateTime.Now.AddDays(-2).ToString("yyyy/MM/dd") + "') and Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if(RadioButtonList1.SelectedValue == "3")
            {
                postedDate = " between Convert(DATE,'" + DateTime.Now.AddDays(-3).ToString("yyyy/MM/dd") + "') and Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else if(RadioButtonList1.SelectedValue == "4")
            {
                postedDate = " between Convert(DATE,'" + DateTime.Now.AddDays(-5).ToString("yyyy/MM/dd") + "') and Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            else
            {
                postedDate = " between Convert(DATE,'" + DateTime.Now.AddDays(-10).ToString("yyyy/MM/dd") + "') and Convert(DATE,'" + date.ToString("yyyy/MM/dd") + "')";
            }
            return postedDate;
        }

        protected void lbFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCondition = false;
                string subquery = string.Empty;
                string jobType = string.Empty;
                string postedDate = string.Empty;
                string addAnd = string.Empty;
                string query = string.Empty;
                List<string> queryList = new List<string>();
                con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Project\NextGenItJobs\NextGenItJobs\App_Data\NextGenItJobs.mdf;Integrated Security=True");

                if(ddlCountry.SelectedValue != "0")
                {
                    queryList.Add("Country = '" + ddlCountry.SelectedValue + "'");
                    isCondition = true;
                }
                jobType = selectedCheckBox();

                if(jobType!="")
                {
                    queryList.Add("JobType IN (" + jobType + ")");
                    isCondition = true;
                }
                if(RadioButtonList1.SelectedValue != "0")
                {
                    postedDate = selectedRadioButton();
                    queryList.Add(" Convert(DATE, CreateDate)"+postedDate);
                    isCondition = true;
                }
                if(isCondition)
                {
                    foreach(string a in queryList)
                    {
                        subquery += a + "and";
                    }
                    subquery = subquery.Remove(subquery.LastIndexOf("and"), 3);
                    query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs WHERE " + subquery + " ";
                }
                else
                {
                    query = @"SELECT JobId,Title,Salary,JobType,CompanyName,CompanyImage,Country,State,CreateDate FROM Jobs";
                }
                SqlDataAdapter sda = new SqlDataAdapter(query,con);
                dt = new DataTable();
                sda.Fill(dt);
                showJobList();
                RBSelectedColorChange();
            }
            catch(Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"');</script>");
            }
            finally
            {
                con.Close();
            }
        }

        protected void lbReset_Click(object sender, EventArgs e)
        {
            ddlCountry.ClearSelection();
            CheckBoxList1.ClearSelection();
            RadioButtonList1.SelectedValue = "0";
            RBSelectedColorChange();
            showJobList();
        }

        void RBSelectedColorChange()
        {
            if(RadioButtonList1.SelectedItem.Selected == true)
            {
                RadioButtonList1.SelectedItem.Attributes.Add("class", "selectedradio");
            }
        }
    }
}