<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="showticket.aspx.cs" Inherits="train.showticket" %>

<!DOCTYPE html>

<html>
<head runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Show Ticket - Online Train Ticket Booking</title>
      
    <style>
        
    /* Show ticket Page */
     #showticket {
        font-family: Arial, sans-serif;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        margin: 0;
        background-color: #f4f4f4;
    }
    #showticket  .container {
        width: 90%;
        max-width: 900px;
        background-color: #fff;
        padding: 30px;
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
    #showticket table {
        width: 100%;
        margin-bottom: 20px;
        border-collapse: collapse;
    }
    #showticket table td {
        padding: 10px;
        border: 1px solid grey;
        white-space: normal; /* Allow content to wrap */
        vertical-align: top; /* Align content to the top */
    }
    #showticket .label {
        font-weight: bold;
    }
  
    #showticket .button-container {
        text-align: center;
        margin-top: 20px;
    }
    /* End of Show Ticket page */
    </style>
        
</head>


<body id ="showticket">

 

    <div class="container">
        <h4>Online Train Ticket Booking</h4>
        <h2 style="text-align: center;">Train Ticket</h2>
        
        <!-- Train Details in Two Columns -->
        <table>
            <tr>
                <td class="label">Booking No:</td>
                <td><asp:Label ID="lblbookingno" runat="server"  ></asp:Label></td>
                <td class="label">Booker Name:</td>
                <td><asp:Label ID="lblbooker" runat="server" ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train No:</td>
                <td><asp:Label ID="lblTrainNo" runat="server"  ></asp:Label></td>
                <td class="label">Train Seat Class Type:</td>
                <td><asp:Label ID="lblSeatType" runat="server" ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train Name:</td>
                <td><asp:Label ID="lblTrainName" runat="server" ></asp:Label></td>
                <td class="label">Train Seats Booked:</td>
                <td><asp:Label ID="lblSeatsBooked" runat="server"  ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train Type:</td>
                <td><asp:Label ID="lblTrainType" runat="server"></asp:Label></td>
                <td class="label">Payment Transcation No:</td>
                <td><asp:Label ID="lblPaymentNo" runat="server"  ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train Source:</td>
                <td><asp:Label ID="lblTrainSource" runat="server"></asp:Label></td>
                <td class="label">Payment Methid:</td>
                <td><asp:Label ID="lblPaymentMethod" runat="server"  ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train Destination:</td>
                <td  ><asp:Label ID="lblTrainDestination" runat="server"  ></asp:Label></td>
                <td class="label">Total Amount Paid:</td>
                <td><asp:Label ID="lblAmountPaid" runat="server"  ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Train Start Time:</td>
                <td  ><asp:Label ID="lblStartTime" runat="server"  ></asp:Label></td>
                <td class="label">Train End Time:</td>
                <td><asp:Label ID="lblEndTime" runat="server"  ></asp:Label></td>
            </tr>
            <tr>
                <td class="label">Booked On:</td>
                <td  ><asp:Label ID="lblBookedOn" runat="server"  ></asp:Label></td>
                <td class="label">Travel Date:</td>
                <td><asp:Label ID="lblTravelDate" runat="server"  ></asp:Label></td>
            </tr>
        </table>
        
        <!-- Separator Line -->
        <hr>

        <!-- Train Details Section -->
        <div>
            <label><strong>Booking Details:</strong></label>
            <p><asp:Label ID="lblBookingDetails" runat="server"></asp:Label></p>
        </div>

        <!-- Separator Line -->
        <hr>

        <!-- Train Details Section -->
        <div>
            <label><strong>Train Details:</strong></label>
            <p><asp:Label ID="lblTrainDetails" runat="server"></asp:Label></p>
        </div>

        <!-- Print Button -->
        <div class="button-container">
            <button onclick="window.print()">Print Ticket</button>
        </div>
    </div>
 
</body>
</html>
