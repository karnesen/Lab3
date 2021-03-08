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
    public partial class NewAuction : System.Web.UI.Page
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
                fillStates();
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

        // start date must be before or the same as end date
        protected void dateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = Convert.ToDateTime(txtStartDate.Text) <= Convert.ToDateTime(txtEndDate.Text);
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearPage();
        }

        protected void btnPopulate_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = "2021-03-20T08:30";
            txtEndDate.Text = "2021-03-28T11:35";
            txtAddress.Text = "300 Auction Lane";
            txtCity.Text = "Harrisonburg";
            ddlState.SelectedIndex = 50;
            txtZipCode.Text = "22801";
            txtNotes.Text = "Auction for lawn equipment.";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String auction = txtNotes.Text;
                String sqlQuery = "INSERT INTO AUCTIONEVENT VALUES(@address, @city, @state, @zip, @datestart, @dateend, @notes)";
                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Parameters.Add(new SqlParameter("@address", txtAddress.Text));
                sqlCommand.Parameters.Add(new SqlParameter("@city", txtCity.Text));
                sqlCommand.Parameters.Add(new SqlParameter("@state", ddlState.SelectedValue));
                sqlCommand.Parameters.Add(new SqlParameter("@zip", txtZipCode.Text));
                sqlCommand.Parameters.Add(new SqlParameter("@datestart", DateTime.Parse(txtStartDate.Text)));
                sqlCommand.Parameters.Add(new SqlParameter("@dateend", DateTime.Parse(txtEndDate.Text)));
                sqlCommand.Parameters.Add(new SqlParameter("@notes", txtNotes.Text));
                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                SqlDataReader queryResults = sqlCommand.ExecuteReader();
                sqlConnect.Close();
                clearPage();
                outputLbl.Text = HttpUtility.HtmlEncode(auction) + " auction was scheduled";
            }
        }

        protected void clearPage()
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            ddlState.SelectedIndex = -1;
            txtZipCode.Text = "";
            txtNotes.Text = "";
            outputLbl.Text = "";
        }
    }
}