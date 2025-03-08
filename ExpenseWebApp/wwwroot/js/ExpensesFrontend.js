async function submitExpense() {
    console.log("Function submitExpense executed");

    const id = parseInt(document.getElementById("ExpenseId").value) || 0;
    const value = parseFloat(document.getElementById("ExpenseValue").value) || 0;
    const description = document.getElementById("ExpenseDescription").value.trim();

    if (!description) {
        alert("Error: Description cant be empty");
        return;
    }

    const expense = { Id: id, Value: value, Description: description };

    console.log("Sending data:", expense);

    try {
        const response = await fetch("/Home/CreateExpenseView", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(expense)
        });

        const result = await response.json();
        console.log("Server response:", result);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Save err: " + (result.message || "Unknown issue"));
        }
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data sending error");
    }
}

async function updateExpense() {
    console.log("Function updateExpense executed");

    let id = document.getElementById("ExpenseId").value;
    let value = document.getElementById("ExpenseValue").value;
    let description = document.getElementById("ExpenseDescription").value;

    if (!id || !value || !description) {
        alert("Error: fill all fields");
        return;
    }

    let expense = {
        Id: parseInt(id),
        Value: parseFloat(value),
        Description: description.trim()
    };

    console.log("Sending data to server:", expense);

    try {
        const response = await fetch(`/Home/EditExpense/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(expense)
        });

        const result = await response.json();
        console.log("Server response:", result);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Update error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data update error");
    }
}

async function loadExpense(id) {
    console.log("Loading expense with ID:", id);

    try {
        const response = await fetch(`/Home/GetExpense/${id}`);
        if (!response.ok) throw new Error("Data loading error");

        const expense = await response.json();
        console.log("Data received:", expense);

        if (!expense) {
            alert("Error: Data not found");
            return;
        }

        document.getElementById("ExpenseId").value = expense.id;
        document.getElementById("ExpenseValue").value = expense.value;
        document.getElementById("ExpenseDescription").value = expense.description;
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data update error");
    }
}

async function deleteExpense(id) {
    console.log("Function deleteExpense executed");

    try {
        const response = await fetch(`/Home/DeleteExpense/${id}`, { method: "DELETE" });
        const result = await response.json();

        console.log("Deleting expense with id:", id);

        if (result.success) {
            window.location.href = result.redirectUrl;
        } else {
            alert("Delete error: " + (result.message || "Unknown error"));
        }
    } catch (error) {
        console.error("Request sending error:", error);
        alert("Data update error");
    }
}
