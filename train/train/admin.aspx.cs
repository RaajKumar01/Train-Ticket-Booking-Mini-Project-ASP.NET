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
                TrainEntryMessage.Visible = false;

            }

            protected void SubmitBtn_Click(object sender, EventArgs e)
            {

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
                        // Show error message if any field is empty
                        TrainEntryMessage.InnerText = "All fields are required. Please fill out every field.";
                        TrainEntryMessage.Visible = true;
                        return;
                    }

                    // Hide the error message if all fields are filled
                    TrainEntryMessage.Visible = false;

                    using (OleDbConnection conn = DBHelper.GetConnection())
                    {
                        try
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

                            TrainEntryMessage.InnerText = "Train details added successfully!";
                            TrainEntryMessage.Style["color"] = "green";
                            TrainEntryMessage.Visible = true;

                            // hide the message after 3 seconds
                            ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + TrainEntryMessage.ClientID + "').style.display = 'none'; }, 3000);", true);

                            ClearFormFields();
                        }
                        catch (Exception ex)
                        {
                            // Show error message in case of any exception
                            TrainEntryMessage.InnerText = "Error: " + ex.Message;
                            TrainEntryMessage.Style["color"] = "red";
                            TrainEntryMessage.Visible = true;
                        }
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
    }
}