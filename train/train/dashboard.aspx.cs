using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Windows.Forms;

using System.Text;
using System.Data;
using System.Data.OleDb;
using train.App_Code;


namespace train
{
    public partial class dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*if (Session["Username"] == null)
            {
                Response.Redirect("dashboard.aspx");
                return;
            }*/


            if (!IsPostBack)
            {
                LoadTickets();
                 
            }

        }

        private void LoadTickets()
        {
            using (OleDbConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                string query = @"
                                SELECT 
                                    b.BookID,
                                    b.BookedTrainType, 
                                    b.BookedTrainNo, 
                                    t.trainName, 
                                    t.trainSource, 
                                    t.trainDestination
                                FROM bookedlist b
                                INNER JOIN trainlist t ON b.BookedTrainType = t.trainType 
                                                   AND b.BookedTrainNo = t.trainNo";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                StringBuilder html = new StringBuilder();

                if (dt.Rows.Count > 0)
                {
                    // Start the form and table
                    html.Append("<form method='post' runat = 'server' action='dashboard.aspx'>");
                    html.Append("<table>");
                    html.Append("<thead>");
                    html.Append("<tr>");
                    html.Append("<th>Train Name</th>");
                    html.Append("<th>Type</th>");
                    html.Append("<th>Source</th>");
                    html.Append("<th>Destination</th>");
                    html.Append("<th>Actions</th>");
                    html.Append("</tr>");
                    html.Append("</thead>");
                    html.Append("<tbody>");

                    // Loop through data and create rows
                    foreach (DataRow row in dt.Rows)
                    {
                        string ticketId = row["BookID"].ToString();


                        // Main row (clickable to expand)
                        html.Append("<tr>");
                        html.AppendFormat("<td><span class='expand-icon' onclick='toggleDetails(\"{0}\")'>+</span> {1}</td>", ticketId, row["trainName"]);
                        html.AppendFormat("<td>{0}</td>", row["trainType"]);
                        html.AppendFormat("<td>{0}</td>", row["trainSource"]);
                        html.AppendFormat("<td>{0}</td>", row["trainDestination"]);
                        html.AppendFormat("<td><button type='submit' name='cancel' value='{0}' class='cancel-btn'>Cancel</button></td>", ticketId);
                        html.Append("</tr>");

                        // Hidden details row
                        html.Append("<tr class='details-row' id='details-" + ticketId + "'>");
                        html.Append("<td colspan='5'><strong>Details:</strong><br>");
                        html.AppendFormat("Ticket ID: {0}<br>", ticketId);
                        html.AppendFormat("Additional Info: More details about the train can go here.<br>");
                        html.Append("</td>");
                        html.Append("</tr>");
                    }

                    html.Append("</tbody>");
                    html.Append("</table>");
                    html.Append("</form>");
                }
                else
                {
                    html.Append("<p class='no-tickets'>No tickets available. Click below to book your first ticket!</p>");
                }

                // Inject the HTML into the div
                TicketsTableContainer.InnerHtml = html.ToString();
                conn.Close();
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            // Check if cancel button was clicked
            if (IsPostBack && Request.Form["cancel"] != null)
            {
                string ticketId = Request.Form["cancel"];
                CancelTicket(ticketId);               
            }
        }

        private void CancelTicket(string ticketId)
        {
            using (OleDbConnection conn = DBHelper.GetConnection())
            {
                conn.Open();

                // Convert ticketId to integer
                int ticketIdInt = int.Parse(ticketId);

                string query = "DELETE FROM bookedlist WHERE BookID = ?";
                OleDbCommand cmd = new OleDbCommand(query, conn);

                // Add parameter as integer
                cmd.Parameters.AddWithValue("?", ticketIdInt);

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                // Provide feedback (optional)
                if (rowsAffected > 0)
                {
                    Response.Write("<script>alert('Ticket cancelled successfully!');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Failed to cancel ticket.');</script>");
                }

                // Reload the tickets
                LoadTickets();
            }
        }

        protected void LogoutBtn_Click(object sender, EventArgs e)
        {
            // Clear the session and redirect to login
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Home.aspx");
        }
    }
}