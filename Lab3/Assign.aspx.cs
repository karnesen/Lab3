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

namespace Lab1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillServices();
                fillEmployees();
                fillEquipment();
            }
        }

        // fills drop down list with services
        private void fillServices()
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

        // fills dropdown list with current employees
        private void fillEmployees()
        {
            String sqlQuery = "Select firstName + ' ' + lastName as fullName, employeeID from Employee";
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
            while (queryResults.Read())
            { 
                ddlEmployees.Items.Add(new ListItem(queryResults["fullName"].ToString(), queryResults["employeeID"].ToString()));
            }
            sqlConnect.Close();
        }

        // fills drop down list with current equipment
        private void fillEquipment()
        {
            String sqlQuery = "Select * from Equipment";
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
            while (queryResults.Read())
            {
                ddlEquipment.Items.Add(new ListItem(queryResults["equipmentType"].ToString(), queryResults["equipmentID"].ToString()));
            }
            sqlConnect.Close();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            // Insert equipment assignment
            if (RadioButtonList1.SelectedValue == "1")
            {
                if (Page.IsValid)
                {
                    int equipmentID = Int32.Parse(ddlEquipment.SelectedValue.ToString());
                    String equipmentName = ddlEquipment.SelectedItem.ToString();
                    String service = ddlServices.SelectedItem.ToString();
                    int serviceID = Int32.Parse(ddlServices.SelectedValue.ToString());
                    String sqlQuery = "INSERT INTO utilizeEquipment VALUES( @equipmentID, @serviceID, @Notes)";
                    

                    // Define the connection to the Database:
                    SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                    // Create the SQL Command object which will send the query:
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Parameters.Add(new SqlParameter("equipmentID", equipmentID));
                    sqlCommand.Parameters.Add(new SqlParameter("serviceID", serviceID));
                    sqlCommand.Parameters.Add(new SqlParameter("Notes", txtNotes.Text));
                    sqlCommand.Connection = sqlConnect;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlQuery;
                    // Open your connection, send the query, retrieve the results:
                    sqlConnect.Open();
                    sqlCommand.ExecuteReader();
                    sqlConnect.Close();

                    clearPage();

                    outputLbl.Text = HttpUtility.HtmlEncode(equipmentName) + " was assigned to: " + service;

                }
            }

            else if (RadioButtonList1.SelectedIndex < 0)
                outputLbl.Text = "Please select an item to assign";

            // Insert Employee Assignment
            else
            {
                if (Page.IsValid)
                {
                    String employeeID = ddlEmployees.SelectedValue.ToString();
                    String employeeName = ddlEmployees.SelectedItem.ToString();
                    String serviceID = ddlServices.SelectedValue.ToString();
                    String service = ddlServices.SelectedItem.ToString();
                    DateTime startDate = DateTime.Parse(txtStartDate.Text);
                    String sqlQuery = "INSERT INTO ASSIGNMENT VALUES(@employeeID, @serviceID, @startDate, @notes)";

                    // Define the connection to the Database:
                    SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                    // Create the SQL Command object which will send the query:
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Parameters.Add(new SqlParameter("@employeeID", employeeID));
                    sqlCommand.Parameters.Add(new SqlParameter("@serviceID", serviceID));
                    sqlCommand.Parameters.Add(new SqlParameter("@startDate", startDate));
                    sqlCommand.Parameters.Add(new SqlParameter("@notes", txtNotes.Text));
                    sqlCommand.Connection = sqlConnect;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = sqlQuery;
                    // Open your connection, send the query, retrieve the results:
                    sqlConnect.Open();
                    sqlCommand.ExecuteReader();
                    sqlConnect.Close();

                    clearPage();

                    outputLbl.Text = HttpUtility.HtmlEncode(employeeName) + " was assigned to: " + service;

                }
            }
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            ddlServices.SelectedIndex = 3;
            RadioButtonList1.SelectedIndex = 0;
            ddlEquipment.Visible = true;
            lblEquipment.Visible = true;
            ddlEmployees.Visible = false;
            lblEmployees.Visible = false;
            lblStartDate.Visible = false;
            txtStartDate.Visible = false;
            rfvStartDate.Enabled = false;

            txtNotes.Visible = true;
            lblNotes.Visible = true;
            lblNotes.Text = "Notes";
            ddlEquipment.SelectedIndex = 3;
            txtNotes.Text = "Will need to be refueld";

        }

        // Shows appropriate controls for assigning employee or equipment
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(RadioButtonList1.SelectedValue.ToString() == "1")
            {
                ddlEquipment.Visible = true;
                lblEquipment.Visible = true;
                ddlEmployees.Visible = false;
                lblEmployees.Visible = false;
                lblStartDate.Visible = false;
                txtStartDate.Visible = false;
                rfvStartDate.Enabled = false;

                txtNotes.Visible = true;
                lblNotes.Visible = true;
                lblNotes.Text = "Notes";
                outputLbl.Text = "";
            }
            else
            {
                ddlEquipment.Visible = false;
                lblEquipment.Visible = false;
                ddlEmployees.Visible = true;
                lblEmployees.Visible = true;
                lblStartDate.Visible = true;
                txtStartDate.Visible = true;
                rfvStartDate.Enabled = true;

                txtNotes.Visible = true;
                lblNotes.Visible = true;
                lblNotes.Text = "Employee Role";
                outputLbl.Text = "";
            }
        }

        private void clearPage()
        {
            ddlServices.SelectedIndex = -1;
            RadioButtonList1.SelectedIndex = -1;
            ddlEquipment.SelectedIndex = -1;
            ddlEmployees.SelectedIndex = -1;
            ddlEmployees.Visible = false;
            lblEmployees.Visible = false;
            ddlEquipment.Visible = false;
            lblEquipment.Visible = false;
            lblStartDate.Visible = false;
            txtStartDate.Visible = false;
            txtNotes.Visible = false;
            lblNotes.Visible = false;
            txtNotes.Text = "";
        }
    }
}