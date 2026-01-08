// Navigation collapse logic for the sidebar

// DOM Elements
const sidebar = document.getElementById('sidebar');
const toggleBtn = document.getElementById('toggle-sidebar');

// Constants for responsive behavior
const COLLAPSE_THRESHOLD = 800;

// Toggle navigation bar function
function toggleNavigation() {
    sidebar.classList.toggle('collapsed');
    
    // Update toggle icon based on state
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

// Function to update active navigation link based on current URL
function updateActiveNavLink() {
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.nav-link');
    
    navLinks.forEach(link => {
        link.classList.remove('active');
        
        const linkPath = new URL(link.href).pathname;
        
        if (currentPath === linkPath || 
            (currentPath === '/' && linkPath === '/') ||
            (linkPath !== '/' && currentPath.startsWith(linkPath))) {
            link.classList.add('active');
        }
    });
}

// Initialize navigation functionality
document.addEventListener('DOMContentLoaded', function() {
    // Add event listener for toggle button if it exists
    if (toggleBtn) {
        toggleBtn.addEventListener('click', toggleNavigation);
    }
    
    // Add event listener for window resize
    window.addEventListener('resize', handleWindowResize);
    
    // Update active nav link
    updateActiveNavLink();
    
    // Initial check on page load
    handleWindowResize();
    
    // Add event listeners for navigation links to handle SPA-like navigation
    document.querySelectorAll('.nav-link').forEach(link => {
        link.addEventListener('click', function(e) {
            // For external links, let them navigate normally
            if (this.target === '_blank' || this.href.startsWith('http')) {
                return;
            }
            
            e.preventDefault();
            
            // Update the URL without refreshing the page
            const url = this.href;
            window.history.pushState({}, '', url);
            
            // Update active nav link
            updateActiveNavLink();
            
            // Here you would typically load the new content via AJAX
            console.log('Navigating to:', url);
        });
    });
    
    // Handle popstate events for back/forward navigation
    window.addEventListener('popstate', updateActiveNavLink);
});
