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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
    
                if (Session["Username"] != null)
                {
                    Response.Redirect("dashboard.aspx");
                    return;
                }

                 

                if (IsPostBack)
                {
                    string ul_emailaddress = Request.Form["ulemail"];
                    string ul_password = Request.Form["ulpass"];

                    
                        using (OleDbConnection conn = DBHelper.GetConnection())
                        {
                            conn.Open();

                            // Query to check if email and password match in the database
                            string query = "SELECT ID, uname, uadmin FROM users WHERE uemail = ? AND upassword = ?";
                            OleDbCommand cmd = new OleDbCommand(query, conn);
                            cmd.Parameters.AddWithValue("?", ul_emailaddress);
                            cmd.Parameters.AddWithValue("?", ul_password);

                            OleDbDataReader reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Credentials are valid; retrieve user information
                                if (reader.Read())
                                {

                                    // Store user details in session 
                                    Session["UserID"] = reader.GetInt32(0);
                                    Session["Username"] = reader.GetString(1);
                                    Session["IsAdmin"] = reader.GetInt32(2);

                                    // Redirect to the dashboard  
                                     
                                     
                                    Response.Redirect("dashboard.aspx");
                                }
                            }
                            else
                            {
                                 
                                string divId = errorLoginMessage.ClientID; 
                                string notificationMessage = "Invalid credential"; 

                                ClientScript.RegisterStartupScript(
                                  this.GetType(),
                                  "ShowNotification",
                                  "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                                  true
                                );

                            }
                            conn.Close();
                        }
                   
                 }
         }
    }
}