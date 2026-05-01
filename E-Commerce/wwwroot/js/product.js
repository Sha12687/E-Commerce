
    //document.getElementById("imageInput").addEventListener("change", function (event) {
    //    var file = event.target.files[0];

    //if (file) {
    //        var reader = new FileReader();

    //reader.onload = function (e) {
    //    document.getElementById("smallPreview").src = e.target.result;
   
    //        };

    //reader.readAsDataURL(file);
    //    }
    //});
document.addEventListener("DOMContentLoaded", function () {
    const input = document.getElementById("imageInput");
    const preview = document.getElementById("preview");

    if (!input || !preview) {
        console.log("Input or preview not found");
        return;
    }

    input.addEventListener("change", function (e) {
        const file = e.target.files[0];

        if (!file) return;

        console.log("File selected:", file.name);

        // Method 1 (recommended)
        preview.src = URL.createObjectURL(file);

        // Optional: clean memory later
        preview.onload = () => {
            URL.revokeObjectURL(preview.src);
        };
    });
});