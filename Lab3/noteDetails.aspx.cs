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
    public partial class noteDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String sqlQuery = "Select noteTitle, noteText from TICKETNOTE WHERE ticketID = " + Session["ticketID"].ToString();

            // Define the connection to the Database:
            SqlConnection sqlConnect = new SqlConnection(WebConfigurationManager.ConnectionStrings["Connect"].ConnectionString);
            // Create the SQL Command object which will send the query:
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnect;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = sqlQuery;
            // Open your connection, send the query, retrieve the results:
            sqlConnect.Open();
            SqlDataReader queryResults =  sqlCommand.ExecuteReader();
            while (queryResults.Read())
            {
                txtNoteTitle.Text = HttpUtility.HtmlEncode(queryResults["noteTitle"].ToString());
                txtNoteBody.Text = HttpUtility.HtmlEncode(queryResults["noteText"].ToString());
            }
            // Close all related connections
            queryResults.Close();
            sqlConnect.Close();
        }
    }
}