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
    await i18n.loadLanguage('en'); // Default to English
});
