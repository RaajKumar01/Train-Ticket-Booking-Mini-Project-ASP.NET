<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="train.dashboard" %>

<!DOCTYPE html>

<html>
<head   runat="server">

    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Dashboard - Online Train Ticket Booking</title>
    <link href="style.css" rel="stylesheet" />
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

                <form id="ticketsboard" runat="server">
                
                <div class="tickets-section">
                    <h2>Your Bookings</h2>

                        <!-- Div for No Tickets message -->
                        <div id="noticketsmessage" runat="server" class="no-tickets">
                           You don't have any bookings at the moment. <br />
                           To book train tickets, visit 'Book Ticket'.
                        </div>


                        <asp:GridView ID="trainListGridView" runat="server" AutoGenerateColumns="False" 
                              OnRowCommand="GridView_RowCommand" CssClass="GridFormtable" GridLines="None">
                            <Columns>
                                <asp:BoundField DataField="BookID" HeaderText="Booking No" SortExpression="BookID" />    
                                <asp:BoundField DataField="BookedTrainNo" HeaderText="Train No" SortExpression="BookedTrainNo" />
                                <asp:BoundField DataField="trainName" HeaderText="Train Name" SortExpression="trainName" />
                                <asp:BoundField DataField="BookedSeatType" HeaderText="Train Seat Type" SortExpression="BookedSeatType" />
                                <asp:BoundField DataField="trainSource" HeaderText="Source" SortExpression="trainSource" />
                                <asp:BoundField DataField="trainDestination" HeaderText="Destination" SortExpression="trainDestination" />
                                <asp:BoundField DataField="BookedDate" HeaderText="Travel Date" SortExpression="BookedDate" DataFormatString="{0:dd-MM-yyyy}" HtmlEncode="false" />

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>

                                        <!-- Show Ticket Button -->
                                        <asp:Button ID="btnShowTicket" Text="Show Ticket" CommandName="ShowTicket" 
                                                    CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="select-btn"    />

                                        <!-- Cancel Button -->
                                        <asp:Button ID="btnCancel" Text="Cancel" CommandName="CancelBooking" 
                                                    CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="select-btn"    />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns> 
                        </asp:GridView> 
                       <!-- End of Train GridView --> 
                </div>
                </form>
             </div>
              <!-- End of Page Main Section here -->


                 <!-- Add About Section here -->
                <div id="about-section">
                    <h3>About Us</h3>
                    <p>Welcome To Train Ticket Booking. We are dedicated to providing a seamless and efficient online train ticket booking experience.</p>
                    <p> Our platform offers a wide range of train options, ensuring comfort and convenience for every traveler. Book your tickets in just a few clicks and start your journey with ease.</p>
                    <p>If you have any questions or need assistance, feel free to reach out to us. We're here to help!</p>
                    <p> Email: <a href="mailto:support@trains.com">support@trains.com</a> </p>
                </div>

       </div> <!-- End of Content Wrapper here -->

    
    <footer>
        &copy; 2025 Online Train Ticket Booking. All rights reserved.
    </footer>

    
      <script>
 
    </script>

</body>
</html>


