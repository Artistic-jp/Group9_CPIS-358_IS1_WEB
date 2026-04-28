/* =========================================
   KAU Restaurant - External JavaScript
   ========================================= */


// Image Slideshow Data

const slides = [
    { image: "images/res-main.png", text: "Your destination for quality meals and convenient ordering" },
    { image: "images/res-main.png", text: "Fresh meals prepared daily" },
    { image: "images/res-main.png", text: "Quick service for busy students" }
];
let currentSlide = 0;


// Slideshow Function - Changes hero image every 5 seconds
// Satisfies: Dynamic Picture & Text (Image Slider)
function changeSlide() {
    currentSlide = (currentSlide + 1) % slides.length;
    const hero = document.querySelector('.hero');
    const heroText = document.querySelector('.hero-text p');
    
    if (hero) {
        hero.style.backgroundImage = `url('${slides[currentSlide].image}')`;
    }
    if (heroText) {
        heroText.textContent = slides[currentSlide].text;
    }
}



// High Contrast Mode
// Satisfies: Change CSS Styles using script

function toggleHighContrast() {
    const body = document.body;
    const header = document.querySelector('header');
    const nav = document.querySelector('nav');
    const container = document.querySelector('.container');
    
    // Check current state
    if (body.classList.contains('high-contrast')) {
        // Remove high contrast mode
        body.classList.remove('high-contrast');
        if (header) header.style.backgroundColor = 'var(--leaf-green)';
        if (nav) nav.style.backgroundColor = 'var(--earth-brown)';
        if (container) {
            container.style.backgroundColor = 'var(--white)';
            container.style.color = 'var(--dark-text)';
        }
    } else {
        // Add high contrast mode
        body.classList.add('high-contrast');
        if (header) header.style.backgroundColor = '#000000';
        if (nav) nav.style.backgroundColor = '#333333';
        if (container) {
            container.style.backgroundColor = '#1a1a1a';
            container.style.color = '#ffffff';
        }
    }
}

// Form Validation - Login Page
// Satisfies: Form Validation with if statements
function validateLoginForm(event) {
    event.preventDefault(); // Stop form from submitting
    
    const studentId = document.getElementById('student-id');
    const password = document.getElementById('password');
    const errorDiv = document.getElementById('error-message');
    
    // Clear previous error
    if (errorDiv) {
        errorDiv.textContent = '';
        errorDiv.style.color = 'red';
    }
    
    // Validate Student ID (must be exactly 7 digits)
    if (!studentId.value || studentId.value.length !== 7) {
        showError('Student ID must be exactly 7 digits');
        studentId.focus();
        return false;
    }
    
    // Check if all digits
    if (isNaN(studentId.value)) {
        showError('Student ID must contain only numbers');
        studentId.focus();
        return false;
    }
    
    // Validate Password (must not be empty)
    if (!password.value || password.value.length === 0) {
        showError('Please enter your password');
        password.focus();
        return false;
    }
    
    // Success - in real app, submit to server
    showSuccess('Login successful! Redirecting...');
    return true;
}


// Show Error Message

function showError(message) {
    const errorDiv = document.getElementById('error-message');
    if (errorDiv) {
        errorDiv.textContent = message;
        errorDiv.style.color = 'red';
    } else {
        alert(message);
    }
}


// Show Success Message
function showSuccess(message) {
    const errorDiv = document.getElementById('error-message');
    if (errorDiv) {
        errorDiv.textContent = message;
        errorDiv.style.color = 'green';
    }
}


// Initialize functions when page loads
function initializePage() {
    // Start slideshow only if hero exists (home page)
    if (document.querySelector('.hero')) {
        setInterval(changeSlide, 5000);
        displayWelcomeMessage();
    }
}

// Run initialization when DOM is ready
document.addEventListener('DOMContentLoaded', initializePage);