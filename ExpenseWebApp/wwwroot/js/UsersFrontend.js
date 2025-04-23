async function submitRegistration() {
    console.log("Function submitRegistration executed");

    const id = parseInt(document.getElementById("UserId").value) || 0;
    const role = document.getElementById("UserRole").value.trim();
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
        Id: id, Username: username, Password: password, Role: role
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