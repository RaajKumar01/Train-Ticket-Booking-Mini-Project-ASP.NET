using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.OleDb;
using train.App_Code;


namespace train
{
    public partial class admin : System.Web.UI.Page
    {
        
            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["Username"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }

                if (Session["IsAdmin"] == null || Convert.ToInt32(Session["IsAdmin"]) == 0)
                {
                    // Inject both the alert and the redirect into the page
                    string script = "alert('You do not have privileges to access this page.'); window.location='dashboard.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script, true);
                    return;
                }

               

            }

            protected void SubmitBtn_Click(object sender, EventArgs e)
            {
                 string divId = TrainEntryMessage.ClientID;

                // optional code not necessary as "required" is added in input tags.
                    if ( string.IsNullOrWhiteSpace(trainName.Value) ||
                    string.IsNullOrWhiteSpace(trainType.Value) ||
                    string.IsNullOrWhiteSpace(acSeats.Value) ||
                    string.IsNullOrWhiteSpace(acPrice.Value) ||
                    string.IsNullOrWhiteSpace(sleeperSeats.Value) ||
                    string.IsNullOrWhiteSpace(sleeperPrice.Value) ||
                    string.IsNullOrWhiteSpace(secondClassSeats.Value) ||
                    string.IsNullOrWhiteSpace(secondClassPrice.Value) ||
                    string.IsNullOrWhiteSpace(trainSource.Value) ||
                    string.IsNullOrWhiteSpace(trainDestination.Value) ||
                    string.IsNullOrWhiteSpace(startTime.Value) ||
                    string.IsNullOrWhiteSpace(trainDetails.Value) ||
                    string.IsNullOrWhiteSpace(endTime.Value)  )
                    {

                        string notificationMessage = "All fields are required. Please fill out every field.";

                        ClientScript.RegisterStartupScript(
                            this.GetType(),
                            "ShowNotification",
                            "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                            true
                        );
                        return;
                    } 

                    using (OleDbConnection conn = DBHelper.GetConnection())
                    {
                         conn.Open();

                            string query = "INSERT INTO trainlist (trainName, trainType, trainACSeats, trainSleeperClassSeats, trainSecondClassSeats, " +
                                           "trainSource, trainDestination, trainStarttime, trainEndTime, trainACPrice, trainSleeperClassPrice, trainSecondClassPrice, trainDetails) " +
                                           "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                            OleDbCommand cmd = new OleDbCommand(query, conn);

                            cmd.Parameters.AddWithValue("?", trainName.Value);
                            cmd.Parameters.AddWithValue("?", trainType.Value);
                            cmd.Parameters.AddWithValue("?", int.Parse(acSeats.Value));
                            cmd.Parameters.AddWithValue("?", int.Parse(sleeperSeats.Value));
                            cmd.Parameters.AddWithValue("?", int.Parse(secondClassSeats.Value));
                            cmd.Parameters.AddWithValue("?", trainSource.Value);
                            cmd.Parameters.AddWithValue("?", trainDestination.Value);
                            cmd.Parameters.AddWithValue("?", startTime.Value);
                            cmd.Parameters.AddWithValue("?", endTime.Value);
                            cmd.Parameters.AddWithValue("?", decimal.Parse(acPrice.Value));
                            cmd.Parameters.AddWithValue("?", decimal.Parse(sleeperPrice.Value));
                            cmd.Parameters.AddWithValue("?", decimal.Parse(secondClassPrice.Value));
                            cmd.Parameters.AddWithValue("?", trainDetails.Value);
                            


                            cmd.ExecuteNonQuery();

                            conn.Close();

                            string notificationMessage = "Train details added successfully!";
                            
                             // green text sucess message 
                            ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "ShowNotification",
                                "showNotification('" + divId + "', '" + notificationMessage + "', " + true.ToString().ToLower() + ");",
                                true
                            );
                         
                            ClearFormFields(); 
 
                    }
            }
            private void ClearFormFields()
            {
                trainName.Value = string.Empty;
                trainType.Value = string.Empty;
                acSeats.Value = string.Empty;
                acPrice.Value = string.Empty;
                sleeperSeats.Value = string.Empty;
                sleeperPrice.Value = string.Empty;
                secondClassSeats.Value = string.Empty;
                secondClassPrice.Value = string.Empty;
                trainSource.Value = string.Empty;
                trainDestination.Value = string.Empty;
                startTime.Value = string.Empty;
                endTime.Value = string.Empty;
                trainDetails.Value = string.Empty;
            }
            protected void LogoutBtn_Click(object sender, EventArgs e)
            {
                // Clear the session and redirect to login
                Session.Clear();
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
    }
}