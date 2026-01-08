// Navigation collapse logic for the sidebar

// DOM Elements
const sidebar = document.getElementById('sidebar');
const toggleBtn = document.getElementById('toggle-btn');

// Constants for responsive behavior
const COLLAPSED_WIDTH = 50;
const EXPANDED_MAX_WIDTH = 240;
const COLLAPSE_THRESHOLD = 800;

// Toggle navigation bar function
function toggleNavigation() {
    sidebar.classList.toggle('collapsed');
    
    // Update toggle icon
    const toggleIcon = toggleBtn.querySelector('.toggle-icon');
    if (sidebar.classList.contains('collapsed')) {
        toggleIcon.textContent = '☰';
    } else {
        toggleIcon.textContent = '☰';
    }
}

// Function to handle window resize events
function handleWindowResize() {
    const windowWidth = window.innerWidth;
    
    if (windowWidth < COLLAPSE_THRESHOLD) {
        // Collapse sidebar when window is too narrow
        sidebar.classList.add('collapsed');
    } else {
        // Expand sidebar when window is wide enough
        sidebar.classList.remove('collapsed');
    }
}

// Initialize navigation functionality
document.addEventListener('DOMContentLoaded', function() {
    // Add event listener for toggle button
    toggleBtn.addEventListener('click', toggleNavigation);
    
    // Add event listener for window resize
    window.addEventListener('resize', handleWindowResize);
    
    // Initial check on page load
    handleWindowResize();
});
