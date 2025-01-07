<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="train.register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

      <script>
          function validateRegisterInputs() {
              var registerusername = document.querySelector('input[name="urname"]').value;
              var registeruseremail = document.querySelector('input[name="uremail"]').value;
              var registerpassword = document.querySelector('input[name="urpass"]').value;
              var registeremailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
              var usernameRegex = /^(?=.*[a-zA-Z0-9_])(?!(.*\s){4,})[a-zA-Z0-9_ ]{4,30}$/;


              if (!usernameRegex.test(registerusername)) {
                   
                  alert("Invalid username. It must be 4-30 characters and contain no special characters.");
      
                  return false; // Prevent form submission
              }  

              if (registerusername.trim() === "") {
                  alert("Please enter your full name.");
                  return false; // Prevent form submission
              }

              if (registeruseremail.trim() === "") {
                  alert("Please enter your email address.");
                  return false; // Prevent form submission
              }

              if (!registeremailRegex.test(registeruseremail)) {
                  alert("Please enter a valid email address.");
  
                  return false; // Prevent form submission
              }

              if (registerpassword.trim() === "") {
                  alert("Please enter your password.");
                  return false; // Prevent form submission
              } else if (registerpassword.length < 6 || registerpassword.length > 12) {
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
        <h1>Register Account</h1>
        <div id="errorRegisterMessage"  runat="server"  style="color:red; font-weight: bold; text-shadow: 1px 1px #000000;">Email already exists. Please use a different email. <br /></div>
         <div class="register-box"> 

             <form id="registerform" runat="server"  method="post"  onsubmit="return validateRegisterInputs()">

                    <label for="urname"><b>Username:</b></label>
                    <input type="text" placeholder="Enter Your Full Name" name="urname" required />

                    <label for="uremail"><b>E-mail:</b></label>
                    <input type="email" placeholder="Enter E-mail Address" name="uremail" required />

                    <label for="urpass"><b>Password:</b></label>
                    <input type="password" placeholder="Enter Password" name="urpass" required />

                    <button id="registerSubmit" runat="server" type="submit">Register</button> <br /> <br />
                     
                    <label><b>Already Have an Account? </b><a href="Login.aspx" style="text-decoration: none; color: teal; font-weight: bold;">Log In</a></label>

             </form> 
         </div>

    </div>

   </div>

    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>
