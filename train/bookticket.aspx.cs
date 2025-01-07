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
 

namespace train
{
    public partial class bookticket : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // reset BookingData
                Session["BookingData"] = null;


                // disable past days selectable in date of travel. 
                trainSearchDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");

                // Set the maximum selectable date to 3 months from now
                trainSearchDate.Attributes["max"] = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");


                trainListSection.Visible = false; // hide the gridview
                 
            }

        }

        protected void SubmitSearchBtn_Click(object sender, EventArgs e) 
        {
                 string divId = TrainBookMessage.ClientID;
             
                if (string.IsNullOrWhiteSpace(trainSearchSource.Value) ||
                       string.IsNullOrWhiteSpace(trainSearchdestination.Value) ||
                       string.IsNullOrWhiteSpace(trainSearchDate.Value))
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

                string source = trainSearchSource.Value.ToLower();
                string destination = trainSearchdestination.Value.ToLower();

                if (source == destination)
                {
                   string notificationMessage = "Source and Destination cannot be same.";

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

                    // Query to check if email and password match in the database
                    string query = @"
                                    SELECT 
                                        trainNo, 
                                        trainName, 
                                        trainType, 
                                        trainSource, 
                                        trainDestination, 
                                        trainACSeats, 
                                        trainSleeperClassSeats,
                                        trainSecondClassSeats, 
                                        trainACPrice, 
                                        trainSleeperClassPrice, 
                                        trainSecondClassPrice, 
                                        trainStarttime, 
                                        trainEndTime
                                    FROM trainlist
                                    WHERE trainSource = ? AND trainDestination = ?";

                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    cmd.Parameters.AddWithValue("?", source);
                    cmd.Parameters.AddWithValue("?", destination);

                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader); // Load data directly into DataTable

                        availHeader.InnerText = "Available Trains on: " + DateTime.Parse(trainSearchDate.Value).ToString("dd/MM/yyyy") + " ";
                        
                        trainListSection.Visible = true;
                        trainListGridView.DataSource = dt; // Bind DataTable to GridView
                        trainListGridView.DataBind();
                    }
                    else
                    {

                            string notificationMessage = "No trains are available for this Source and Destination";

                            ClientScript.RegisterStartupScript(
                                this.GetType(),
                                "ShowNotification",
                                "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                                true
                            );
                         
                    }

                }

         }

   
        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectTrain")
            {
                TrainBookMessage.Visible = false; // hide any mesage notofication box. 

                // Retrieve the row index of the command
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Get the train name from the row
                GridViewRow row = trainListGridView.Rows[rowIndex];

                // Collect booking details
                var bookingData = new
                {
                    BookerName = Session["Username"] != null ? Session["Username"].ToString() : "Invalid User",
                    BookedTrainNo = Convert.ToInt32(row.Cells[0].Text),
                    BookedTrainName =  row.Cells[1].Text,
                    BookedTrainType = row.Cells[2].Text,
                    TrainSource = row.Cells[3].Text,
                    TrainDestination = row.Cells[4].Text,
                    BookingDate = DateTime.Now.ToString("yyyy-MM-dd"), 
                    TravelDate = trainSearchDate.Value,  
                    ACPrice = Convert.ToDecimal(row.Cells[5].Text),
                    SleeperPrice = Convert.ToDecimal(row.Cells[6].Text),
                    SecondPrice = Convert.ToDecimal(row.Cells[7].Text)
     
                };
                Session["BookingData"] = bookingData;
                Response.Redirect("confirmbooking.aspx");
            }
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