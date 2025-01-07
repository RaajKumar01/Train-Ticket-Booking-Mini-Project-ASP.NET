<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="train.admin" %>

<!DOCTYPE html>

<html>
<head   runat="server">
    <title>Admin Page - Online Train Ticket Booking</title>
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
     

    <div class="pages-main-content">
        <h1>Train Details Entry</h1>
        <div id="TrainEntryMessage"  runat="server"  style="color:red; font-weight: bold; text-shadow: 1px 1px #000000; text-align: center;"> Notifiation Message <br /></div>
             <div class="train-form">
                <h2>Enter Train Details</h2>
                <form id="trainForm" runat="server">
                    <label for="trainName">Train Name:</label>
                    <input type="text" id="trainName" runat="server" placeholder="Enter train name" required />

                    <label for="trainType">Train Type:</label>
                    <input type="text" id="trainType" runat="server" placeholder="Enter train type" required />

                    <div class="input-group">
                        <div>
                            <label for="acSeats">Total AC Seats:</label>
                            <input type="number" id="acSeats" runat="server" placeholder="Enter number of AC seats" required />
                        </div>
                        <div>
                            <label for="acPrice">Ticket Price (INR):</label>
                            <input type="number" id="acPrice" runat="server" placeholder="Enter AC seat price" required />
                        </div>
                    </div>

                    <div class="input-group">
                        <div>
                            <label for="sleeperSeats">Total Sleeper Class Seats:</label>
                            <input type="number" id="sleeperSeats" runat="server" placeholder="Enter number of Sleeper seats"  required/>
                        </div>
                        <div>
                            <label for="sleeperPrice">Ticket Price (INR):</label>
                            <input type="number" id="sleeperPrice" runat="server" placeholder="Enter Sleeper seat price"  required />
                        </div>
                    </div>

                    <div class="input-group">
                        <div>
                            <label for="secondClassSeats">Total Second Class Seats:</label>
                            <input type="number" id="secondClassSeats" runat="server" placeholder="Enter number of Second Class seats" required />
                        </div>
                        <div>
                            <label for="secondClassPrice">Ticket Price (INR):</label>
                            <input type="number" id="secondClassPrice" runat="server" placeholder="Enter Second Class seat price" required />
                        </div>
                    </div>

                    <label for="trainSource">Train Source:</label>
                    <input type="text" id="trainSource" runat="server" placeholder="Depature (Ex. Chennai)"  required />

                    <label for="trainDestination">Train Destination:</label>
                    <input type="text" id="trainDestination" runat="server" placeholder="Destination (Ex. Delhi)" required />

                    <div class="input-group">
                        <div>
                            <label for="startTime">Train Start Time:</label>
                            <input type="time" id="startTime" runat="server"  required />
                        </div>
                        <div>
                            <label for="endTime">Train End Time:</label>
                            <input type="time" id="endTime" runat="server" required />
                        </div>
                    </div>

                     <label for="trainDetails">Additional Train Details:</label>
                     <textarea id="trainDetails" runat="server" placeholder="Enter travel duration, catering services, features, etc." rows="4" required></textarea>

                    <button type="submit" id="submitBtn" runat="server"  onserverclick="SubmitBtn_Click">Submit</button>
                </form>
            </div>
    
            </div>
    </div>
    
    <footer>
        &copy; 2024 Online Train Ticket Booking. All rights reserved.
    </footer>

</body>
</html>
