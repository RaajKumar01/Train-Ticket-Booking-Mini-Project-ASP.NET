using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.OleDb;
using train.App_Code;
using System.Text;
using System.Web.UI.HtmlControls;

namespace train
{
    public partial class bookticket : System.Web.UI.Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            { 

                // disable past days selectable in date of travel. 
                trainSearchDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

                // Hide /unhide the booking form 
                TrainBookMessage.Visible = false; // notofication message hide

                trainListSection.Visible = false;  // hide gridview
                BookingTrainForm.Visible = false; // hide Train Seat selecton div

                searchSection.Visible = true; // unhide search
 
 
            }

        }

        protected void SubmitSearchBtn_Click(object sender, EventArgs e) 
        {
             
                if (string.IsNullOrWhiteSpace(trainSearchSource.Value) ||
                       string.IsNullOrWhiteSpace(trainSearchdestination.Value) ||
                       string.IsNullOrWhiteSpace(trainSearchDate.Value))
                {
                    // Show error message if any field is empty
                    TrainBookMessage.InnerText = "All fields are required. Please fill out every field.";
                    TrainBookMessage.Visible = true;
                    return;
                }

                string source = trainSearchSource.Value.ToLower();
                string destination = trainSearchdestination.Value.ToLower();

                if (source == destination)
                {
                    TrainBookMessage.InnerText = "Source and Destination cannot be same.";
                    TrainBookMessage.Visible = true;
                    return;
                }

                TrainBookMessage.Visible = false;

                 

                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Query to check if email and password match in the database
                    string query = "SELECT * FROM trainlist WHERE trainSource = ? AND trainDestination = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    cmd.Parameters.AddWithValue("?", source);
                    cmd.Parameters.AddWithValue("?", destination);

                    OleDbDataReader reader = cmd.ExecuteReader();

                    StringBuilder tableRows = new StringBuilder();

                    if (reader.HasRows)
                    {
                        List<Train> trainList = new List<Train>(); // from public class Train
                        
                        while (reader.Read())
                        {
                            Train train = new Train(); // create a instance object for Train class to store data. 

                            if (reader["trainNo"] != DBNull.Value)
                                train.TrainNo = Convert.ToInt32(reader["trainNo"]);

                            if (reader["trainName"] != DBNull.Value)
                                train.TrainName = reader["trainName"].ToString();

                            if (reader["trainType"] != DBNull.Value)
                                train.TrainType = reader["trainType"].ToString();

                            if (reader["trainSource"] != DBNull.Value)
                                train.TrainSource = reader["trainSource"].ToString();

                            if (reader["trainDestination"] != DBNull.Value)
                                train.TrainDestination = reader["trainDestination"].ToString();

                            trainList.Add(train); // Move this line inside the loop
                        } 
                        trainListSection.Visible = true;
                        trainListGridView.DataSource = trainList; // Bind the trainList to the GridView
                        trainListGridView.DataBind();
                    }
                    else
                    {
 
                        TrainBookMessage.InnerText = "No trains are available for the this Source and Destination";
                        TrainBookMessage.Visible = true;

                        // hide the message after 3 seconds
                        ClientScript.RegisterStartupScript(this.GetType(), "HideMessage", "setTimeout(function() { document.getElementById('" + TrainBookMessage.ClientID + "').style.display = 'none'; }, 3000);", true);

                     }

                }

         }

   
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectTrain")
            {

                // disable search box
                searchSection.Visible = false; // unhide search


                // Retrieve the row index of the command
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Get the train name from the row
                GridViewRow row = trainListGridView.Rows[rowIndex];
                int trainID = Convert.ToInt32(row.Cells[0].Text); // Assuming Train Name is in the second column
                string trainName = row.Cells[1].Text; // Assuming Train Name is in the second column

                 
                searchSection.Disabled = true; // disable inputs for train search form
 

                trainListSection.Visible = false;  // hide gridview
                BookingTrainForm.Visible = true; // unhide Train Seat selecton div
                 

                // process to show booking train form after selecting Train seat in Griedview
                TFBookerName.Value = Session["Username"] != null ? Session["Username"].ToString() : "Invalid User";
                TFTrainName.Value = trainName;
                TFTrainNO.Value = trainID.ToString();

                string query = "SELECT trainACSeats,trainSleeperClassSeats,trainSecondClassSeats,trainACPrice,trainSleeperClassPrice,trainSecondClassPrice FROM trainList WHERE trainNo = ?";

                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", trainID); // Replace with your dynamic train number

                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Dynamically add options based on seat availability and prices
                        if (reader["trainACSeats"] != DBNull.Value && Convert.ToInt32(reader["trainACSeats"]) > 0)
                        {
                            string acPrice = reader["trainACPrice"] != DBNull.Value
                                             ? Decimal.Parse(reader["trainACPrice"].ToString()).ToString("C")
                                             : "N/A";
                            TFSeatTypeASP.Items.Add(new ListItem("AC - " + acPrice, "AC"));
                        }
                        if (reader["trainSleeperClassSeats"] != DBNull.Value && Convert.ToInt32(reader["trainSleeperClassSeats"]) > 0)
                        {
                            string sleeperPrice = reader["trainSleeperClassPrice"] != DBNull.Value
                                                  ? Decimal.Parse(reader["trainSleeperClassPrice"].ToString()).ToString("C")
                                                  : "N/A";
                            TFSeatTypeASP.Items.Add(new ListItem("Sleeper Class - " + sleeperPrice, "Sleeper Class"));
                        }
                        if (reader["trainSecondClassSeats"] != DBNull.Value && Convert.ToInt32(reader["trainSecondClassSeats"]) > 0)
                        {
                            string secondClassPrice = reader["trainSecondClassPrice"] != DBNull.Value
                                                      ? Decimal.Parse(reader["trainSecondClassPrice"].ToString()).ToString("C")
                                                      : "N/A";
                            TFSeatTypeASP.Items.Add(new ListItem("Second Class - " + secondClassPrice, "Second Class"));
                        }
                    }
                    reader.Close();
                }
            }
        }


        public void ProceedPaymentBtn_Click(object sender, EventArgs e)
        {

            int ticketCount;
            if (!int.TryParse(TFtickets.Value, out ticketCount) || ticketCount < 1 || ticketCount > 20)
            {
                TrainBookMessage.InnerText = "Please enter a valid ticket count between 1 and 20.";
                TrainBookMessage.Visible = true;
                return;
            }


            trainListSection.Visible = false; // Hide gridview
            BookingTrainForm.Visible = true; // Unhide Train Seat selection div


            // Get the selected item text (e.g., "Second Class - $50")
            string selectedText = TFSeatTypeASP.SelectedItem.Text;

            // Extract the seat type and price from the selected text
            string[] parts = selectedText.Split('-'); // Splits into ["Second Class ", " $50"]
            string seatType = parts[0].Trim();       // "Second Class"
            string price = parts.Length > 1 ? parts[1].Trim() : "N/A"; // "$50"
 


            // Retrieve values
            string bookerNameValue = Session["Username"] != null ? Session["Username"].ToString() : "Invalid User";
            string bookedTrainTypeValue = TFTrainName.Value;
            string bookingDateValue = DateTime.Now.ToString("yyyy-MM-dd");
            string travelDateValue = trainSearchDate.Value;
            string bookingtotaltickets = TFtickets.Value;
            string BookingTrainNo = TFTrainNO.Value;


            // Redirect with POST to confirm page for booking for limitted data.
            RedirectWithHtmlForm("confirmbooking.aspx", new Dictionary<string, string>
            {
                { "BookedTrainNo", BookingTrainNo },
                { "BookerName", bookerNameValue },
                { "BookedTrainType", bookedTrainTypeValue },
                { "BookingSeatType", seatType },
                { "BookingDate", bookingDateValue },
                { "TravelDate", travelDateValue },
                { "TotalTickets", bookingtotaltickets },
                { "TicketPrice", price }

            });
             
        }

        // Helper method to generate an HTML form for redirection
        private void RedirectWithHtmlForm(string url, Dictionary<string, string> postData)
        {
            // Begin the form
            string form = "<form id='redirectForm' action='" + url + "' method='post'>";

            // Add hidden input fields
            foreach (var key in postData.Keys)
            {
                form += "<input type='hidden' name='" + key + "' value='" + postData[key] + "' />";
          
            }

            // End the form and add script
            form += "</form>";
            form += "<script> alert('Form is about to be submitted'); document.getElementById('redirectForm').submit(); </script>";

            // Write the form to the response
            Response.Clear();
            Response.Write(form);
            Response.End();
        }


        public class Train
        {
           /* get: Retrieves the value of the property.
             set: Assigns a value to the property.*/
            public int TrainNo { get; set; }
            public string TrainName { get; set; }
            public string TrainType { get; set; }
            public string TrainSource { get; set; }
            public string TrainDestination { get; set; }
        }

        /* This Train class serves as a data model for holding information about trains in a structured manner
        Train train = new Train
        {
         *  No = "1"
            Name = "Express Train",
            Type = "Fast",
            Source = "Chennai",
            Destination = "Madurai"
        };
        List<Train> trainList = new List<Train>();

        Train train1 = new Train { No = 1 ,Name = "Express Train", Type = "Fast", Source = "Banglore", Destination = "Delhi" };
        Train train2 = new Train { No= 2 , Name = "Local Train", Type = "Slow", Source = "Chennai", Destination = "Trichy" };
        */
    }
}