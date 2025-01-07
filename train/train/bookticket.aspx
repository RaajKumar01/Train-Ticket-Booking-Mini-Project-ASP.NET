<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookticket.aspx.cs" Inherits="train.bookticket" %>


<!DOCTYPE html>

<html>
<head  runat="server">
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
            <h1>Search & Book Tickets </h1>

                 <div id="TrainBookMessage"  runat="server"  style="color:red; font-weight: bold; text-shadow: 1px 1px #000000; text-align: center;"> Notification Message <br /></div>
                
              <div class="book-form-page">
                 <form id="bookForm" runat="server" >

                        <!-- Train Search Div -->
                        <div id="searchSection" runat="server">
                            <h2>Search Trains</h2>
                            <label for="source">Source:</label>
                            <input type="text" id="trainSearchSource" name="source"  runat ="server" placeholder="Enter departure location"   />
                            <label for="destination">Destination:</label>
                            <input type="text" id="trainSearchdestination"  name="destination" runat="server" placeholder="Enter destination location"   />
                            <label for="date">Date of Travel:</label>
                            <input type="date" id="trainSearchDate" name="date" runat="server"    />
                           
                            <button type="submit" id="submitSearchBtn" runat="server"  onserverclick="SubmitSearchBtn_Click">Search</button>
                        </div>
                        <!-- End of Train Search Div -->


                            <!-- Manually Added GridView to show the list of the trains available for the search
                                 Easy to handle server-side and selsctions then Html Table -->
                        <div id="trainListSection" runat="server">
                            <h1>Available Trains</h1>
                            <asp:GridView ID="trainListGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView_RowCommand" CssClass="Availabletable" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="trainNo" HeaderText="Train No" SortExpression="trainNo" />
                                    <asp:BoundField DataField="trainName" HeaderText="Train Name" SortExpression="trainName" />
                                    <asp:BoundField DataField="trainType" HeaderText="Train Type" SortExpression="trainType" />
                                    <asp:BoundField DataField="trainSource" HeaderText="Source" SortExpression="trainSource" />
                                    <asp:BoundField DataField="trainDestination" HeaderText="Destination" SortExpression="trainDestination" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                             <asp:Button ID="btnSelect" Text="Select" CommandName="SelectTrain" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="select-btn" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                            <!-- End of Train GridView -->

                     <!-- Selecting the Train Details Needed -->
                      <div id="BookingTrainForm" class="book-form-main" runat="server">
                         <h2>Train Booking Details</h2>
                        
                            <!-- Booker Name -->
                            <label for="TFBookerName">Booker Name:</label>
                            <input type="text" id="TFBookerName" runat="server" readonly="readonly"   />
                            
                            <!-- Booked Train No -->
                            <label for="TFTrainNO">Selected Train No:</label>
                            <input type="number" id="TFTrainNO" runat="server" readonly="readonly"   />
                            
                            <!-- Booked Train Name -->
                            <label for="TFTrainName">Selected Train Name:</label>
                            <input type="text" id="TFTrainName" runat="server" readonly="readonly"  />

                            <!--   Train Seat Type -->
                           <label for="TFSeatTypeASP">Select Train Seat Type:</label>
                            <asp:DropDownList ID="TFSeatTypeASP"  runat="server">
                            </asp:DropDownList>

                             <!-- Select Seats -->
                            <label for="TFtickets">Enter Ticket Needed: (Max: 20):</label>
                            <input type="number" id="TFtickets" runat="server" min="1" max="20" placeholder="Enter number of tickets"   />
                             
                            <!-- Processed to payment Button -->
                            <button type="submit" id="proceedBtn" runat="server"  onserverclick="ProceedPaymentBtn_Click"> Proceed to Payment</button>
                    </div> 
                     <!-- Book Ticket Main Form -->
                      

                 </form> <!-- End of book-Form class -->
               </div> <!-- End of book-form-page class -->


         </div> <!-- End of page main content -->
       </div><!-- End of wrapper -->

    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>