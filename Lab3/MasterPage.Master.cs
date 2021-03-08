using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lab1
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                btnToLogin.Visible = true;
                btnToLogout.Visible = false;
                lblMessage.Text = "Login for more!";
            }
            else
            {
                btnToLogin.Visible = false;
                btnToLogout.Visible = true;
                lblMessage.Text = "Welcome " + Session["username"].ToString();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void btnToLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("LoginPage.aspx?loggedout=true");
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["InvalidUse"] = "You must first login to search customers.";
                Response.Redirect("LoginPage.aspx");
            }
            Session["search"] = txtSearchCustomer.Text;
            Response.Redirect("searchResults.aspx");
        }
    }
}