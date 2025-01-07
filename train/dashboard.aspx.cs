using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Windows.Forms;

 
using System.Data;
using System.Data.OleDb;
using train.App_Code;


namespace train
{
    public partial class dashboard : System.Web.UI.Page
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
                LoadBookings();
            }

        }

        private void LoadBookings()
        {
            noticketsmessage.Visible = false; // hide the no tickets message

            using (OleDbConnection conn = DBHelper.GetConnection())
            {
                        conn.Open();

                        string query = @"
                            SELECT 
                                b.BookID,
                                b.BookedTrainNo,
                                t.trainName,  
                                b.BookedSeatType, 
                                t.trainSource, 
                                t.trainDestination,
                                b.BookedDate
                            FROM bookedlist b
                            INNER JOIN trainlist t 
                                ON b.BookedTrainNo = t.trainNo
                            WHERE b.BookerID = ?";

                        OleDbCommand cmd = new OleDbCommand(query, conn);

                        // Add the parameter value from the session
                        cmd.Parameters.AddWithValue("?", Convert.ToInt32(Session["UserID"]) );

                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);

                                // Bind data to GridView
                                trainListGridView.DataSource = dt;
                                trainListGridView.DataBind();
                                 
                            }
                            else
                            {
                                noticketsmessage.Visible = true;
                            }
                        }
            }
        }
         
        

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "ShowTicket")
            {
                // Retrieve the row index of the command
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Get the train name from the row
                GridViewRow row = trainListGridView.Rows[rowIndex];

                int ticketId = Convert.ToInt32(row.Cells[0].Text);  

                // Store the TicketID in the session
                Session["TicketID"] = ticketId;

                // Redirect to showticket.aspx
                Response.Redirect("showticket.aspx");
            }
            else if (e.CommandName == "CancelBooking")
            {
                // Retrieve the row index of the command
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Get the train name from the row
                GridViewRow row = trainListGridView.Rows[rowIndex];

                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    string query = "DELETE FROM bookedlist WHERE BookID = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    // Add parameter as integer
                    cmd.Parameters.AddWithValue("?", Convert.ToInt32(row.Cells[0].Text));

                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    // Provide feedback (optional)
                    if (rowsAffected > 0)
                    {
                        noticketsmessage.InnerHtml = "Ticket cancelled successfully!<br/>Your Refund will be processed within 7 business days.";
                        noticketsmessage.Style["color"] = "green"; 
                        noticketsmessage.Visible = true;
                    } 
                }
                // Inject JavaScript to redirect after 3 seconds
                string script = @"   setTimeout(   function() { 
                                                   window.location.href='dashboard.aspx'; 
                                                   }, 3000 );   ";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
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