// Localization manager for the Electron application

class LocalizationManager {
    constructor() {
        this.currentLanguage = 'en';
        this.translations = {};
        this.defaultLanguage = 'en';
    }

    // Load translations from JSON file
    async loadLanguage(languageCode) {
        try {
            const response = await fetch(`/lang/${languageCode}.json`);
            if (!response.ok) {
                throw new Error(`Failed to load language file for ${languageCode}`);
            }
            this.translations[languageCode] = await response.json();
            this.currentLanguage = languageCode;
            this.applyTranslations();
        } catch (error) {
            console.error(`Error loading language ${languageCode}:`, error);
            // Fallback to default language if current language fails
            if (languageCode !== this.defaultLanguage) {
                this.loadLanguage(this.defaultLanguage);
            }
        }
    }

    // Translate a key to current language
    t(key) {
        const keys = key.split('.');
        let value = this.translations[this.currentLanguage];
        
        for (const k of keys) {
            if (value && typeof value === 'object' && k in value) {
                value = value[k];
            } else {
                // Fallback to default language if key not found
                value = this.translations[this.defaultLanguage];
                for (const k2 of keys) {
                    if (value && typeof value === 'object' && k2 in value) {
                        value = value[k2];
                    } else {
                        return key; // Return original key if not found in either language
                    }
                }
                break;
            }
        }
        
        return typeof value === 'string' ? value : key;
    }

    // Apply translations to all elements with data-i18n attribute
    applyTranslations() {
        const elements = document.querySelectorAll('[data-i18n]');
        elements.forEach(element => {
            const key = element.getAttribute('data-i18n');
            const translation = this.t(key);
            
            // Handle different element types
            if (element.tagName === 'INPUT' || element.tagName === 'TEXTAREA') {
                element.placeholder = translation;
            } else if (element.tagName === 'IMG') {
                element.alt = translation;
            } else {
                element.textContent = translation;
            }
        });
    }

    // Set current language and reload translations
    setLanguage(languageCode) {
        return this.loadLanguage(languageCode);
    }
}

// Create global instance
const i18n = new LocalizationManager();

// Initialize localization on page load
document.addEventListener('DOMContentLoaded', async () => {
    // Load saved language from localStorage or default to 'en'
    const savedLanguage = localStorage.getItem('language') || 'en';
    await i18n.loadLanguage(savedLanguage); // Default to English
    
    // Setup language switcher
    setupLanguageSwitcher();
});

// Setup language switcher functionality
function setupLanguageSwitcher() {
    // Add language switcher dropdown to toolbar
    const toolbarRight = document.querySelector('.toolbar-right');
    if (toolbarRight) {
        const languageSwitcher = createLanguageSwitcher();
        toolbarRight.appendChild(languageSwitcher);
        
        // Load language files from the lang directory
        loadAvailableLanguages();
    }
}

// Create language switcher dropdown
function createLanguageSwitcher() {
    const switcherContainer = document.createElement('div');
    switcherContainer.className = 'language-switcher';
    
    const switcherButton = document.createElement('button');
    switcherButton.className = 'toolbar-btn';
    switcherButton.setAttribute('aria-label', 'Switch Language');
    switcherButton.innerHTML = `<span class="toolbar-icon">🌐</span>`;
    
    const dropdownMenu = document.createElement('div');
    dropdownMenu.className = 'language-dropdown';
    dropdownMenu.style.display = 'none';
    
    switcherContainer.appendChild(switcherButton);
    switcherContainer.appendChild(dropdownMenu);
    
    // Toggle dropdown on button click
    switcherButton.addEventListener('click', () => {
        dropdownMenu.style.display = dropdownMenu.style.display === 'none' ? 'block' : 'none';
    });
    
    // Close dropdown when clicking outside
    document.addEventListener('click', (e) => {
        if (!switcherContainer.contains(e.target)) {
            dropdownMenu.style.display = 'none';
        }
    });
    
    // Add CSS for language switcher
    addLanguageSwitcherStyles();
    
    return switcherContainer;
}

// Load available languages from the lang directory
async function loadAvailableLanguages() {
    try {
        // Get list of language files from the server
        // For simplicity, we'll hardcode the available languages
        const availableLanguages = [
            { code: 'en', name: 'English' },
            { code: 'de', name: 'Deutsch' },
            { code: 'fr', name: 'Français' },
            { code: 'es', name: 'Español' },
            { code: 'ja', name: '日本語' },
            { code: 'zh-CN', name: '简体中文' },
            { code: 'zh-TW', name: '繁體中文' }
        ];
        
        const dropdownMenu = document.querySelector('.language-dropdown');
        if (dropdownMenu) {
            // Clear existing options
            dropdownMenu.innerHTML = '';
            
            // Add language options to dropdown
            availableLanguages.forEach(language => {
                const option = document.createElement('button');
                option.className = 'language-option';
                option.textContent = language.name;
                option.dataset.languageCode = language.code;
                
                // Set current language as active
                if (language.code === i18n.currentLanguage) {
                    option.classList.add('active');
                }
                
                // Add click event to switch language
                option.addEventListener('click', async () => {
                    await i18n.setLanguage(language.code);
                    localStorage.setItem('language', language.code);
                    
                    // Update active language in dropdown
                    document.querySelectorAll('.language-option').forEach(opt => {
                        opt.classList.remove('active');
                    });
                    option.classList.add('active');
                    
                    // Close dropdown
                    dropdownMenu.style.display = 'none';
                });
                
                dropdownMenu.appendChild(option);
            });
        }
    } catch (error) {
        console.error('Error loading available languages:', error);
    }
}

// Add CSS for language switcher
function addLanguageSwitcherStyles() {
    const style = document.createElement('style');
    style.textContent = `
        .language-switcher {
            position: relative;
            display: inline-block;
        }
        
        .language-dropdown {
            position: absolute;
            top: 100%;
            right: 0;
            margin-top: 8px;
            background-color: var(--bg-secondary);
            border: 1px solid var(--color-border);
            border-radius: var(--border-radius-sm);
            box-shadow: var(--shadow-md);
            z-index: 1000;
            min-width: 120px;
            overflow: hidden;
        }
        
        .language-option {
            display: block;
            width: 100%;
            padding: 8px 16px;
            background-color: transparent;
            border: none;
            color: var(--color-text);
            font-size: 14px;
            cursor: pointer;
            text-align: left;
            transition: background-color var(--transition-fast);
        }
        
        .language-option:hover {
            background-color: var(--bg-tertiary);
        }
        
        .language-option.active {
            background-color: var(--bg-button);
            color: white;
        }
    `;
    document.head.appendChild(style);
}
