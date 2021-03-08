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
    public partial class createNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["InvalidUse"] = "You must first login to create a new note.";
                Response.Redirect("LoginPage.aspx");
            }

          

            if (!Page.IsPostBack)
            {
                fillServices();
            }

            if (Session["serviceIDNote"] != null)
            {
                ddlServices.SelectedValue = Session["serviceIDNote"].ToString();
                Session.Remove("serviceIDNote");
            }
        }

        protected void btnCreateNote_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                String noteName = txtNoteTitle.Text;
                String sqlQuery = "INSERT INTO TICKETNOTE VALUES('" +
                    DateTime.Now +
                    "', " + ddlServices.SelectedValue +
                    ", " + Session["EmployeeID"].ToString() +
                    ", @noteName, @noteText)";


                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Parameters.Add(new SqlParameter("@noteName", txtNoteTitle.Text));
                sqlCommand.Parameters.Add(new SqlParameter("@noteText", txtNoteBody.Text));
                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                sqlCommand.ExecuteReader();
                sqlConnect.Close();
                lblStatus.Text = HttpUtility.HtmlEncode(noteName) + " has been saved!";
            }
        }

        protected void fillServices()
        {
            String sqlQuery = "Select Service.serviceType, Service.serviceStartDate, Service.serviceID, Customer.firstName, Customer.lastName" +
                " from CUSTOMER INNER JOIN Service on CUSTOMER.customerID = Service.customerID ";
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
            String output = "";
            while (queryResults.Read())
            {
                if (queryResults["serviceType"].ToString() == "A")
                    output = "Auction - ";
                else
                    output = "Move - ";
                output += queryResults["firstName"] + " " + queryResults["lastName"];
                ddlServices.Items.Add(new ListItem(output, queryResults["serviceID"].ToString()));
            }
            sqlConnect.Close();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNoteTitle.Text = "";
            txtNoteBody.Text = "";
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            txtNoteTitle.Text = "This is a test Note";
            txtNoteBody.Text = "This is the body of a test note! This body has lots of notes about this test note.";
        }
    }
}