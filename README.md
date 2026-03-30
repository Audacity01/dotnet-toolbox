# DotnetToolbox

A growing collection of reusable .NET utility classes and extension methods. Built for real-world use.

## Installation
Clone and reference the project, or copy what you need.

## What's Inside
- **StringExtensions** — Truncate, slug generation, email validation
- **DateTimeHelpers** — Relative time, business days, date ranges
- **FileUtils** — Safe file ops, temp files, size formatting
- **CollectionExtensions** — Chunk, shuffle, safe dictionary conversion
- **Guard** — Argument validation helpers (null checks, range checks)
- **Retry** — Retry logic with exponential backoff for sync and async
- **HttpHelper** — Typed GET/POST, reachability check, query string builder
- **EnumHelper** — Parse, get description, list values, to dictionary
- **NumberExtensions** — Clamp, ordinals, percentage formatting, range checks

## Usage

```csharp
using DotnetToolbox.Extensions;

var slug = "Hello World! This is a test.".ToSlug();
// hello-world-this-is-a-test

var truncated = "Some really long text here".Truncate(10);
// "Some reall..."

var isValid = "test@example.com".IsValidEmail();
// true
```
