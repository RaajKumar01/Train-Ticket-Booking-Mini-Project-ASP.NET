<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmbooking.aspx.cs" Inherits="train.confirmbooking" %>


<!DOCTYPE html>

<html>
<head id="Head1"  runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Confirm Book Ticket - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />
       <script type="text/javascript" src="script.js"> </script>
</head>
<body>
    
   <div class="content-wrapper">
        
         <div class="navbar">
            <div class="title">Trains</div>
            <div class="menu">
                <a href="dashboard.aspx">Home</a>
                <a href="dashboard.aspx#about-section">About</a>
                <a href="admin.aspx">Admin</a>
                <a href="bookticket.aspx">Book Ticket</a>
            </div>
        </div>                                              
    
        <div class="user-info">
        <span>Welcome, <strong><%= Session["Username"] %></strong></span>
        <button type="submit" id="logoutBtn" class="logout-btn" runat="server" onserverclick="LogoutBtn_Click">Logout</button>
        </div> 

         <div class="pages-main-content">
            <h1>Confirm Booking and Payment Details</h1>

                 <div class="notification-message" id="TrainBookConfirmMessage"  runat="server"> Notification Message </div>
                
                <div id="bookingFinalForm" class="form-container" runat="server">
                 <form id="bookFormConform" runat="server">

                     <!-- Book Ticket Main Form -->
                     
                        <h2>Complete Your Train Booking</h2>
                        
                            <!-- Booker Name -->
                            <label for="CBbookerName">Booker Name:</label>
                            <input type="text" id="CBbookerName" runat="server" readonly="readonly"   />

                            <div class="input-group">
                                 <div>
                                     <!-- Booked Train Name -->
                                   <label for="CBbookedTrainName">Booking Train Name: </label>
                                   <input type="text" id="CBbookedTrainName" runat="server" readonly="readonly"   />
                                 </div>
                                 <div>                                                                                                                 
                                    <!-- Booked Train Type -->
                                    <label for="CBbookedTrainType">Booking Train Type:</label>
                                    <input type="text" id="CBbookedTrainType" runat="server" readonly="readonly"   />
                                </div>
                            </div>

                              <div class="input-group">
                                 <div>
                                     <!-- Booked Train Name -->
                                   <label for="CBbookedTrainSource">Booking Train Source: </label>
                                   <input type="text" id="CBbookedTrainSource" runat="server" readonly="readonly"   />
                                 </div>
                                 <div>                                                                                                                 
                                    <!-- Booked Train Type -->
                                    <label for="CBbookedTrainDestination">Booking Train Destination:</label>
                                    <input type="text" id="CBbookedTrainDestination" runat="server" readonly="readonly"   />
                                </div>
                            </div>

                         
                            <div class="input-group">
                                 <div>
                                    <!-- Booking Date -->
                                    <label for="CBbookingDate">Booking Date:</label>
                                    <input type="date" id="CBbookingDate" runat="server" readonly="readonly"    />
                                 </div>
                                 <div>                                                                                                                 
                                    <!-- Travel Date -->
                                    <label for="CBtravelDate">Travel Date:</label>
                                    <input type="date" id="CBtravelDate" runat="server" readonly="readonly"    />
                                </div>
                            </div>
                            
                             <!-- Booked Train Seat Type -->
                            <label for="CBbookingSeat">Select Train Seat Type:</label>
                            <select id="CBbookingSeat" runat="server" >
                                <option value="AC">AC Class</option>
                                <option value="Sleeper">Sleeper Class</option>
                                <option value="Second">Second Class</option>
                            </select>
                            
                             <div class="input-group">
                                 <div>
                                    <!-- Selected total tickets Seats -->
                                    <label for="CBticketsTotal">Total Tickets Needed (Max: 6):</label>
                                    <input type="number" id="CBticketsTotal" min="1" max="6" runat="server" placeholder="Enter number of tickets needed" onchange="updateTotalAmount()"  />
                                 </div>
                                 <div>
                                    <!--   total tickets Ammount autoupdate -->
                                    <label for="CBticketsAmount">Total Amount (₹):</label>
                                    <input type="number" id="CBticketsAmount" runat="server" readonly="readonly"   />
                                 </div>
                            </div>
                            <!-- Booking Details -->
                            <label for="CBbookingDetails">Booking Details:</label>
                            <textarea id="CBbookingDetails" runat="server" placeholder="Enter passengers' names and ages, separated by commas" rows="4"  ></textarea>
                            
                            <!-- Payment Method -->
                            <label for="CBpaymentMethod">Payment Method:</label>
                            <select id="CBpaymentMethod" runat="server" onchange="updateTotalAmount()" >
                                <option value="Credit Card">Credit Card</option>
                                <option value="Debit Card">Debit Card</option>
                            </select>
                              <!-- Payment Name on Card -->
                            <label for="CBcardHolder">Name on Card:</label>
                            <input type="text" id="CBcardHolder"   runat="server"  placeholder="Enter Name on Card" required />
                              <!-- Payment  Card Number -->
                            <label for="CBcardNumber">Card Number:</label>
                            <input type="number" id="CBcardNumber" runat="server"  max="16" placeholder="Enter 16-digit card number" required />

                            
                     
                             <div class="input-group">
                                 <div>
                                         <!-- Payment  Card Expiry -->
                                        <label for="CBexpiryMonth">Expiry Date:</label>
                                        <div   style="display: flex; align-items: center; gap: 5px;">
                                            <input type="number" id="CBexpiryMonth" runat="server"   placeholder="MM" min="1" max="12"   style="width: 60px;" required />
                                            <span>/</span>
                                            <input type="number" id="CBexpiryYear"  runat="server" placeholder="YY" min="25" max="99"   style="width: 60px;" required />
                                        </div>

                                 </div>
                                 <div>                                                                                                                 
                                      <!-- Payment  Card CVV -->
                                        <label for="CBcardcvv">CVV:</label>
                                        <input type="number" id="CBcardcvv" runat="server"  max="3" placeholder="Enter 3-Digit CVV Number" required />
                                </div>
                            </div>

                          <!-- Book Now Button -->
                            <button type="submit" id="BookingConfirmBtn" runat="server"  onserverclick="BookingConfirmBtn_Click"> Pay & Book Now</button>
                    

                 </form> <!-- End of book-Form class -->
 
             </div> 
                     <!-- End of Book Ticket Main Form -->

         </div> <!-- End of page main content -->
       </div><!-- End of wrapper -->

    <footer>
        &copy; 2025 Online Train Ticket Booking. All rights reserved.
    </footer>


   <script>
       // Define seat prices from the server-side values
       var seatPrices = {
           AC: <%= SeatACPrice %>,
        Sleeper: <%= SeatSleeperPrice %>,
        Second: <%= SeatSecondPrice %>
        };

    // Function to update total amount
    function updateTotalAmount() {
        var seatType = document.getElementById('<%= CBbookingSeat.ClientID %>').value;
        var totalTickets = parseInt(document.getElementById('CBticketsTotal').value) || 1;
        var seatPrice = seatPrices[seatType] || 0;
        var totalAmount = seatPrice * totalTickets;

        // Update the total amount field
        document.getElementById('CBticketsAmount').value = totalAmount.toFixed(2);
    }
        
    // Attach event listeners
    document.getElementById('<%= CBbookingSeat.ClientID %>').addEventListener('change', updateTotalAmount);
       document.getElementById('CBticketsTotal').addEventListener('input', updateTotalAmount);
    </script>


</body>
</html>
