function confirmDelete() {
    return confirm("Are you sure you want to delete?");
}
//document.addEventListener("DOMContentLoaded", () => {
//    const editButtons = document.querySelectorAll(".edit-btn");

//    editButtons.forEach((button) => {
//        button.addEventListener("click", (event) => {
//            event.preventDefault();

//            const form = button.closest("form");
//            const input = form.querySelector('input[name="UpdatedGenreName"]');
//            const cancelButton = form.querySelector('.cancel-btn');
//            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

            
//            input.style.display = "inline-block";
//            input.focus();
//            cancelButton.style.display = "inline-block";
//            deleteForm.style.display = "none";
//            button.innerHTML = '<i class="fas fa-save"></i> Save';
//            button.classList.add("save-mode");

            
//            button.addEventListener("click", () => {
//                form.submit();
//            });
//        });
//    });

//    const cancelButtons = document.querySelectorAll(".cancel-btn");

//    cancelButtons.forEach((button) => {
//        button.addEventListener("click", (event) => {
//            event.preventDefault();

            
//            const form = button.closest("form");
//            const input = form.querySelector('input[name="UpdatedGenreName"]');
//            const editButton = form.querySelector('.edit-btn');
//            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

//            input.style.display = "none";
//            input.value = input.defaultValue; 
//            deleteForm.style.display = "block";
//            button.style.display = "none";
//            editButton.innerHTML = '<i class="fas fa-edit"></i> Edit';
//            editButton.classList.remove("save-mode");
//        });
//    });
//});
document.addEventListener("DOMContentLoaded", () => {
    const editButtons = document.querySelectorAll(".edit-btn");

    editButtons.forEach((button) => {
        button.addEventListener("click", (event) => {
            event.preventDefault();

            const form = button.closest("form");
            const inputs = form.querySelectorAll('input');  // Select all input elements
            const cancelButton = form.querySelector('.cancel-btn');
            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

            // Show all input fields and set them to editable
            inputs.forEach(input => {
                input.style.display = "inline-block";  // Show each input
            });

            cancelButton.style.display = "inline-block";
            deleteForm.style.display = "none";
            button.innerHTML = '<i class="fas fa-save"></i> Save';
            button.classList.add("save-mode");

            // Listen for the button click again to submit the form
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
            const inputs = form.querySelectorAll('input');  // Select all input elements
            const editButton = form.querySelector('.edit-btn');
            const deleteForm = document.getElementById(`delete-form-${button.id.split('-')[2]}`);

            // Hide all input fields and reset them to their default values
            inputs.forEach(input => {
                input.style.display = "none";  // Hide the input
                input.value = input.defaultValue;  // Reset to default value
            });

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
    form.submit(); 
}
function clearForm() {
    const form = document.getElementById('add-book-form');
    form.reset();
}

let slideIndex = 0;
const slides = document.querySelectorAll(".slide");
function showSlide(index) {
    if (index >= slides.length) {
        slideIndex = 0;
    } else if (index < 0) {
        slideIndex = slides.length - 1;
    } else {
        slideIndex = index;
    }
    document.querySelector(".slides").style.transform = `translateX(-${slideIndex * 100
        }%)`;
}

function changeSlide(direction) {
    showSlide(slideIndex + direction);
}
setInterval(() => {
    changeSlide(1);
}, 8000);

