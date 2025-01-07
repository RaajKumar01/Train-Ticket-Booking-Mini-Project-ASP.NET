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

            errorRegisterMessage.Visible = false;


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
                        errorRegisterMessage.Visible = true;
                         
                    }
                    else
                    {
                        // Step 2: Insert the new record
                        string insertQuery = "INSERT INTO users (uname, uemail, upassword) VALUES (?, ?, ?)";
                        OleDbCommand insertCmd = new OleDbCommand(insertQuery, conn);

                        insertCmd.Parameters.AddWithValue("?", u_username);
                        insertCmd.Parameters.AddWithValue("?", emailaddress);
                        insertCmd.Parameters.AddWithValue("?", u_password);
                         

                        insertCmd.ExecuteNonQuery();

                        // Step 3: Get the inserted ID
                        insertCmd.CommandText = "SELECT @@IDENTITY";
                        int insertedId = Convert.ToInt32(insertCmd.ExecuteScalar());

                        // Store user details in session
                        Session["UserID"] = insertedId;
                        Session["Username"] = u_username;
                        Session["Email"] = emailaddress;


                        errorRegisterMessage.Visible = false;
                        Response.Redirect("dashboard.aspx");
                        
                    }
                    conn.Close();
                }
            }
        }
    }
}