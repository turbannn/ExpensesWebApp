﻿async function submitLogin() {
    console.log("Function Login executed");

    const username = document.getElementById("LoginUsername").value.trim();
    const password = document.getElementById("LoginPassword").value.trim();

    if (!username) {
        alert("Error: Username cant be empty");
        return;
    }
    if (!password) {
        alert("Error: Password cant be empty");
        return;
    }

    const userLogin = {
        Username: username, Password: password
    };

    console.log("Sending data:", userLogin);

    fetch("/User/SubmitLogin", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(userLogin)
    })
        .then(response => response.json())
        .then(result => {
            console.log("Server response:", result);

            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Login error: " + (result.message || "Unknown error"));
            }
        })
        .catch(error => {
            console.error("Request sending error:", error);
            alert("Data sending error");
        });
}
async function submitRegistration() {
    console.log("Function submitRegistration executed");

    const id = parseInt(document.getElementById("UserId").value) || 0;
    const role = document.getElementById("UserRole").value.trim();
    const username = document.getElementById("UserUsername").value.trim();
    const password = document.getElementById("UserPassword").value.trim();
    const email = document.getElementById("UserEmail").value.trim();

    if (!username) {
        alert("Error: Username cant be empty");
        return;
    }
    if (!password) {
        alert("Error: Password cant be empty");
        return;
    }

    const user = {
        Id: id, Username: username, Password: password, Role: role, Email: email
    };

    console.log("Sending data:", user);

    fetch("/User/SubmitRegistration", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(user)
    })
    .then(response => response.json())
    .then(result => {
        console.log("Server response:", result);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Login error: " + (result.message || "Unknown error"));
        }
    })
    .catch(error => {
        console.error("Request sending error:", error);
        alert("Data sending error");
    });
}

async function submitPasswordReset() {
    console.log("Function submitPasswordReset executed");
    
    const email = document.getElementById("UserEmail").value.trim();

    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!emailPattern.test(email)) {
        alert("Please enter a valid email address.");
        return;
    }

    if (!email) {
        alert("Error: Username cant be empty");
        return;
    }

    console.log("Sending data:", email);

    fetch("/User/SubmitPasswordReset", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(email)
    })
    .then(response => response.json())
    .then(result => {
        console.log("Server response:", result);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Login error: " + (result.message || "Unknown error"));
        }
    })
    .catch(error => {
        console.error("Request sending error:", error);
        alert("Data sending error");
    });
}
async function logout() {
    await fetch("/User/Logout", { method: "POST" });
    window.location.href = "/";
}
function RedirectToDeletedExpenses() {
    window.location.href = "/User/DeletedExpenses";
}