<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="train.dashboard" %>

<!DOCTYPE html>

<html>
<head   runat="server">
    <title>Dashboard - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />
    <style>

            table {
                width: 80%;
                border-collapse: collapse;
                margin-left: auto;
                margin-right: auto;
            }

            table th {

                background-color: #13858a; /* Change to your desired color */
                color: white; /* Optional: Change text color */
            }


            th, td {
                border: 1px solid black;
                padding: 8px;
                text-align: left;
                color: white;
            }

            th {
                background-color: #f2f2f2;
            }

            .details-row {
                display: none;
                background-color: #f9f9f9;
            }

            .details-row td {
                padding: 12px;
                font-size: 0.9em;
                color: #555;
            }

            .expand-icon {
                cursor: pointer;
                color: red;
                font-size: 20px;

            }

            .cancel-btn {
                padding: 5px 10px;
                background-color: #ff5555;
                border: none;
                color: white;
                cursor: pointer;
                border-radius: 5px;
                font-weight: bold;
            }
        
    </style>
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

        <form id="dash_user_form" runat="server"><div class="user-info">
        <span>Welcome, <strong><%= Session["Username"] %></strong></span>
        <button type="submit" id="logoutBtn" class="logout-btn" runat="server" onserverclick="LogoutBtn_Click">Logout</button>
        </div> </form>


   
                
                <div class="tickets-section">
                    <h2>Your Bookings</h2>

                        <div  id="TicketsTableContainer" runat="server"></div>

                        <div class="book-ticket-btn">
                            <button onclick="window.location.href='BookTicket.aspx'">Book Tickets</button>
                        </div>

                </div>
    
       </div>

    
    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

    
      <script>
          function toggleDetails(ticketId) {
              // Find the details row using its ID
              const detailsRow = document.getElementById("details-" + ticketId);

              // Toggle visibility of the details row
              if (detailsRow.style.display === "table-row") {
                  detailsRow.style.display = "none";
                  // Update the icon to "+"
                  document.querySelector("[onclick='toggleDetails(\"" + ticketId + "\")']").textContent =  "+";

              } else {
                  detailsRow.style.display = "table-row";
                  // Update the icon to "-"
                  document.querySelector("[onclick='toggleDetails(\"" + ticketId + "\")']").textContent = "-";

              }
          }
    </script>

</body>
</html>


