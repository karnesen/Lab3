//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab3
{
    public partial class CustomerMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CustomerUsername"] == null)
            {
                btnToLogin.Visible = true;
                btnToLogout.Visible = false;
                lblMessage.Text = "Login for more!";
            }
            else
            {
                btnToLogin.Visible = false;
                btnToLogout.Visible = true;
                lblMessage.Text = "Welcome " + Session["CustomerUsername"].ToString();
            }
        }

        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserLogin.aspx");
        }

        protected void btnToLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("UserLogin.aspx?loggedout=true");
        }
    }
}