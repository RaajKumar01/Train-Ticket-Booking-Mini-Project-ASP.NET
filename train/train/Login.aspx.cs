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

                errorLoginMessage.Visible = false;

                if (IsPostBack)
                {
                    string ul_emailaddress = Request.Form["ulemail"];
                    string ul_password = Request.Form["ulpass"];

                    if (!string.IsNullOrEmpty(ul_emailaddress) && !string.IsNullOrEmpty(ul_password))
                    {
                        using (OleDbConnection conn = DBHelper.GetConnection())
                        {
                            conn.Open();

                            // Query to check if email and password match in the database
                            string query = "SELECT ID, uname FROM users WHERE uemail = ? AND upassword = ?";
                            OleDbCommand cmd = new OleDbCommand(query, conn);
                            cmd.Parameters.AddWithValue("?", ul_emailaddress);
                            cmd.Parameters.AddWithValue("?", ul_password);

                            OleDbDataReader reader = cmd.ExecuteReader();

                            if (reader.HasRows)
                            {
                                // Credentials are valid; retrieve user information
                                if (reader.Read())
                                {
                                    int userId = reader.GetInt32(0);
                                    string username = reader.GetString(1);

                                    // Store user details in session
                                    Session["UserID"] = userId;
                                    Session["Username"] = username;
                                    Session["Email"] = ul_emailaddress;

                                    // Redirect to the dashboard or homepage
                                     
                                    errorLoginMessage.Visible = false;
                                    Response.Redirect("dashboard.aspx");
                                }
                            }
                            else
                            {
                                // Show an error message if login fails
                                errorLoginMessage.Visible = true;
                                
                            }
                            conn.Close();
                        }
                    }
                    else
                    {
                        // Display an error if inputs are empty
                        errorLoginMessage.Visible = true;
                    }

                }
         }
    }
}