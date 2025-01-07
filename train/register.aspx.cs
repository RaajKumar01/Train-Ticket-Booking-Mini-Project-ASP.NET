using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Windows.Forms;

using System.Data.OleDb;
using train.App_Code;


namespace train
{
    public partial class register : System.Web.UI.Page
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
                string u_username = Request.Form["urname"];
                string u_password = Request.Form["urpass"];
                string emailaddress = Request.Form["uremail"];

                     
                using (OleDbConnection conn = DBHelper.GetConnection())
                {
                    conn.Open();

                    // Step 1: Check if email exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE uemail = ?";
                    OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("?", emailaddress);

                    int emailCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (emailCount > 0)
                    {
                        // Email already exists
                        string divId = errorRegisterMessage.ClientID;
                        string notificationMessage = "Email already exists!";

                        ClientScript.RegisterStartupScript(
                          this.GetType(),
                          "ShowNotification",
                          "showNotification('" + divId + "', '" + notificationMessage + "', " + false.ToString().ToLower() + ");",
                          true
                        );
                         
                    }
                    else
                    {
                        // Step 2: Insert the new record
                        string insertQuery = "INSERT INTO users (uname, uemail, upassword, uadmin) VALUES (?, ?, ?, ?)";
                        OleDbCommand insertCmd = new OleDbCommand(insertQuery, conn);

                        insertCmd.Parameters.AddWithValue("?", u_username);
                        insertCmd.Parameters.AddWithValue("?", emailaddress);
                        insertCmd.Parameters.AddWithValue("?", u_password);
                        insertCmd.Parameters.AddWithValue("?", 0);
                         

                        insertCmd.ExecuteNonQuery();

                        // Step 3: Get the inserted ID
                        insertCmd.CommandText = "SELECT @@IDENTITY";
                        int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());

                        // Store user details in session
                        Session["UserID"] = insertedId;
                        Session["Username"] = u_username;
                        Session["IsAdmin"] = 0;

                        errorRegisterMessage.Visible = false;
                        Response.Redirect("dashboard.aspx");
                        
                    }
                    conn.Close();
                }
            }
        }
    }
}