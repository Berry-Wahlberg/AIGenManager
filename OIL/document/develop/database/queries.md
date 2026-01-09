# Database Queries

## Overview

The BerryAIGen.Toolkit application uses a powerful query system to search and filter images based on various criteria. The core of this system is the `QueryBuilder` class, which parses natural language search queries and converts them into efficient SQL queries for the SQLite database.

## QueryBuilder

### Purpose
The `QueryBuilder` class is responsible for:
- Parsing search prompts with natural language syntax
- Converting search terms into SQL WHERE clauses
- Handling complex query logic (AND/OR conditions)
- Supporting various search criteria (model, seed, steps, CFG, etc.)
- Providing a flexible query language for users

### Location
`BerryAIGen.Database/QueryBuilder.cs`

## Query Syntax

The query language supports a wide range of search criteria, allowing users to build complex queries using natural language syntax.

### Basic Syntax

```
[search term] [criterion]:[value]
```

### Supported Criteria

#### Model and Hash

- `model:"model name"` - Search by model name
- `model_hash:abc123` - Search by model hash
- `model_hash:abc123|def456` - Search by multiple model hashes
- `model_or_hash:"model name"|abc123` - Search by model name or hash

#### Generation Parameters

- `seed:12345` - Search by seed
- `seed:1000-2000` - Search by seed range
- `steps:20|30|40` - Search by steps
- `cfg:7.5|8.0` - Search by CFG scale
- `sampler:dpm++ 2m|euler a` - Search by sampler

#### Image Properties

- `size:512x512` - Search by image size
- `size:portrait` - Search by orientation (portrait, landscape, square)
- `size:16:9` - Search by aspect ratio
- `rating:5` - Search by rating (1-10)
- `rating:>=7` - Search by rating range
- `nsfw:true|false` - Search by NSFW status
- `favorite:true|false` - Search by favorite status
- `for deletion:true|false` - Search by deletion status

#### Metadata and Content

- `prompt:"search term"` - Search in prompt
- `negative:"search term"` - Search in negative prompt
- `nometa:true` - Search for images without metadata

#### File Properties

- `path:"folder path"` - Search by file path
- `path:contains:"subfolder"` - Search by path containing a string
- `folder:"folder name"` - Search by folder
- `album:"album name"` - Search by album
- `type:image|video` - Search by file type

#### Date and Time

- `date:today` - Images created today
- `date:yesterday` - Images created yesterday
- `date:3 days ago` - Images created 3 days ago
- `date:between today and 7 days ago` - Images created in a date range
- `date:before 2023-01-01` - Images created before a specific date

## Query Processing Flow

1. **Prompt Parsing**: The search prompt is parsed to extract specific criteria
2. **Regex Matching**: Regular expressions identify and extract criteria values
3. **SQL Generation**: WHERE clauses are generated for each criterion
4. **Condition Combining**: Conditions are combined using AND/OR logic
5. **Query Execution**: The final query is executed against the database

## Common Query Examples

### Basic Search

```
cat
```

Searches for images with "cat" in the prompt.

### Model and Parameters

```
model:"realistic vision" steps:20 cfg:7.5
```

Searches for images made with the "realistic vision" model, 20 steps, and CFG scale 7.5.

### Complex Query

```
cat nsfw:false favorite:true size:1024x1024 rating:>=8 date:since 2023-01-01
```

Searches for favorite, safe-for-work cat images with 1024x1024 resolution, rating 8 or higher, created since January 1, 2023.

### Advanced Query

```
model_hash:abc123|def456 seed:1000-5000 sampler:dpm++ 2m|euler a cfg:7.0-8.0
```

Searches for images with specific model hashes, seed range, samplers, and CFG scale range.

## QueryBuilder Methods

### Core Methods

- `QueryPrompt(string prompt)` - Parses a prompt and returns a WHERE clause
- `ParseParameters(string prompt)` - Parses parameters and returns a structured result
- `Parse(string prompt)` - Parses a full prompt with text and parameters

### Helper Methods

The QueryBuilder contains numerous helper methods for parsing specific criteria:

- `ParseModelName()` - Parses model name criteria
- `ParseHash()` - Parses model hash criteria
- `ParseSeed()` - Parses seed criteria
- `ParseSteps()` - Parses steps criteria
- `ParseCFG()` - Parses CFG scale criteria
- `ParseSampler()` - Parses sampler criteria
- `ParseSize()` - Parses size criteria
- `ParseDate()` - Parses date criteria
- `ParseRating()` - Parses rating criteria
- `ParseFavorite()` - Parses favorite criteria
- `ParseNSFW()` - Parses NSFW criteria

## Performance Considerations

### Index Usage

The database is optimized with indexes on frequently queried columns:
- `ModelHash` - For model-based searches
- `Seed` - For seed-based searches
- `CreatedDate` - For date-based searches
- `Rating` - For rating-based searches

### Query Optimization

- Queries are built with parameterized values to prevent SQL injection
- AND/OR logic is carefully handled to optimize query execution
- LIKE queries use wildcards appropriately to leverage indexes
- Complex queries are broken down into manageable components

## Best Practices for Writing Queries

### 1. Be Specific

Use specific criteria to narrow down results and improve performance:

```
// Good
model:"realistic vision" steps:20 cfg:7.5

// Better
model:"realistic vision 1.3" steps:20 cfg:7.5 sampler:dpm++ 2m
```

### 2. Use Multiple Criteria

Combine multiple criteria for more precise results:

```
// Good
cat

// Better
cat nsfw:false size:1024x1024 rating:>=8
```

### 3. Use OR for Multiple Values

Use the pipe (`|`) separator for multiple values of the same criterion:

```
// Good
model_hash:abc123

// Better for multiple models
model_hash:abc123|def456|ghi789
```

### 4. Use Ranges When Appropriate

Use ranges for numerical criteria:

```
// Good
seed:12345

// Better for a range
seed:10000-20000
```

### 5. Avoid Overly Broad Queries

Overly broad queries can return too many results and impact performance:

```
// Bad (returns all images)

// Better (narrows down results)
size:1024x1024 date:since 2023-01-01
```

## Query Examples

### Finding Images by Model

```
model:"deliberate" steps:20-30 cfg:7.0-8.0
```

### Finding Images by Date Range

```
date:between 2023-01-01 and 2023-12-31
```

### Finding High-Rated Images

```
rating:>=9 favorite:true
```

### Finding Images by Size

```
size:1024x1024|768x1024
```

### Finding Images by Prompt and Parameters

```
"cat" seed:12345 steps:20 cfg:7.5
```

## Integration with Search Service

The QueryBuilder is used by the SearchService to:
- Parse user search queries
- Generate SQL WHERE clauses
- Execute searches against the database
- Return filtered results to the UI

## Conclusion

The query system in BerryAIGen.Toolkit provides a powerful and flexible way for users to search and filter their AI-generated images. By leveraging the QueryBuilder class, users can build complex queries using natural language syntax, making it easy to find exactly what they're looking for. The system is designed for performance, with optimized SQL generation and efficient database indexing ensuring fast search results even with large image collections.
