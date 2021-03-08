//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Lab3
{
    public partial class CustomerTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            String customerID = Session["SelectedCustomer"].ToString();
            DataTable dt = new DataTable();
            String sqlQuery = "SELECT SERVICE.serviceType, SERVICE.serviceID, serviceTICKET.ticketStatus, serviceTICKET.ticketOpenDate " +
                " FROM CUSTOMER INNER JOIN SERVICE ON CUSTOMER.customerID = SERVICE.customerID INNER JOIN serviceTICKET ON SERVICE.serviceID = serviceTICKET.serviceID" +
                " WHERE CUSTOMER.customerID = " + customerID;
            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            SqlDataAdapter queryResults = new SqlDataAdapter(sqlCommand);
            queryResults.Fill(dt);
            gvCustomerTicket.DataSource = dt;
            gvCustomerTicket.DataBind();
        }

        protected void gvCustomerTicket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvCustomerTicket, "Select$" + e.Row.RowIndex);
            e.Row.Attributes["style"] = "cursor:pointer";
            e.Row.Attributes["onmouseover"] = "this.style.backgroundColor = '#c8e4b6'";
            e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'";
        }

        protected void gvCustomerTicket_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedService"] = gvCustomerTicket.SelectedValue.ToString();
            Response.Redirect("editTicket.aspx");
        }
    }
}