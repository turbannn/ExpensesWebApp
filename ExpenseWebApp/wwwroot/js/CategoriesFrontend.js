async function createCategory() {
    console.log("Function createCategory executed");

    const id = 0;
    const name = document.getElementById("CategoryName").value;

    if (!name) {
        alert("Error: name cant be empty");
        return;
    }

    const category = { Id: id, Name: name };

    console.log("Sending data:", category);

    try {
        const response = await fetch("/Category/CreateCategory", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(category)
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

async function updateCategory() {
    console.log("Function updateCategory executed");

    let id = document.getElementById("CategoryId").value;
    let name = document.getElementById("CategoryName").value;

    if (!id || !name) {
        alert("Error: fill all fields");
        return;
    }

    let category = {
        Id: parseInt(id),
        Name: name
    };

    console.log("Sending data to server:", category);

    try {
        const response = await fetch(`/Category/EditCategory/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(category)
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

async function deleteCategory(id) {
    console.log("Function deleteCategory executed");

    try {
        const response = await fetch(`/Category/DeleteCategory/${id}`, { method: "DELETE" });
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

function RedirectToCategoryCreation() {
    window.location.href = "/Category/CreateCategory";
}
function RedirectToCategoryEditing(categoryId) {
    window.location.href = `/Category/EditCategory/${categoryId}`;
}