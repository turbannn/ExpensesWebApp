async function Login() {
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

    fetch("/Home/SubmitLogin", {
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
function LoginRedirection() {
    window.location.href = "/Home/TryLogin";
}

function RedirectToRegistration() {
    window.location.href = "/User/Register";
}