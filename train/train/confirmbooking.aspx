<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmbooking.aspx.cs" Inherits="train.confirmbooking" %>


<!DOCTYPE html>

<html>
<head id="Head1"  runat="server">
    <title>Book Ticket - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />

   <style>

        .Availabletable {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

        .Availabletable th {
            border: 2.5px solid #000;
            background-color: #13858a;
            color: white;
            padding: 10px;
        }

        .Availabletable td {
            border: 2.5px solid #000;
            padding: 10px;
            text-align: left;
            color: white;
        }

        .select-btn {
            padding: 5px 10px;
            background-color: #ffd800;
            border: none;
            color: black;
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
    
         <div class="pages-main-content">
            <h1>Confirm Booking and Payment Details</h1>

                 <div id="TrainBookConfirmMessage"  runat="server"  style="color:red; font-weight: bold; text-shadow: 1px 1px #000000; text-align: center;"> Notification Message <br /></div>
                
              <div class="book-form-page">
                 <form id="bookFormConform" runat="server" >

                     <!-- Book Ticket Main Form -->
                     <div id="bookingFinalForm" class="book-form-main" runat="server">
                        <h2>Complete Your Train Booking</h2>
                        
                            <!-- Booker Name -->
                            <label for="CBbookerName">Booker Name:</label>
                            <input type="text" id="CBbookerName" runat="server" readonly="readonly"   />

                            <label for="CBbookedTrainNO">Booker Name:</label>
                            <input type="number" id="CBbookedTrainNO" runat="server" readonly="readonly"   />

                            <!-- Booked Train Type -->
                            <label for="CBbookedTrainType">Booking Train Type:</label>
                            <input type="text" id="CBbookedTrainType" runat="server" readonly="readonly"   />

                            <!-- Booked Train Seat Type -->
                            <label for="CBbookingSeatType">Selected Train Seat Type:</label>
                            <input type="text" id="CBbookingSeatType" runat="server" readonly="readonly"   />

                            <!-- Booking Date -->
                            <label for="CBbookingDate">Booking Date:</label>
                            <input type="date" id="CBbookingDate" runat="server" readonly="readonly"    />

                            <!-- Travel Date -->
                            <label for="CBtravelDate">Travel Date:</label>
                            <input type="date" id="CBtravelDate" runat="server" readonly="readonly"    />

                            <!-- Selected total tickets Seats -->
                            <label for="CBticketsTotal">Total Tickets:</label>
                            <input type="number" id="CBticketsTotal" runat="server" readonly="readonly"     />

                            <!--   total tickets Ammount -->
                            <label for="CBticketsAmount">Total Amount:</label>
                            <input type="number" id="CBticketsAmount" runat="server" readonly="readonly"   />

                            <!-- Booking Details -->
                            <label for="CBbookingDetails">Booking Details:</label>
                            <textarea id="CBbookingDetails" runat="server" placeholder="Enter passengers' names and ages, separated by commas" rows="4"  ></textarea>

                            <!-- Payment Method -->
                            <label for="CBpaymentMethod">Payment Method:</label>
                            <select id="CBpaymentMethod" runat="server" >
                                <option value="Credit Card">Credit Card</option>
                                <option value="Debit Card">Debit Card</option>
                            </select>
                        
                            <label for="CBcardHolder">Name on Card:</label>
                            <input type="text" id="CBcardHolder" runat="server"  placeholder="Enter Name on Card" required />

                            <label for="CBcardNumber">Card Number:</label>
                            <input type="text" id="CBcardNumber" runat="server"  maxlength="16" placeholder="1111-2222-3333-4444" required />

                            <label for="CBexpiryMonth">Expiry Date:</label>
                            <div   style="display: flex; align-items: center; gap: 5px;">
                                <input type="number" id="CBexpiryMonth" runat="server"   placeholder="MM" min="1" max="12"   style="width: 60px;" required />
                                <span>/</span>
                                <input type="number" id="CBexpiryYear"  runat="server" placeholder="YY" min="25" max="99"   style="width: 60px;" required />
                            </div>

                            <label for="CBcardcvv">CVV:</label>
                            <input type="text" id="CBcardcvv" runat="server"  maxlength="16" placeholder="Enter CVV Number" required />


                            <!-- Book Now Button -->
                            <button type="submit" id="BookingConfirmBtn" runat="server"  onserverclick="BookingConfirmBtn_Click"> Pay & Book Now</button>
                    </div> 
                     <!-- End of Book Ticket Main Form -->

                 </form> <!-- End of book-Form class -->
               </div> <!-- End of book-form-page class -->


         </div> <!-- End of page main content -->
       </div><!-- End of wrapper -->

    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>
