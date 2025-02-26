function submitExpense() {
    console.log("Функция submitExpense вызвана");

    const id = parseInt($("#ExpenseId").val()) || 0;
    const value = parseFloat($("#ExpenseValue").val()) || 0;
    const description = $("#ExpenseDescription").val().trim();

    if (!description) {
        alert("Ошибка: описание не может быть пустым!");
        return;
    }

    const expense = { Id: id, Value: value, Description: description };

    console.log("Отправка данных:", expense);

    $.ajax({
        url: "/Home/CreateExpenseView",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(expense),
        success: function (result) {
            console.log("Ответ сервера:", result);

            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Ошибка при сохранении: " + (result.message || "Неизвестная ошибка"));
            }
        },
        error: function (xhr, status, error) {
            console.error("Ошибка при отправке запроса:", error);
            alert("Ошибка при отправке данных!");
        }
    });
}

function updateExpense() {
    console.log("Функция updateExpense вызвана");

    let id = $("#ExpenseId").val();
    let value = $("#ExpenseValue").val();
    let description = $("#ExpenseDescription").val();

    if (!id || !value || !description) {
        alert("Ошибка: заполните все поля!");
        return;
    }

    let expense = {
        Id: parseInt(id),
        Value: parseFloat(value),
        Description: description.trim()
    };

    console.log("Отправка данных на сервер:", expense);

    $.ajax({
        url: "/Home/EditExpense/" + id, // ID передаём в URL // if same name with http atribute arg, it works - ???
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify(expense),
        success: function (result) {
            console.log("Ответ сервера:", result);
            if (result.success) {
                window.location.href = result.redirectUrl;
            } else {
                alert("Ошибка при обновлении: " + (result.message || "Неизвестная ошибка"));
            }
        },
        error: function (xhr, status, error) {
            console.error("Ошибка при отправке запроса:", error);
            alert("Ошибка при обновлении данных!");
        }
    });
}

function loadExpense(id) {
    console.log("Загрузка расхода с ID:", id);

    $.ajax({
        url: "/Home/GetExpense/" + id, // API-запрос на получение данных
        type: "GET",
        success: function (expense) {
            console.log("Данные получены:", expense);

            if (!expense) {
                alert("Ошибка: Данные не найдены!");
                return;
            }

        },
        error: function (xhr, status, error) {
            console.error("Ошибка при загрузке данных:", error);
            alert("Ошибка при получении данных!");
        }
    });
}

function deleteExpense(id) {
    console.log("Функция deleteExpense вызвана");

    $.ajax({
        url: "/Home/DeleteExpense/" + id,
        type: "DELETE",
        success: function (response) {
            console.log("Deleting expense with id:", id);

            if (response.success) {
                window.location.href = response.redirectUrl;
            } else {
                alert("Ошибка при удалении: " + (response.message || "Неизвестная ошибка"));
            }
        },
        error: function (xhr, status, error) {
            console.error("Ошибка при удалении:", error);
            alert("Ошибка при удалении данных!");
        }
    });
}
