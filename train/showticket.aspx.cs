using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using train.App_Code;
using System.Data.OleDb;

namespace train
{
    public partial class showticket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Username"] == null)
            {
                Response.Redirect("login.aspx");
                return;
            }

         // Check if TicketID exists in the session
            if (Session["TicketID"] == null || Convert.ToInt32(Session["TicketID"]) == 0)
            {
                
                Response.Redirect("dashboard.aspx");

            }
            else
            {
                
                int ticketId = Convert.ToInt32(Session["TicketID"]);

                GetTicketDetails(ticketId);
                

            } 
        }
        private void GetTicketDetails(int ticketId)
        {



            Session["TicketID"] = null;

            using (OleDbConnection conn = DBHelper.GetConnection())
            {
                conn.Open();
                //  Selects all columns from the bookedlist table
                string query = @"
                            SELECT 
                                b.*, 
                                t.*
                            FROM bookedlist b
                            INNER JOIN trainlist t 
                                ON b.BookedTrainNo = t.trainNo
                            WHERE b.BookID = ?";

                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("?", ticketId);

                 
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                        lblbookingno.Text = reader["BookID"].ToString();
                        lblbooker.Text = Session["Username"].ToString(); // session name
                        lblPaymentNo.Text = reader["PaymentTranscation"].ToString();
                        lblStartTime.Text = Convert.ToDateTime(reader["trainStarttime"]).ToString("HH:mm");
                        lblEndTime.Text = Convert.ToDateTime(reader["trainEndtime"]).ToString("HH:mm");
                        lblBookedOn.Text = Convert.ToDateTime(reader["BookingDate"]).ToString("dd/MM/yyyy");
                        lblTravelDate.Text = Convert.ToDateTime(reader["BookedDate"]).ToString("dd/MM/yyyy");
                        lblBookingDetails.Text = reader["BookingDetails"].ToString();
                        lblTrainNo.Text = reader["trainNo"].ToString();
                        lblTrainName.Text = reader["trainName"].ToString();
                        lblSeatType.Text = reader["BookedSeatType"].ToString();
                        lblSeatsBooked.Text = reader["BookedTotalSeats"].ToString();
                        lblTrainType.Text = reader["trainType"].ToString();
                        lblPaymentMethod.Text = reader["BookerPayMethod"].ToString();
                        lblTrainSource.Text = reader["trainSource"].ToString();
                        lblAmountPaid.Text = Convert.ToDecimal(reader["BookedTotalAmount"]).ToString("C");
                        lblTrainDestination.Text = reader["trainDestination"].ToString();
                        lblTrainDetails.Text = reader["trainDetails"].ToString();
                } 
                conn.Close(); 
            }
        }
    }
}