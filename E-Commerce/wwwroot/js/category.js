document.addEventListener("DOMContentLoaded", function () {

    const input = document.getElementById("searchInput");

    if (input) {
        input.addEventListener("keyup", function () {
            let filter = input.value.toLowerCase();
            let rows = document.querySelectorAll("#categoryTable tbody tr");

            rows.forEach(row => {
                let text = row.innerText.toLowerCase();
                row.style.display = text.includes(filter) ? "" : "none";
            });
        });
    }

});