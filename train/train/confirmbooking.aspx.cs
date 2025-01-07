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
        protected void Page_Load(object sender, EventArgs e)
        {


            Console.Write("Test");
            if (IsPostBack)
            {
                Console.Write("Test1");
                TrainBookConfirmMessage.Visible = false;
                // Retrieve values from Request.Form from booktexti
                string BookedTrain = Request.Form["BookedTrainNo"];
                string bookerName = Request.Form["BookerName"];
                string bookedTrainType = Request.Form["BookedTrainType"];
                string bookingSeatType = Request.Form["BookingSeatType"];
                string bookingDateValue = Request.Form["BookingDate"];
                string travelDateValue = Request.Form["TravelDate"];
                
                int totalTickets = Convert.ToInt32(Request.Form["TotalTickets"]);
                int ticketPrice = Convert.ToInt32(Request.Form["TicketPrice"]);
                decimal totalAmount = ticketPrice * totalTickets;


                // Convert to 'yyyy-MM-dd' format for input fields to display
                CBbookingDate.Value = bookingDateValue;
                CBtravelDate.Value = travelDateValue;

                CBbookedTrainNO.Value = BookedTrain;
                CBbookerName.Value = bookerName;
                CBbookedTrainType.Value = bookedTrainType;
                CBbookingSeatType.Value = bookingSeatType;
                CBticketsTotal.Value = totalTickets.ToString();
                CBticketsAmount.Value = totalAmount.ToString("C");

            }
            else
            {
                     Console.Write("Tes21");
               
            }


        }

        protected void BookingConfirmBtn_Click(object sender, EventArgs e)
        {

        
            if (string.IsNullOrWhiteSpace(CBbookingDetails.Value) ||
                string.IsNullOrWhiteSpace(CBcardHolder.Value))
                {
                    // Show error message if any field is empty
                    TrainBookConfirmMessage.InnerText = "All fields are required. Please fill out every field.";
                    TrainBookConfirmMessage.Visible = true;
                    return;
                }

                int expiryMonth = Convert.ToInt32(CBexpiryMonth.Value);
                int expiryYear = Convert.ToInt32(CBexpiryYear.Value);

                if (expiryMonth < 1 || expiryMonth > 12)
                {
                    TrainBookConfirmMessage.InnerText = "Please enter a valid month between 01 and 12 for Expiry Month";
                    TrainBookConfirmMessage.Visible = true;
                    return;
                }

                if (expiryYear < 25 || expiryYear > 99)
                {
                    TrainBookConfirmMessage.InnerText = "Please enter a valid year between 25 and 99 for Expiry Year";
                    TrainBookConfirmMessage.Visible = true;
                    return;
                }

 
                string creditCardInput = CBcardNumber.Value.Replace("-", "");
                
                // Optionally validate the length
                if (creditCardInput.Length != 16 || CBcardcvv.Value.Length != 3)
                {
                    TrainBookConfirmMessage.InnerText = "Please enter valid information for payment details.";
                    TrainBookConfirmMessage.Visible = true;
                    return;
                   
                }
             
                // Hide the error message if all fields are filled
                TrainBookConfirmMessage.Visible = false;


                // Parse to DateTime for safety (optional but recommended) to store in DB. 
                DateTime bookingDate = DateTime.Parse(CBbookingDate.Value);
                DateTime travelDate = DateTime.Parse(CBtravelDate.Value);
                
                decimal totalAmountfinal = Convert.ToDecimal(CBticketsAmount.Value);
                
                
                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO bookedlist (BookerName, BookedTrainNo, BookingDate, BookedDate, BookingDetails, PaymentTranscation, BookerPayMethod, BookedTrainType, BookedTotalSeats, BookedTotalAmount) " +
                                       "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        OleDbCommand cmd = new OleDbCommand(query, conn);

                        // Adding parameters with the respective values
                        cmd.Parameters.AddWithValue("?", CBbookerName.Value);
                        cmd.Parameters.AddWithValue("?", int.Parse(CBbookedTrainNO.Value));  
                        cmd.Parameters.AddWithValue("?", bookingDate); // Booking Date
                        cmd.Parameters.AddWithValue("?", travelDate);  // Travel Date as BookedDate
                        cmd.Parameters.AddWithValue("?", CBbookingDetails.Value); // Booking Details
                        cmd.Parameters.AddWithValue("?", new Random().Next(100000, 999999)); // Payment Transaction (Dummy value)
                        cmd.Parameters.AddWithValue("?", CBpaymentMethod.Value); // Payment Method
                        cmd.Parameters.AddWithValue("?", CBbookedTrainType.Value); // Booked Train Type
                        cmd.Parameters.AddWithValue("?", int.Parse(CBticketsTotal.Value)); // Booked Total Seats
                        cmd.Parameters.AddWithValue("?", totalAmountfinal); // Total Amount (Currency)

                        cmd.ExecuteNonQuery();

                        conn.Close();

                        TrainBookConfirmMessage.InnerText = "Booking successfully completed!<br />Your payment has been processed.<br />Your can download your ticket in 'My Acccount'";
                        TrainBookConfirmMessage.Style["color"] = "green";
                        TrainBookConfirmMessage.Visible = true;

                       
                    }
                    catch (Exception ex)
                    {
                        TrainBookConfirmMessage.InnerText = "Error: " + ex.Message;
                        TrainBookConfirmMessage.Style["color"] = "red";
                        TrainBookConfirmMessage.Visible = true;
                    }
                }

        }
    }
}