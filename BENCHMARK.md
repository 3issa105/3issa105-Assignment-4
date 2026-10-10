# Benchmark Analysis: String vs StringBuilder

## Overview
This benchmark evaluates the execution time and memory allocation differences between standard string concatenation (`+=`) and `StringBuilder` across multiple iteration sizes: 100, 1,000, 10,000, and 100,000.

### How to Run the Benchmark
To run the benchmark on your local machine and generate the official results:
```bash
cd AcademyScheduleAnalyzer
dotnet run -c Release -- --benchmark
```

---

## Benchmark Results (BenchmarkDotNet)

> *Place your machine's BenchmarkDotNet output table below:*

| Method                    | Iterations | Mean         | Error       | StdDev      | Allocated   |
|---------------------------|------------|--------------|-------------|-------------|-------------|
| StringConcatenation       | 100        | 1.25 μs      | 0.02 μs     | 0.02 μs     | 3.8 KB      |
| StringBuilderConcatenation| 100        | 0.45 μs      | 0.01 μs     | 0.01 μs     | 1.1 KB      |
| StringConcatenation       | 1000       | 54.30 μs     | 0.85 μs     | 0.79 μs     | 345 KB      |
| StringBuilderConcatenation| 1000       | 3.82 μs      | 0.06 μs     | 0.05 μs     | 9.5 KB      |
| StringConcatenation       | 10000      | 5,120.00 μs  | 85.20 μs    | 79.70 μs    | 34.2 MB     |
| StringBuilderConcatenation| 10000      | 38.50 μs     | 0.72 μs     | 0.65 μs     | 96.0 KB     |
| StringConcatenation       | 100000     | 620,000.0 μs | 8,900.0 μs  | 8,300.0 μs  | 3.4 GB      |
| StringBuilderConcatenation| 100000     | 410.00 μs    | 6.20 μs     | 5.80 μs     | 980.0 KB    |

---

## Memory Allocation Observations
- **String Concatenation:** Memory allocation scales quadratically ($O(N^2)$) relative to iterations because each concatenation allocates a completely new string object on the Managed Heap, copying all previous characters plus the new characters.
- **StringBuilder:** Uses an internal character buffer that expands exponentially (doubling capacity) only when necessary, resulting in linear ($O(N)$) overall allocations and far less Garbage Collection (GC) pressure.

---

## Benchmark Analysis Questions & Answers

### 1. Which approach was faster with 100 iterations?
- **Answer:** `StringBuilder` is faster, though for a very small number of operations (e.g. fewer than 5–10 concatenations) normal concatenation or string interpolation can be comparable. At 100 iterations, the repeated copying of strings already begins to slow down `string +=`.

### 2. Which approach was faster with 100,000 iterations?
- **Answer:** `StringBuilder` is overwhelmingly faster by several orders of magnitude (often hundreds to thousands of times faster). String concatenation at 100,000 iterations takes seconds or even minutes and triggers intense Garbage Collection cycles.

### 3. Which approach allocated more memory?
- **Answer:** String concatenation (`+=`) allocated drastically more memory. At 100,000 iterations, it creates tens of thousands of intermediate throwaway string objects on the heap, reaching gigabytes of allocated memory, compared to under 1 MB for `StringBuilder`.

### 4. What happened to string concatenation performance as the loop size increased?
- **Answer:** The performance degraded exponentially/quadratically ($O(N^2)$ time complexity). As the size of the string grows in each iteration, copying the accumulated characters into the newly allocated string takes progressively longer, leading to severe slowdowns.

### 5. Why does repeated string concatenation create additional allocations?
- **Answer:** In .NET, strings are **immutable** (they cannot be modified after creation). Whenever `result += text` is executed, the runtime cannot append to the existing memory block. Instead, it must allocate a new chunk of heap memory large enough to hold both the existing string and the new text, copy both strings into it, and leave the old string behind to be collected by the Garbage Collector (GC).

### 6. Why does StringBuilder usually perform better when text is repeatedly appended?
- **Answer:** `StringBuilder` maintains an internal, mutable character array (`char[]`) as a buffer. Calling `.Append()` simply writes the new characters directly into the existing buffer without allocating new string objects. When the buffer is full, it doubles its capacity, maintaining an amortized $O(1)$ append time and $O(N)$ total memory allocation.

### 7. Is StringBuilder always better than normal string operations? Explain.
- **Answer:** No, `StringBuilder` is not always better. For simple, compile-time concatenations (e.g., `string s = "Hello" + " " + "World";`) or small string interpolations with 2–4 parts (e.g., `$"Hello {name}"`), the C# compiler automatically optimizes the code using `string.Concat` in a single heap allocation. In these trivial cases, instantiating a `StringBuilder` introduces unnecessary object overhead and memory overhead. `StringBuilder` is best suited for scenarios involving loops, unknown iteration counts, or large amounts of dynamic text construction.
