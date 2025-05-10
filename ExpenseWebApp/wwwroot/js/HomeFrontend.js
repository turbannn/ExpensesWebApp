function LoginRedirection(page = 1, pageSize = 10) {
    const url = `/Home/TryLogin?page=${page}&pageSize=${pageSize}`;
    window.location.href = url;
}

function RedirectToRegistration() {
    window.location.href = "/Home/Register";
}