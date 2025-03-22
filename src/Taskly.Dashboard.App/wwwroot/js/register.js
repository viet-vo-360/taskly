function showMessage() {
    let password = document.getElementById("passwordInput").value;
    let message = document.getElementById("passwordMessage");
    if (password.length < 6) {
        message.style.display = "block";
        message.style.color = "red";
        Message(message, "Password must be at least 6 characters long!")
    }
}

function Message(element, message) {
    element.textContent = message;
}

function validatePassword() {
    let password = document.getElementById("passwordInput").value;
    let message = document.getElementById("passwordMessage");

    if (password.length >= 6) {
        message.style.color = "green";
        Message(message, "Valid password.")
    } else {
        message.style.color = "red";
        Message(message, "Password must be at least 6 characters long!")
    }
}

function hideMessageIfEmpty() {
    let password = document.getElementById("passwordInput").value;
    let message = document.getElementById("passwordMessage");

    if (password.length === 0) {
        message.style.display = "none";
    }
}

function check() {
    let errorMessage = document.getElementById("error-message");
    let check = document.getElementById("agreeCheckbox");
    let register = document.getElementById("registerBtn");

    if (check.checked == true) {
        errorMessage.style.display = "none";
        register.disabled = false;
    } else {
        errorMessage.style.display = "block"
        register.disabled = true;
    }
}


function showConfirmMessage() {
    let confirmMessage = document.getElementById("confirmPasswordMessage");
    let passwordInput = document.getElementById("passwordInput").value;
    let confirmPassword = document.getElementById("confirmPassword").value;
    if (passwordInput.length !== 0 && passwordInput !== confirmPassword) {
        changeClass(confirmMessage, "confirmError", "confirmSuccess")
        ConfirmMessage(confirmMessage, "passwords do not match!")
    }
}

function changeClass(element, addClass, removeClass) {
    element.classList.add(addClass);
    element.classList.remove(removeClass);
}

function ConfirmMessage(element, message) {
    element.textContent = message;
}

function validateConfirmPassword() {
    let passwordInput = document.getElementById("passwordInput").value;
    let confirmPassword = document.getElementById("confirmPassword").value;
    let confirmMessage = document.getElementById("confirmPasswordMessage");

    if (passwordInput === confirmPassword) {
        ConfirmMessage(confirmMessage, "Passwords match!")
        changeClass(confirmMessage, "confirmSuccess", "confirmError")
    } else {
        ConfirmMessage(confirmMessage, "passwords do not match!")
        changeClass(confirmMessage, "confirmError", "confirmSuccess")
    }
}





