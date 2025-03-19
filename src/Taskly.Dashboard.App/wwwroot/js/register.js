function showMessage() {
    let message = document.getElementById("passwordMessage");
    message.style.display = "block";
    message.style.color = "red";
    message.textContent = "Password must be at least 6 characters long.";
}

function validatePassword() {
    let password = document.getElementById("passwordInput").value;
    let message = document.getElementById("passwordMessage");

    if (password.length >= 6) {
        message.style.color = "green";
        message.textContent = "Valid password.";
    } else {
        message.style.color = "red";
        message.textContent = "Password must be at least 6 characters long.";
    }
}

function hideMessageIfEmpty() {
    let password = document.getElementById("passwordInput").value;
    let message = document.getElementById("passwordMessage");

    if (password.length === 0) {
        message.style.display = "none";
    }
}