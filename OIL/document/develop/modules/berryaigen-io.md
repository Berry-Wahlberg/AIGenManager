# BerryAIGen.IO Module

## Overview
The BerryAIGen.IO module provides file I/O operations for the BerryAIGen.Toolkit application, focusing on metadata extraction, file scanning, and image processing. It handles reading and writing files, extracting metadata from various AI image formats, and processing image files for thumbnail generation and other purposes.

## Core Components

### 1. ComfyUI

**Purpose**: Provides functionality for working with ComfyUI workflows and nodes.

**Key Features**:
- Parsing and processing ComfyUI workflow files
- Extracting node information from workflows
- Converting workflow data to database models

### 2. FileParameters

**Purpose**: Defines parameters for file operations.

**Key Properties**:
- File path information
- Processing options
- Metadata extraction settings

### 3. HashFunctions

**Purpose**: Provides hash generation functionality for files.

**Key Methods**:
- `ComputeSHA256`: Computes SHA256 hash for a file
- `ComputeMD5`: Computes MD5 hash for a file
- `ComputeHash`: Computes hash using specified algorithm

**Key Features**:
- Efficient hash computation
- Support for multiple hash algorithms
- Progress reporting for large files

### 4. Metadata

**Purpose**: Represents metadata extracted from image files.

**Key Properties**:
- `Prompt`: Generation prompt
- `NegativePrompt`: Negative prompt
- `Model`: Model name
- `Seed`: Generation seed
- `Sampler`: Sampling method
- `CFGScale`: CFG scale value
- `Steps`: Number of steps
- `Height`: Image height
- `Width`: Image width
- `AestheticScore`: Aesthetic score

**Key Features**:
- Comprehensive metadata representation
- Support for multiple metadata formats
- Easy conversion to database models

### 5. MetadataScanner

**Purpose**: Main component for extracting metadata from image files.

**Key Methods**:
- `ExtractMetadata`: Extracts metadata from a file
- `GetFormat`: Determines the metadata format of a file

**Key Features**:
- Supports multiple metadata formats:
  - AUTOMATIC1111 and compatible
  - InvokeAI
  - NovelAI
  - Stable Diffusion
  - EasyDiffusion
  - Fooocus family (Fooocus, RuinedFooocus, FooocusMRE)
  - Stable Swarm
- Extracts metadata from various file types:
  - JPG/JPEG with EXIF
  - PNG
  - WebP
  - MP4 (video)
  - .txt metadata files
- Efficient metadata extraction
- Error handling for corrupt files

### 6. ModelScanner

**Purpose**: Scans directories for AI model files.

**Key Methods**:
- `Scan`: Scans a directory for model files
- `GetModelInfo`: Extracts information from a model file

**Key Features**:
- Scans for various model types
- Extracts model metadata
- Generates model hashes
- Supports recursive scanning

### 7. StealthPng

**Purpose**: Handles Stealth PNG functionality for images.

**Key Methods**:
- `ExtractStealthData`: Extracts stealth data from PNG files
- `EmbedStealthData`: Embeds stealth data into PNG files

**Key Features**:
- Support for Stealth PNG format
- Extracting hidden metadata
- Embedding metadata into images

## Metadata Extraction Process

The metadata extraction process follows these steps:

1. **File Identification**: The scanner identifies the file type and metadata format
2. **File Reading**: The file is opened and read into memory
3. **Metadata Parsing**: The appropriate parser is used to extract metadata based on the format
4. **Data Normalization**: Extracted metadata is normalized into a standard format
5. **Metadata Validation**: The extracted metadata is validated for correctness
6. **Model Matching**: The metadata is matched with available models
7. **Database Integration**: The metadata is converted to database models for storage

## Supported Metadata Formats

The MetadataScanner supports extraction from various AI image generation platforms:

### 1. AUTOMATIC1111 and Compatible
- Tensor.Art
- SDNext
- Other AUTOMATIC1111 forks

### 2. InvokeAI
- Dream/sd-metadata/invokeai_metadata formats

### 3. NovelAI
- NovelAI-specific metadata format

### 4. Stable Diffusion
- Base Stable Diffusion metadata format

### 5. EasyDiffusion
- EasyDiffusion metadata format

### 6. Fooocus Family
- RuinedFooocus
- Fooocus
- FooocusMRE

### 7. Stable Swarm
- Stable Swarm metadata format

## Usage Examples

### Extracting Metadata from an Image

```csharp
// Create a MetadataScanner instance
var scanner = new MetadataScanner();

// Extract metadata from a file
var metadata = scanner.ExtractMetadata("image.png");

// Access extracted metadata
Console.WriteLine($"Prompt: {metadata.Prompt}");
Console.WriteLine($"Model: {metadata.Model}");
Console.WriteLine($"Seed: {metadata.Seed}");
```

### Computing File Hash

```csharp
// Compute SHA256 hash for a file
var hash = HashFunctions.ComputeSHA256("image.png");
Console.WriteLine($"SHA256: {hash}");
```

### Scanning for Models

```csharp
// Scan a directory for models
var models = ModelScanner.Scan("models/");

// Process found models
foreach (var model in models)
{
    Console.WriteLine($"Found model: {model.Filename}");
    Console.WriteLine($"SHA256: {model.SHA256}");
}
```

## Performance Considerations

### 1. Efficient File Reading
- Files are read in chunks for large files
- Memory usage is optimized
- Proper disposal of file streams

### 2. Parallel Processing
- Support for parallel metadata extraction
- Efficient use of multiple CPU cores
- Progress reporting for long operations

### 3. Caching
- Caching of extracted metadata for repeated access
- Hash caching to avoid recomputation

### 4. Error Handling
- Robust error handling for corrupt files
- Graceful degradation when metadata cannot be extracted
- Detailed error messages for debugging

## Integration with Other Modules

The BerryAIGen.IO module integrates with several other modules:

### 1. BerryAIGen.Database
- Converts extracted metadata to database models
- Provides hash generation for database records

### 2. BerryAIGen.Toolkit
- Used by the ScanningService for folder scanning
- Used by the MetadataScannerService for metadata extraction
- Provides file I/O functionality for various operations

### 3. BerryAIGen.Common
- Uses common utilities from the Common module
- Follows shared interfaces and patterns

## Conclusion

The BerryAIGen.IO module provides essential file I/O and metadata extraction functionality for the BerryAIGen.Toolkit application. It supports a wide range of metadata formats from various AI image generation platforms, allowing the application to extract comprehensive metadata from different types of image files. Its efficient design and robust error handling make it a reliable component for handling the diverse file types and metadata formats encountered in AI-generated image collections.

