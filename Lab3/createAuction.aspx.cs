//Kirsi And Josh Coleman 2/15/21
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Lab1
{
    public partial class createAuction : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"] == null)
            {
                Session["InvalidUse"] = "You must first login to create a new Auction.";
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                fillCustomer();
                fillStates();

                if (Session["selectedCustomer"] != null)
                {
                    fillCustomerTable(Session["selectedCustomer"].ToString());
                }
            }
        }


        // Fill dropdown list with states
        protected void fillStates()
        {
            List<String> states = new List<String> { "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY" };
            foreach (String i in states)
            {
                ddlState.Items.Add(new ListItem(i));
            }

        }

        // fill drop down list with current customers
        protected void fillCustomer()
        {
            String sqlQuery = "Select firstName + ' '  + lastName as FullName , customerID from CUSTOMER";
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
                ddlCustomer.Items.Add(new ListItem(queryResults["FullName"].ToString(), queryResults["customerID"].ToString()));
            }
            sqlConnect.Close();
        }

        // start date must be before or the same as end date
        protected void dateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Convert.ToDateTime(txtStartDate.Text) <= Convert.ToDateTime(txtEndDate.Text);
        }

        // check that there is no overlapping auction
        protected void checkNotOverlappingService_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime startDate = DateTime.Parse(txtStartDate.Text);
            DateTime completionDate = DateTime.Parse(txtEndDate.Text);
            int customerID = Int32.Parse(ddlCustomer.SelectedValue.ToString());
            String sqlQuery = "Select * from Service WHERE serviceType='A' AND customerID=" + customerID; ;
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
            args.IsValid = true;
            while (queryResults.Read())
            {
                if (startDate <= DateTime.Parse(queryResults["serviceCompletionDate"].ToString())
                    && completionDate >= DateTime.Parse(queryResults["serviceStartDate"].ToString()))
                {
                    args.IsValid = false;
                }
            }

            sqlConnect.Close();
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String serviceType = "A";
                DateTime startDate = DateTime.Parse(txtStartDate.Text);
                DateTime completionDate = DateTime.Parse(txtEndDate.Text);
                Double cost = double.Parse(txtServiceCost.Text);
                String CustomerName = ddlCustomer.SelectedItem.ToString();
                int customerID = Int32.Parse(ddlCustomer.SelectedValue.ToString());
                String notes = txtNotes.Text;

                String address = txtAddress.Text;
                String city = txtCity.Text;
                String state = ddlState.SelectedValue;
                String zip = txtZipCode.Text;

                String sqlQuery = "INSERT INTO SERVICE VALUES(@serviceType, '', '', @startDate, @completionDate, @cost, @customerID) SELECT CAST(scope_identity() AS int)";
                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Parameters.Add(new SqlParameter("@serviceType", serviceType));
                sqlCommand.Parameters.Add(new SqlParameter("@startDate", startDate.ToString("yyyy-MM-dd")));
                sqlCommand.Parameters.Add(new SqlParameter("@completionDate", completionDate.ToString("yyyy-MM-dd")));
                sqlCommand.Parameters.Add(new SqlParameter("@cost", cost));
                sqlCommand.Parameters.Add(new SqlParameter("@customerID", customerID));
                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                int modified = (int)sqlCommand.ExecuteScalar();


                Console.WriteLine(modified);
                sqlConnect.Close();

                sqlQuery = "INSERT INTO Auction VALUES(@modified, @address, @city, @state, @zip, @notes)";
                sqlQuery += "INSERT INTO SERVICETICKET VALUES("
                    + Session["EmployeeID"].ToString() + ", " + modified + ", '"
                    + DateTime.Now + "', " + 1 + ")";
                sqlCommand.CommandText = sqlQuery;
                sqlCommand.Parameters.Add(new SqlParameter("@modified", modified));
                sqlCommand.Parameters.Add(new SqlParameter("@address", address));
                sqlCommand.Parameters.Add(new SqlParameter("@city", city));
                sqlCommand.Parameters.Add(new SqlParameter("@state", state));
                sqlCommand.Parameters.Add(new SqlParameter("@zip", zip));
                sqlCommand.Parameters.Add(new SqlParameter("@notes", notes));
                sqlConnect.Open();
                SqlDataReader queryResults = sqlCommand.ExecuteReader();
                sqlConnect.Close();
                clearPage();

                outputLbl.Text = "You have scheduled an Auction for " + CustomerName + " on " + startDate.ToString("d");
            }
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            ddlCustomer.SelectedValue = "6";
            txtStartDate.Text = "2021-03-20T08:30";
            txtEndDate.Text = "2021-03-28T11:35";
            txtServiceCost.Text = "300";
            txtAddress.Text = "300 Auction Lane";
            txtCity.Text = "Harrisonburg";
            ddlState.SelectedIndex = 50;
            txtZipCode.Text = "22801";
            txtNotes.Text = "High valued customer auction";
        }

       protected void clearPage()
        {
            ddlCustomer.SelectedIndex = -1;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtServiceCost.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            ddlState.SelectedIndex = -1;
            txtZipCode.Text = "";
            txtNotes.Text = "";
            outputLbl.Text = "";
        }

        private void fillCustomerTable(String id)
        {
            String sqlQuery = "Select * from serviceRequest inner join Person ON serviceRequest.UserID = Person.UserID  where Person.UserID = " + id;

            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            String serviceRequestId = "";
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            String username = "";
            SqlDataReader queryResults = sqlCommand.ExecuteReader();
            while (queryResults.Read())
            {
                username = queryResults["username"].ToString();
                DateTime start = DateTime.Parse(queryResults["serviceDeadlineStart"].ToString());
                DateTime end = DateTime.Parse(queryResults["serviceDeadlineEnd"].ToString());
                txtStartDate.Text = start.ToString("yyyy-MM-ddTHH:mm");
                txtEndDate.Text = end.ToString("yyyy-MM-ddTHH:mm");
                serviceRequestId = queryResults["serviceRequestID"].ToString();
            }
            sqlConnect.Close();

            sqlConnect.Open();
            sqlQuery = "UPDATE serviceRequest SET requestStatus = 0 where serviceRequestID =" + serviceRequestId;
            sqlCommand.CommandText = sqlQuery;
            queryResults = sqlCommand.ExecuteReader();

            sqlConnect.Close();
            sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);

            sqlQuery = "Select CustomerID from customer where email = '" + username + "'";
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;

            sqlConnect.Open();
            queryResults = sqlCommand.ExecuteReader();
            while (queryResults.Read())
            {
                ddlCustomer.SelectedValue = queryResults["customerID"].ToString();
            }
            sqlConnect.Close();
        }
    }

}