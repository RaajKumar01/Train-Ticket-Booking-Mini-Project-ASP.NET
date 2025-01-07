<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="train.Login" %>

<!DOCTYPE html>

<html >
<head   runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

        <script type="text/javascript" src="script.js"> </script>

</head>
<body>
        <div class="content-wrapper">


    <div class="navbar">
        <div class="title">Trains</div>
        <div class="menu">
                    <a href="dashboard.aspx">Home</a>
                    <a href="login.aspx">About</a> <!-- rediect to same page -->
                    <a href="admin.aspx">Admin</a>
                    <a href="bookticket.aspx">Book Ticket</a>
        </div>
    </div>

 

            <div class="lr-main-content">
                <h1>Account Login</h1>
 
                <div class="notification-message"  id="errorLoginMessage"  runat="server" > Invalid credentials </div>
                <div class="auth-box">
                    <form id="loginform" runat="server" method="post" onsubmit="return validateInputs()">
                        <label for="ulemail"><b>E-mail:</b></label>
                        <input type="email" placeholder="Enter E-mail Address" name="ulemail" required />
                        <label for="ulpass"><b>Password:</b></label>
                        <input type="password" placeholder="Enter Password" name="ulpass" required />
                        <button id="loginSubmit" runat="server" type="submit">Login</button>
                        <p style="margin-bottom: 0;">First time booking? <a href="Register.aspx" style="color: teal; font-weight: bold;">Register here</a></p>
                    </form>
                </div>
            </div>
             

   </div>

    <footer>
        &copy; 2025 Online Train Ticket Booking. All rights reserved.
    </footer>


</body>
</html>
