# DotnetToolbox

A growing collection of reusable .NET utility classes and extension methods. Built for real-world use.

## Installation
Clone and reference the project, or copy what you need.

## What's Inside
- **StringExtensions** — Truncate, slug generation, email validation
- **DateTimeHelpers** — Relative time, business days, date ranges
- **FileUtils** — Safe file ops, temp files, size formatting

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
