
// Login Function Validation
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

// Register Function Validation
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

// Notification Message Function
function showNotification(divId, message, isSuccess) {

    var messageDiv = document.getElementById(divId);

    if (messageDiv) {
        // Set the message text
        messageDiv.innerHTML = message;

        // Apply styles based on success or error
        if (isSuccess) {
            messageDiv.style.color = "#77ab46"; //  green text
        } else {
            messageDiv.style.color = "#de1a24"; // red text
        }

        // Show the div
        messageDiv.style.display = "block";

        // Hide the div after 4 seconds
        setTimeout(function () {
            messageDiv.style.display = "none";
        }, 4000);
    }
}
