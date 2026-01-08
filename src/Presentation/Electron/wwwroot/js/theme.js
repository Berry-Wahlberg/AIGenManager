// Theme management functionality

// Theme modes: 'light', 'dark', 'system'
let currentTheme = localStorage.getItem('theme') || 'system';

// Initialize theme on page load
function initializeTheme() {
    applyTheme(currentTheme);
    setupThemeToggle();
}

// Apply the specified theme
function applyTheme(theme) {
    currentTheme = theme;
    localStorage.setItem('theme', theme);
    
    if (theme === 'system') {
        // Detect system theme
        const systemPrefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
        document.documentElement.setAttribute('data-theme', systemPrefersDark ? 'dark' : 'light');
    } else {
        document.documentElement.setAttribute('data-theme', theme);
    }
    
    updateThemeToggleDisplay();
}

// Setup theme toggle button
function setupThemeToggle() {
    const themeToggle = document.getElementById('theme-toggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', () => {
            // Cycle through themes: light → dark → system → light
            const nextTheme = currentTheme === 'light' ? 'dark' : 
                            currentTheme === 'dark' ? 'system' : 'light';
            applyTheme(nextTheme);
        });
        
        // Update display on initial load
        updateThemeToggleDisplay();
    }
    
    // Listen for system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
        if (currentTheme === 'system') {
            applyTheme('system');
        }
    });
}

// Update theme toggle button display
function updateThemeToggleDisplay() {
    const themeToggle = document.getElementById('theme-toggle');
    if (!themeToggle) return;
    
    // Remove all theme classes
    themeToggle.classList.remove('theme-light', 'theme-dark', 'theme-system');
    
    // Add current theme class
    themeToggle.classList.add(`theme-${currentTheme}`);
    
    // Update aria-label
    const themeLabels = {
        light: 'Switch to dark theme',
        dark: 'Switch to system theme',
        system: 'Switch to light theme'
    };
    themeToggle.setAttribute('aria-label', themeLabels[currentTheme]);
    
    // Update tooltip text
    const tooltip = themeToggle.querySelector('.tooltip');
    if (tooltip) {
        const tooltipTexts = {
            light: 'Light Theme',
            dark: 'Dark Theme',
            system: 'System Theme'
        };
        tooltip.textContent = tooltipTexts[currentTheme];
    }
}

// Initialize theme when DOM is loaded
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', initializeTheme);
} else {
    initializeTheme();
}
