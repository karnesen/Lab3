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


namespace Lab2
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["InvalidUse"] = "You must first login to view employee home.";
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                GetData();
            }
        }

        private void GetData()
        {
            DataTable dt = new DataTable();
            String sqlQuery = "SELECT serviceTICKET.serviceTicketID, TICKETHOLDER.note as Note, TICKETHOLDER.creationDate as 'Assigned On:'" +
                "FROM serviceTICKET INNER JOIN TICKETHOLDER ON serviceTICKET.serviceTicketID = TICKETHOLDER.serviceTicketID " +
                "WHERE TICKETHOLDER.creationDate = (select max(creationDate) from TICKETHOLDER where serviceTicketID = serviceTICKET.serviceTicketID) " +
                "AND TICKETHOLDER.employeeID = " + Session["EmployeeID"];
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
            gvWork.DataSource = dt;
            gvWork.DataBind();
        }

        protected void gvWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvWork, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor = '#c8e4b6'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'";
            }
        }

        protected void gvWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["selectedService"] = gvWork.SelectedValue.ToString();
            Response.Redirect("editTicket.aspx", false);
        }

        protected void gvNewCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvNewCustomers, "Select$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
                e.Row.Attributes["onmouseover"] = "this.style.backgroundColor = '#c8e4b6'";
                e.Row.Attributes["onmouseout"] = "this.style.backgroundColor='white'";
            }
        }

        protected void gvNewCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvNewCustomers.SelectedValue != null)
            {
                int selectedCustomer = Int32.Parse(gvNewCustomers.SelectedValue.ToString());
                Session["selectedCustomer"] = selectedCustomer;
                String sqlQuery = "SELECT new from Person where UserID= " + selectedCustomer;
                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                SqlDataReader queryResults = sqlCommand.ExecuteReader();
                while (queryResults.Read())
                { 
                    if((bool)queryResults["new"])
                    {
                        Response.Redirect("createCustomer.aspx", false);
                    }
                    else
                    {
                        if(gvNewCustomers.SelectedRow.Cells[1].Text == "M")
                        {
                            Response.Redirect("createMove.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("createAuction.aspx", false);
                        }
                    }
                }
                
            }
        }
    }
}