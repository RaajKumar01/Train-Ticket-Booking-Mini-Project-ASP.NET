<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="train.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head   runat="server">
    <title>Login - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

        <script>
            function validateInputs() {
                var loginuseremail = document.querySelector('input[name="ulemail"]').value;
                var loginpassword = document.querySelector('input[name="ulpass"]').value;
                var loginemailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

                if (loginuseremail.trim() === "") {
                    alert("Please enter your email address.");
                    return false; // Prevent form submission
                }

                if (!loginemailRegex.test(loginuseremail)) {
                    alert("Please enter a valid email address.");

                    return false; // Prevent form submission
                }

                if (loginpassword.trim() === "") {
                    alert("Please enter your password.");
                    return false; // Prevent form submission
                }
                else if (loginpassword.length < 6 || loginpassword.length > 12) {
                    alert("Password must be between 6 and 12 characters.");

                    return false; // Prevent form submission
                }

                return true; // Proceed with form submission
            } 
    </script>

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

 

    <div class="lr-main-content">
        <h1>Account Login</h1>
        <div id="errorLoginMessage"  runat="server"  style="color:red; font-weight: bold; text-shadow: 1px 1px #000000;">These credentials do not match our records<br /></div>
         <div class="login-box"> 

             <form id="loginform" runat="server"  method="post"  onsubmit="return validateInputs()">           

                    <label for="ulemail"><b>E-mail:</b></label>
                    <input type="email" placeholder="Enter E-mail Address" name="ulemail" required />

                    <label for="ulpass"><b>Password:</b></label>
                    <input type="password" placeholder="Enter Password" name="ulpass" required />

                    <button id="loginSubmit" runat="server" type="submit">Login</button><br/><br/>
                     
                    <label><b>First time booking? </b><a href="Register.aspx" style="text-decoration: none; color: teal; font-weight: bold;">Register here</a></label>

             </form> 
         </div>

    </div>

             

   </div>

    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>


</body>
</html>
