//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Lab2
{
    public partial class editTicket : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["InvalidUse"] = "You must first login to view a ticket.";
                Response.Redirect("LoginPage.aspx");
            }

            if (IsPostBack)
            {
                setCurrent();

            }
        }


        protected void gvNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvNotes, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor = '#c8e4b6'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'";
            }
        }

        protected void gvNotes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ticketID"] = gvNotes.SelectedRow.Cells[0].Text;
            Response.Redirect("noteDetails.aspx");
        }

        protected void btnNewNote_Click(object sender, EventArgs e)
        {
            Session["serviceIDNote"] = ddlServices.SelectedValue;
            Response.Redirect("createNotes.aspx");
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            String sqlQuery = "INSERT INTO TICKETHOLDER VALUES('"
                + DateTime.Now + "', "
                + ddlEmployee.SelectedValue + ", "
                + ddlServices.SelectedValue + ", @assignNote )";
            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Parameters.Add(new SqlParameter("@assignNote", txtAssignTicket.Text));
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            SqlDataReader queryResults = sqlCommand.ExecuteReader();
            sqlConnect.Close();

            gvHolder.DataBind();
            txtAssignTicket.Text = "";
            ddlEmployee.SelectedIndex = -1;
            setCurrent();
        }

        private void setCurrent()
        {
            int selected = Int32.Parse(ddlServices.SelectedValue);

            String sqlQuery = "SELECT EMPLOYEE.firstName + ' ' + EMPLOYEE.lastName as Name, TICKETHOLDER.creationDate " +
                " FROM EMPLOYEE INNER JOIN TICKETHOLDER ON EMPLOYEE.employeeID = TICKETHOLDER.employeeID " +
                " Where TICKETHOLDER.serviceTicketID = " + selected +
                " AND TICKETHOLDER.creationDate = (select max(creationDate) from TICKETHOLDER where serviceTicketID = " + selected + ")";


            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            SqlDataReader queryResults = sqlCommand.ExecuteReader();
            String employee = "";
            while (queryResults.Read())
            {
                employee = queryResults["Name"].ToString();
            }
            sqlConnect.Close();
            lblCurrent.Text = "Current Ticket Holder: " + HttpUtility.HtmlEncode(employee);
        }

        protected void ddlServices_DataBound(object sender, EventArgs e)
        {
            if (Session["selectedService"] != null)
            {
                ddlServices.SelectedValue = Session["selectedService"].ToString();
                Session.Remove("selectedService");
            }
            setCurrent();
        }

        protected void btnAddInventory_Click(object sender, EventArgs e)
        {
            Session["serviceIDInventory"] = ddlServices.SelectedValue;
            Response.Redirect("InventoryAdd.aspx");

        }

        protected void btnAuctionInventory_Click(object sender, EventArgs e)
        {
            Session["serviceIDInventory"] = ddlServices.SelectedValue;
            Response.Redirect("InventoryAuction.aspx");
        }
    }
}