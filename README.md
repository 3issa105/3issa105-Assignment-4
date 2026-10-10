# Academy Schedule Analyzer

Student Name: Ahmed Eissa
Cohort: .NET Diploma
Assignment: C# Intermediate — Module 2-1 (Assignment 4)

## Project
[AcademyScheduleAnalyzer/](AcademyScheduleAnalyzer/)

## LeetCode
[LeetCode/README.md](LeetCode/README.md)

## LinkedIn
[LinkedIn/README.md](LinkedIn/README.md)

## Benchmark
[BENCHMARK.md](BENCHMARK.md)

---

## Instructions for Running the Console Application

### Prerequisites
- [.NET SDK 8.0, 9.0, or 10.0+](https://dotnet.microsoft.com/download)

### Run the Interactive Schedule Analyzer
Open a terminal in the repository root and run:
```bash
dotnet run --project AcademyScheduleAnalyzer
```
This launches the console menu containing options `1` through `16` and `0` to exit.

### Run BenchmarkDotNet Performance Tests
To execute the benchmark tests comparing `string` concatenation with `StringBuilder`:
```bash
dotnet run --project AcademyScheduleAnalyzer -c Release -- --benchmark
```
*Note: BenchmarkDotNet requires a Release build to measure optimal performance without debugger interference.*
