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

namespace Lab3
{
    public partial class UserRequestService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void dateValidation_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (String.IsNullOrEmpty(txtEndDate.Text))
                args.IsValid = true;
            else
                args.IsValid = Convert.ToDateTime(txtStartDate.Text) <= Convert.ToDateTime(txtEndDate.Text);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String sqlQuery = "INSERT INTO serviceRequest (UserID, dateRequested, serviceType, serviceDeadlineStart, serviceDeadlineEnd, notes, requestStatus) " +
                    "Values (@UserID, @dateRequested, @serviceType, @serviceDeadlineStart, @serviceDeadlineEnd, @notes, 1)";

                // Define the connection to the Database:
                SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["AUTH"].ConnectionString);
                // Create the SQL Command object which will send the query:
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Parameters.Add(new SqlParameter("@UserID", Session["CustomerID"].ToString()));
                sqlCommand.Parameters.Add(new SqlParameter("@dateRequested", DateTime.Now));
                sqlCommand.Parameters.Add(new SqlParameter("@serviceType", ddlServiceType.SelectedValue));
                sqlCommand.Parameters.Add(new SqlParameter("@serviceDeadlineStart", DateTime.Parse(txtStartDate.Text).ToString("MM/dd/yyyy HH:mm:ss")));
                sqlCommand.Parameters.Add(new SqlParameter("@serviceDeadlineEnd", DateTime.Parse(txtEndDate.Text).ToString("MM/dd/yyyy HH:mm:ss")));
                sqlCommand.Parameters.Add(new SqlParameter("@notes", txtNoteBody.Text));

                sqlCommand.Connection = sqlConnect;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText = sqlQuery;
                // Open your connection, send the query, retrieve the results:
                sqlConnect.Open();
                SqlDataReader queryResults = sqlCommand.ExecuteReader();
                lblStatus.Text = ddlServiceType.SelectedItem + " " + " request has been submitted.";
                ddlServiceType.SelectedIndex = -1;
                txtStartDate.Text = "";
                txtEndDate.Text = "";
                txtNoteBody.Text = "";
            }
        }
    }
}