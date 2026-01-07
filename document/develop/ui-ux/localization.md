# Localization System

## Overview

The BerryAIGen.Toolkit application implements a comprehensive localization system that supports multiple languages. This document describes the localization architecture, how translations are managed, and how to extend or modify the localization system.

## Core Implementation

### Location

The localization system implementation is located in `BerryAIGen.Toolkit/Localization/` directory.

### Key Components

#### 1. Resource Files

- **Strings.resx**: Default English resource file
- **Strings.[culture].resx**: Language-specific resource files (e.g., Strings.fr.resx for French)
- **Strings.designer.cs**: Auto-generated strongly-typed resource accessor

#### 2. Localization Manager

The `LocalizationManager` class manages language selection and translation retrieval:

```csharp
public class LocalizationManager
{
    public static void ChangeLanguage(string culture)
    {
        // Implementation for changing application language
    }
    
    public static string GetTranslation(string key)
    {
        // Implementation for retrieving translations
    }
    
    // Additional localization management methods
}
```

## Localization Architecture

### Resource File Structure

Each resource file follows a consistent structure with key-value pairs:

| Key | Value |
|-----|-------|
| App_Title | BerryAIGen.Toolkit |
| Menu_File | File |
| Menu_Edit | Edit |
| Button_Search | Search |
| Label_Filter | Filter: |

### Supported Languages

The application currently supports the following languages:

| Language | Culture Code | Resource File |
|----------|--------------|---------------|
| English | en | Strings.resx |
| French | fr | Strings.fr.resx |
| German | de | Strings.de.resx |
| Japanese | ja | Strings.ja.resx |
| Simplified Chinese | zh-CN | Strings.zh-CN.resx |
| Spanish | es | Strings.es.resx |

### Language Detection

#### System Language Detection

- The application automatically detects the system language on startup
- It checks for available resource files matching the system culture
- Falls back to English if the system language is not supported

#### Manual Language Selection

- Users can manually select their preferred language in the settings
- Available options: System, English, French, German, Japanese, Simplified Chinese, Spanish
- Language changes are applied immediately without restarting the application

## Localization in XAML

### Using Resource Keys

In XAML files, translations are referenced using resource keys:

```xaml
<!-- Example: Button with localized text -->
<Button Content="{x:Static local:Strings.Button_Search}" />

<!-- Example: Label with localized text -->
<Label Content="{x:Static local:Strings.Label_Filter}" />

<!-- Example: Menu item with localized text -->
<MenuItem Header="{x:Static local:Strings.Menu_File}" />
```

### Using Dynamic Localization

For dynamic content that needs to update when the language changes:

```xaml
<!-- Example: TextBlock with dynamic localization -->
<TextBlock Text="{Binding Source={x:Static local:Strings}, Path=App_Title}" />
```

## Localization in Code

### Accessing Translations

In C# code, translations are accessed through the strongly-typed resource manager:

```csharp
// Example: Getting a translation in code
string searchButtonText = Strings.Button_Search;
string filterLabel = Strings.Label_Filter;

// Example: Setting control text from code
searchButton.Content = Strings.Button_Search;
```

### Changing Language at Runtime

```csharp
// Example: Changing language in code
LocalizationManager.ChangeLanguage("fr"); // Switch to French
LocalizationManager.ChangeLanguage("en"); // Switch to English
```

## Adding New Languages

### Step 1: Create Resource File

1. In the `Localization` directory, create a new resource file named `Strings.[culture].resx`
2. For example: `Strings.it.resx` for Italian
3. Set the Access Modifier to `Public` in the resource designer

### Step 2: Add Translations

1. Open the new resource file in Visual Studio
2. Add translations for all keys from the default `Strings.resx` file
3. Ensure consistency in translation quality and style

### Step 3: Update Language Selection

1. Update the language selection UI in the settings page
2. Add the new language to the available options
3. Update any language-related enums or constants

### Step 4: Test

1. Switch to the new language in the settings
2. Verify that all UI elements display the correct translations
3. Test edge cases and ensure fallback behavior works correctly

## Best Practices for Localization

### 1. Use Descriptive Resource Keys

- Use clear, descriptive resource keys that indicate their purpose
- Good: `Button_Search`, `Label_Filter`, `Menu_File`
- Bad: `String1`, `Text2`, `Btn3`

### 2. Avoid Hard-Coded Text

- Never hard-code text in XAML or code
- Always use resource keys for all user-facing text
- Use the strongly-typed resource manager for type safety

### 3. Consider UI Layout

- Design UI elements with enough space for longer translations
- Avoid fixed-width containers that may truncate text in some languages
- Test layouts with languages that read from right to left (RTL) if supported

### 4. Use Plural Forms Appropriately

- Be aware that different languages have different pluralization rules
- Use appropriate formatting for numbers, dates, and currencies
- Consider using .NET's formatting capabilities for dynamic content

### 5. Maintain Consistency

- Keep translation terminology consistent across the application
- Use the same resource keys for similar concepts
- Ensure a consistent tone and style for all translations

### 6. Test Thoroughly

- Test all UI elements in all supported languages
- Verify that dynamic content updates correctly when language changes
- Test edge cases and error messages
- Get feedback from native speakers if possible

## Localization Testing

### Manual Testing

1. Switch between all supported languages
2. Verify that all UI elements display correctly
3. Check for truncated text or layout issues
4. Ensure that dynamic content updates properly

### Automated Testing

- Implement unit tests to verify that all resource keys are present in all languages
- Test that the fallback mechanism works correctly
- Verify that language changes are applied consistently

## Performance Considerations

- Resource files are compiled into satellite assemblies for efficient loading
- The strongly-typed resource manager provides fast access to translations
- Language changes at runtime are optimized to minimize performance impact
- Resource files are loaded on-demand when needed

## Conclusion

The localization system in BerryAIGen.Toolkit provides a robust and extensible framework for supporting multiple languages. It follows best practices for localization, including descriptive resource keys, consistent terminology, and thorough testing. The system is designed to be easy to extend, allowing new languages to be added with minimal effort. By following the guidelines outlined in this document, developers can ensure that the application remains accessible to users worldwide in their preferred languages.
