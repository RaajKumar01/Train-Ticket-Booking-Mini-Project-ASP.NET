<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookticket.aspx.cs" Inherits="train.bookticket" %>


<!DOCTYPE html>

<html>
<head  runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Book Ticket - Online Train Ticket Booking</title>
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
            <h1>Search & Book Tickets </h1>

                 <div class="notification-message" id="TrainBookMessage"  runat="server"> Notification Message</div>
                
              
                  <form id="bookForm" runat="server" >
                

                        <!-- Train Search Div -->
                        <div class="form-container" runat="server">
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
                            <h1 id="availHeader" runat="server">Available Trains< on</h1>
                            <asp:GridView ID="trainListGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView_RowCommand" CssClass="GridFormtable" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="trainNo" HeaderText="Train No" SortExpression="trainNo" />
                                    <asp:BoundField DataField="trainName" HeaderText="Train Name" SortExpression="trainName" />
                                    <asp:BoundField DataField="trainType" HeaderText="Train Type" SortExpression="trainType" />
                                    <asp:BoundField DataField="trainSource" HeaderText="Source" SortExpression="trainSource" />
                                    <asp:BoundField DataField="trainDestination" HeaderText="Destination" SortExpression="trainDestination" /> 
                                    <asp:BoundField DataField="trainACSeats" HeaderText="AC Seats" SortExpression="trainACPrice" />
                                    <asp:BoundField DataField="trainSleeperClassSeats" HeaderText="Sleeper Seats" SortExpression="trainSleeperClassPrice" />
                                    <asp:BoundField DataField="trainSecondClassSeats" HeaderText="II Class Seats" SortExpression="trainSecondClassPrice" />
                                    <asp:BoundField DataField="trainACPrice" HeaderText="AC (₹)" SortExpression="trainACPrice" />
                                    <asp:BoundField DataField="trainSleeperClassPrice" HeaderText="Sleeper (₹)" SortExpression="trainSleeperClassPrice" />
                                    <asp:BoundField DataField="trainSecondClassPrice" HeaderText="Second (₹)" SortExpression="trainSecondClassPrice" />
                                    <asp:BoundField DataField="trainStarttime" HeaderText="Start Time" SortExpression="trainStarttime" />
                                    <asp:BoundField DataField="trainEndTime" HeaderText="End Time" SortExpression="trainEndTime" />
                                   

                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                             <asp:Button ID="btnSelect" Text="Select" CommandName="SelectTrain" CommandArgument="<%# Container.DataItemIndex %>" runat="server" CssClass="select-btn" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                            <!-- End of Train GridView -->

                     
                     <!-- Book Ticket Main Form -->

                 </form> <!-- End of book-Form class -->
  


         </div> <!-- End of page main content -->
    
       </div><!-- End of wrapper -->

    <footer>
        &copy; 2025 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>