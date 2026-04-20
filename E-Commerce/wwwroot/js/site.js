// Sidebar toggle
document.getElementById("SidebarToggle")
    .addEventListener("click", function () {
        document.getElementById("wrapper").classList.toggle("toggled");
    });

// Dynamic year
document.getElementById("year").innerText = new Date().getFullYear();