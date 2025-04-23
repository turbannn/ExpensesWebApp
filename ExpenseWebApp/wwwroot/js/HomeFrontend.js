async function Login() {
    console.log("Function submitUser executed");

    const id = parseInt(document.getElementById("UserId").value) || 0;
    const username = document.getElementById("UserUsername").value.trim();
    const password = document.getElementById("UserPassword").value.trim();

    if (!username) {
        alert("Error: Value cant be empty");
        return;
    }
    if (!password) {
        alert("Error: Description cant be empty");
        return;
    }

    const user = {
        Id: id, Username: username, Password: password
    };

    console.log("Sending data:", user);

    fetch("/Home/SubmitLogin", {
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
function LoginRedirection() {
    window.location.href = "/Home/TryLogin";
}

function RedirectToRegistration() {
    window.location.href = "/User/Register";
}