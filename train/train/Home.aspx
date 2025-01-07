<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="train.Home" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Homepage - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

</head>
<body>
    
   <div class="content-wrapper">
        
         <div class="navbar">
            <div class="title">Trains</div>
            <div class="menu">
                <a href="Home.aspx">Home</a>
                <a href="#">About</a>
                <a href="#">Admin</a>
                <a href="#">Book Ticket</a>
            </div>
        </div>                                              
    
        <div class="main-content">
            <h1>Welcome To Train Ticket Booking</h1>
            <h3>Book Your Train Tickets Online Effortlessly<br>Your Journey Starts Here</h3>
        </div>

         <div class="home-button">
                <a href="login.aspx">Login</a>
                <a href="register.aspx">Register</a>
         </div>
    </div>

    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>
