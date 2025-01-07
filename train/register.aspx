<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="train.register" %>

<!DOCTYPE html>

<html  >
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Register - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

        <script type="text/javascript" src="script.js" > </script>

</head> 
<body>

      <div class="content-wrapper"> 

       <div class="navbar">
        <div class="title">Trains</div>
        <div class="menu">
                    <a href="dashboard.aspx">Home</a>
                    <a href="register.aspx">About</a> <!-- rediect to same page -->
                    <a href="admin.aspx">Admin</a>
                    <a href="bookticket.aspx">Book Ticket</a>
        </div>
    </div>

  


            <div class="lr-main-content">
                <h1>Register Account</h1>
                   <div class="notification-message"  id="errorRegisterMessage"  runat="server" > Invalid credentials </div>
                   <div class="auth-box">
                    <form id="registerform" runat="server" method="post" onsubmit="return validateRegisterInputs()">
                        <label for="urname"><b>Username:</b></label>
                        <input type="text" placeholder="Enter Your Full Name" name="urname" required />
                        <label for="uremail"><b>E-mail:</b></label>
                        <input type="email" placeholder="Enter E-mail Address" name="uremail" required />
                        <label for="urpass"><b>Password:</b></label>
                        <input type="password" placeholder="Enter Password" name="urpass" required />
                        <button id="registerSubmit" runat="server" type="submit">Register</button>
                        <p style="margin-bottom: 0;">Already have an account? <a href="Login.aspx" style="color: teal; font-weight: bold;">Log In</a></p>
                    </form>
                </div>
            </div>

   </div>

    <footer>
        &copy; 2025 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>
