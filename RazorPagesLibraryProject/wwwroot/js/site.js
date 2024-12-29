
function confirmDelete() {
    return confirm("Are you sure you want to delete this genre?");
}
document.addEventListener("DOMContentLoaded", () => {
    const editButtons = document.querySelectorAll(".edit-btn");

    editButtons.forEach((button) => {
        button.addEventListener("click", (event) => {
            event.preventDefault();

            const form = button.closest("form");
            const input = form.querySelector('input[name="UpdatedGenreName"]');
            const cancelButton = form.querySelector('.cancel-btn');
            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

            
            input.style.display = "inline-block";
            input.focus();
            cancelButton.style.display = "inline-block";
            deleteForm.style.display = "none";
            button.innerHTML = '<i class="fas fa-save"></i> Save';
            button.classList.add("save-mode");

            
            button.addEventListener("click", () => {
                form.submit();
            });
        });
    });

    const cancelButtons = document.querySelectorAll(".cancel-btn");

    cancelButtons.forEach((button) => {
        button.addEventListener("click", (event) => {
            event.preventDefault();

            
            const form = button.closest("form");
            const input = form.querySelector('input[name="UpdatedGenreName"]');
            const editButton = form.querySelector('.edit-btn');
            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

            input.style.display = "none";
            input.value = input.defaultValue; 
            deleteForm.style.display = "block";
            button.style.display = "none";
            editButton.innerHTML = '<i class="fas fa-edit"></i> Edit';
            editButton.classList.remove("save-mode");
        });
    });
});

function clearSearch() {
    const form = document.getElementById('search-form');
    const input = form.querySelector('input[name="SearchWord"]');
    input.value = '';
    form.submit(); // Submit the form to trigger OnGet
}