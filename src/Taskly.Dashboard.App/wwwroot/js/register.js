// Password Validation
function showPasswordMessage() {
    const password = getValue("passwordInput");
    const message = getElement("passwordMessage");

    if (password.length < 6) {
        updateMessage(message, "Password must be at least 6 characters long!", "red", true);
    }
}

function validatePassword() {
    const password = getValue("passwordInput");
    const message = getElement("passwordMessage");

    if (password.length >= 6) {
        updateMessage(message, "Valid password.", "green", true);
    } else {
        updateMessage(message, "Password must be at least 6 characters long!", "red", true);
    }
}

function hidePasswordMessageIfEmpty() {
    const password = getValue("passwordInput");
    const message = getElement("passwordMessage");

    if (password.length === 0) {
        message.style.display = "none";
    }
}

// Confirm Password Validation
function showConfirmPasswordMessage() {
    const password = getValue("passwordInput");
    const confirmPassword = getValue("confirmPassword");
    const message = getElement("confirmPasswordMessage");

    if (password && password !== confirmPassword) {
        updateConfirmMessage(message, "Passwords do not match!", "confirmError", "confirmSuccess");
    }
}

function validateConfirmPassword() {
    const password = getValue("passwordInput");
    const confirmPassword = getValue("confirmPassword");
    const message = getElement("confirmPasswordMessage");

    if (password === confirmPassword) {
        updateConfirmMessage(message, "Passwords match!", "confirmSuccess", "confirmError");
    } else {
        updateConfirmMessage(message, "Passwords do not match!", "confirmError", "confirmSuccess");
    }
}

// Checkbox Agreement
function toggleRegisterButton() {
    const checkbox = getElement("agreeCheckbox");
    const registerBtn = getElement("registerBtn");
    const errorMessage = getElement("error-message");

    const isChecked = checkbox.checked;

    errorMessage.style.display = isChecked ? "none" : "block";
    registerBtn.disabled = !isChecked;
}

// Helper Functions
function getElement(id) {
    return document.getElementById(id);
}

function getValue(id) {
    return getElement(id).value.trim();
}

function updateMessage(element, text, color, show = true) {
    element.textContent = text;
    element.style.color = color;
    element.style.display = show ? "block" : "none";
}

function updateConfirmMessage(element, text, addClass, removeClass) {
    element.textContent = text;
    element.classList.add(addClass);
    element.classList.remove(removeClass);
}