# BerryAIGen.Scripting Module

## Overview

The BerryAIGen.Scripting module is a lightweight scripting support module for the BerryAIGen.Toolkit application. Currently in an early stage of development, this module is designed to provide scripting capabilities for automating tasks, generating prompts, and extending the functionality of the application.

## Core Implementation

### Location
The BerryAIGen.Scripting module is located in the `BerryAIGen.Scripting` directory.

### Current State

As of the current implementation, the module contains:
- A minimal C# class structure (`Class1.cs`)
- A Python script demonstrating Jinja2 template rendering for prompt generation
- Basic project structure for future expansion

### Key Components

#### 1. Basic C# Class Structure

##### Purpose
The `Class1` class is a placeholder for future scripting functionality in C#.

##### Location
`BerryAIGen.Scripting/Class1.cs`

##### Class Structure

```csharp
public class Class1
{
    // Placeholder for future implementation
}
```

#### 2. Python Scripts

##### Purpose
The Python scripts in the `python` directory demonstrate potential scripting capabilities, particularly for prompt generation using Jinja2 templates.

##### Location
`BerryAIGen.Scripting/python/jinja.py`

##### Key Features

- **Jinja2 Template Support**: Uses Jinja2 templating engine for prompt generation
- **Dynamic Prompt Generation**: Demonstrates how to generate varied prompts from templates
- **Randomization Support**: Shows how to incorporate random choices in generated content

##### Example Script

```python
from jinja2 import Template

template_str = """score_9, score_8_up, score_7_up, score_6_up, brunette, medium breasts, {looking at viewer|}, 
{smiling|},
brunette, 
thighs,
nurse, stockings,  {lace bra|nude|uniform, white lab dress {lace bra|} {stethoscope|}},
{low angle,||}
{downblouse|{from behind, rear view|side view|}},
{plain background|dark background|},
"""

template = Template(template_str)

rendered = template.render()
print(rendered)
```

## Intended Functionality

While the module is currently minimal, it is designed to support the following functionality in future releases:

1. **Script Execution**: Ability to execute external scripts from within the application
2. **Prompt Generation**: Advanced prompt generation using templates and variables
3. **Automation**: Automating repetitive tasks like batch processing images
4. **Extensibility**: Allowing users to extend the application's functionality through scripts
5. **Multi-language Support**: Support for multiple scripting languages (Python, Lua, etc.)
6. **Integration Points**: Hooks for integrating scripts with the application's core functionality

## Usage Examples

### Prompt Generation (Future Implementation)

```csharp
// Example of how the scripting module might be used in the future
var scriptingService = ServiceLocator.ScriptingService;
var prompt = await scriptingService.GeneratePrompt("portrait_template.j2", new { subject = "cat", style = "watercolor" });
```

### Script Execution (Future Implementation)

```csharp
// Example of how script execution might work
var result = await scriptingService.ExecuteScriptAsync("process_images.py", new string[] { "--input", "folder1", "--output", "folder2" });
```

## Integration with BerryAIGen.Toolkit

The BerryAIGen.Scripting module is designed to integrate with the BerryAIGen.Toolkit application in the following ways:

1. **ServiceLocator Integration**: Accessible through the ServiceLocator pattern
2. **UI Integration**: Future UI components for script management and execution
3. **Event Hooks**: Integration with application events like image import or metadata extraction
4. **Configuration**: Script configuration through the application's settings

## Future Development Roadmap

The module is planned to evolve with the following features:

1. **Script Engine**: Implement a script execution engine supporting multiple languages
2. **Template System**: Advanced prompt templating with variables and functions
3. **API Exposure**: Expose application functionality to scripts through a well-defined API
4. **Script Repository**: Built-in repository for sharing and downloading scripts
5. **Debugging Tools**: Tools for debugging and testing scripts
6. **Documentation**: Comprehensive documentation for script developers

## Performance Considerations

When implementing the scripting module, the following performance considerations will be important:

- **Isolation**: Scripts should run in isolated environments to prevent crashes
- **Resource Limits**: Implement memory and CPU limits for scripts
- **Asynchronous Execution**: Scripts should execute asynchronously to avoid blocking the UI
- **Caching**: Cache compiled scripts for faster execution

## Security

Security will be a critical consideration for the scripting module:

- **Sandboxing**: Run scripts in a sandboxed environment
- **Permission System**: Implement a permission system for script capabilities
- **Validation**: Validate scripts before execution
- **Secure API**: Expose only safe API methods to scripts

## Conclusion

The BerryAIGen.Scripting module is currently in its early stages but has significant potential to enhance the functionality of the BerryAIGen.Toolkit application. By providing scripting capabilities, the module will enable users to automate tasks, generate dynamic prompts, and extend the application's functionality in creative ways.

As development progresses, this module will become an increasingly important part of the BerryAIGen ecosystem, allowing users to tailor the application to their specific needs and workflows.

## Related Documentation

- [BerryAIGen.Toolkit Module](./berryaigen-toolkit.md)
- [Jinja2 Template Documentation](https://jinja.palletsprojects.com/)
