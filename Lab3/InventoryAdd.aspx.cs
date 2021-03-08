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
    public partial class AddInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fillServices();
                
            }
            if (Session["serviceIDInventory"] != null)
            {
                ddlServices.SelectedValue = Session["serviceIDInventory"].ToString();
                Session.Remove("serviceIDInventory");
            }

            updateInventoryOutput();
        }

        // Fills drop down list with services
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
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String itemName = txtItemName.Text;
                Double itemValue = Double.Parse(txtItemValuation.Text);
                int serviceID = Int32.Parse(ddlServices.SelectedValue.ToString());
                String service = ddlServices.SelectedItem.ToString();
                DateTime currentDate = DateTime.Now;


                // Create your Query String

                String sqlQuery = "INSERT INTO INVENTORY VALUES (" + serviceID + ", @itemName, @currentDate, @cost)";

                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Parameters.Add(new SqlParameter("@itemName", itemName));
                sqlCommand.Parameters.Add(new SqlParameter("@currentDate", currentDate));
                sqlCommand.Parameters.Add(new SqlParameter("@cost", itemValue));
                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                sqlCommand.ExecuteReader();
                sqlConnect.Close();

                clearPage();

                outputLbl.Text = HttpUtility.HtmlEncode(itemName) + " was added to the inventory of: " + service;
                updateInventoryOutput();

            }
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            ddlServices.SelectedIndex = 3;
            txtItemName.Text = "Painting";
            txtItemValuation.Text = "500";
            updateInventoryOutput();
        }

        protected void clearPage()
        {
            txtItemName.Text = "";
            txtItemValuation.Text = "";
            outputLbl.Text = "";
            txtCurrentItems.Text = "";

        }

        
        protected void ddlServices_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateInventoryOutput();
        }

        // Shows inventory in current selection
        private void updateInventoryOutput()
        {
            String sqlQuery = "Select itemDescription, itemCost, dateAdded  from INVENTORY  WHERE serviceID = " + ddlServices.SelectedValue.ToString();
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
                output += queryResults["itemDescription"] + ", " + queryResults.GetDecimal(1).ToString("C") + " added on " + queryResults.GetDateTime(2).ToString("d") + "\n";
            }

            sqlConnect.Close();
            if (output.Length != 0)
                txtCurrentItems.Text = output;
            else
                txtCurrentItems.Text = "No Items In Inventory";
        }
    }
}