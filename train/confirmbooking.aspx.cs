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
    public partial class confirmbooking : System.Web.UI.Page
    {

        // Declare a protected global variable accessable within the page
        protected decimal SeatACPrice;
        protected decimal SeatSleeperPrice;
        protected decimal SeatSecondPrice;
        protected int SelectedTrainNo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

            // Always ensure session data is available
            if (Session["BookingData"] == null)
            {
                // Redirect back to the booking page if session data is missing
                Response.Redirect("bookticket.aspx");
                return;
            }

            var bookingData = (dynamic)Session["BookingData"];

            // Retrieve booking data from session
            if (!IsPostBack)
            {

                  

                   //readonly data 
                   CBbookerName.Value =  bookingData.BookerName; 
                   CBbookedTrainName.Value =   bookingData.BookedTrainName;
                   CBbookedTrainType.Value =    bookingData.BookedTrainType; 
                   CBbookingDate.Value =    bookingData.BookingDate;
                   CBtravelDate.Value =   bookingData.TravelDate;
                   CBbookedTrainSource.Value = bookingData.TrainSource;
                   CBbookedTrainDestination.Value = bookingData.TrainDestination; 

                // default AC Prce at first since it's the first select option
                    CBticketsTotal.Value = 1.ToString();
                    CBticketsAmount.Value = bookingData.ACPrice.ToString();

                    //assign value for global that are not included in form on pre-load of page
                    SeatACPrice = bookingData.ACPrice;
                    SeatSleeperPrice = bookingData.SleeperPrice;
                    SeatSecondPrice = bookingData.SecondPrice;
                    SelectedTrainNo = bookingData.BookedTrainNo;
            }
            else {
                
                // after clicking Pay and Buy Now button. Set Value for train since it's a postback.
                SeatACPrice = bookingData.ACPrice;
                SeatSleeperPrice = bookingData.SleeperPrice;
                SeatSecondPrice = bookingData.SecondPrice;
                SelectedTrainNo = bookingData.BookedTrainNo;
            } 
            
 
        }

        protected void BookingConfirmBtn_Click(object sender, EventArgs e)
        {


            string divId = TrainBookConfirmMessage.ClientID;
            if (string.IsNullOrWhiteSpace(CBbookingDetails.Value) ||
                string.IsNullOrWhiteSpace(CBexpiryMonth.Value) ||
                string.IsNullOrWhiteSpace(CBexpiryYear.Value) ||
                string.IsNullOrWhiteSpace(CBcardNumber.Value) ||
                string.IsNullOrWhiteSpace(CBbookingDetails.Value) ||
                string.IsNullOrWhiteSpace(CBcardcvv.Value))
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

                int expiryMonth = Convert.ToInt32(CBexpiryMonth.Value);
                int expiryYear = Convert.ToInt32(CBexpiryYear.Value);

                if (expiryMonth < 1 || expiryMonth > 12)
                {
                    string notificationMessage = "Please enter a valid month between 01 and 12 for Expiry Month";

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "ShowNotification",
                        "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                        true
                    );
                    return;
                }

                if (expiryYear < 25 || expiryYear > 99)
                {
                    string notificationMessage = "Please enter a valid year between 25 and 99 for Expiry Year";

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "ShowNotification",
                        "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                        true
                    );
                    return;
                }

                if (CBcardNumber.Value.Length != 16 || CBcardcvv.Value.Length != 3)
                {
 
                    string notificationMessage = "Please enter valid information for payment details.";

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "ShowNotification",
                        "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                        true
                    );
                    return;
                }   


                if (Convert.ToInt32(CBticketsTotal.Value) < 1 || Convert.ToInt32(CBticketsTotal.Value) > 6) 
                {
 
                    string notificationMessage = "Invalid Entry for Tickets. Please enter value from 1 to 6.";

                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "ShowNotification",
                        "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                        true
                    );
                    return;
                }
             
 


                // Parse to DateTime for safety (optional but recommended) to store in DB. 
                DateTime bookingDate = DateTime.Parse(CBbookingDate.Value);
                DateTime travelDate = DateTime.Parse(CBtravelDate.Value);
                
                 
                
                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                     
                        conn.Open();

                        string query = "INSERT INTO bookedlist (BookerID, BookedTrainNo, BookingDate, BookedDate, BookingDetails, PaymentTranscation, BookerPayMethod, BookedSeatType, BookedTotalSeats, BookedTotalAmount) " +
                                       "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        OleDbCommand cmd = new OleDbCommand(query, conn);
                      
                        // Adding parameters with the respective values
                        cmd.Parameters.AddWithValue("?", Convert.ToInt32(Session["UserID"]));
                        cmd.Parameters.AddWithValue("?", Convert.ToInt32(SelectedTrainNo));  
                        cmd.Parameters.AddWithValue("?", bookingDate); // Booking Date
                        cmd.Parameters.AddWithValue("?", travelDate);  // Travel Date as BookedDate
                        cmd.Parameters.AddWithValue("?", CBbookingDetails.Value); // Booking Details
                        cmd.Parameters.AddWithValue("?", new Random().Next(100000, 999999)); // Payment Transaction (Dummy value)
                        cmd.Parameters.AddWithValue("?", CBpaymentMethod.Value); // Payment Method
                        cmd.Parameters.AddWithValue("?", CBbookingSeat.Value); // Booked Train Seat
                        cmd.Parameters.AddWithValue("?", int.Parse(CBticketsTotal.Value)); // Booked Total Seats
                        cmd.Parameters.AddWithValue("?", decimal.Parse(CBticketsAmount.Value)); // Total Amount (Currency)

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        // reset the bookingData
                        Session["BookingData"] = null;

                        string notificationMessage = "Booking successfully completed!<br />Your payment has been processed.<br />Redirecting to Homepage...";
                        
                        // change IsSucess to true to display in green text
                        ClientScript.RegisterStartupScript(
                            this.GetType(),
                            "ShowNotification",
                            "showNotification('" + divId + "', '" + notificationMessage + "', " + true.ToString().ToLower() + ");",
                            true
                        );



                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function () { window.location.href = 'dashboard.aspx'; }, 4000);", true);
                    
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