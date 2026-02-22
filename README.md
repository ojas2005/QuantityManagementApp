--------------------------------------QuantityManagementApp--------------------------------
---

## 👨‍💻 Author

**Ojas**
Start date:-  20-Feb-2026

---

# 🚀 Project Overview

**QuantityManagementApp** is a feature-driven C# application built using **.NET 8**, focused on:

* Measurement equality validation
* Cross-unit conversion
* Unit addition
* Floating-point precision handling
* Weight & Length abstraction
* 200+ NUnit test cases
* Git feature-branch development

The project evolves incrementally through structured Use Cases (UC1–UC9), following clean architecture and OOP principles.

---

# 🌿 Development Strategy (Feature Branching)

Each use case is developed independently:

```
dev
 ├── feature/UC1-FeetEquality
 ├── feature/UC2-InchEquality
 ├── feature/UC3-GenericLength
 ├── feature/UC4-YardEquality
 ├── feature/UC5-UnitConversion
 ├── feature/UC6-UnitAddition
 ├── feature/UC7-TargetUnitAddition
 ├── feature/UC8-StandaloneUnit
 └── feature/UC9-WeightMeasurementEquality
```

✔ Incremental delivery
✔ Independent validation
✔ Clean Git history
✔ Production-like workflow

---

# 📚 Feature Evolution

---

## ✅ UC1 – Feet Equality

* Implemented immutable `Feet` class
* Overrode `Equals()`
* Verified equality contract
* Introduced NUnit testing

📌 Learning:

* Value-based equality
* Null safety
* Type safety
* Floating-point comparison

---

## ✅ UC2 – Feet & Inches Equality

* Added `Inches` class
* Maintained independent equality
* Identified DRY violation

📌 Key Insight:
Code duplication leads to maintenance risk → Motivated UC3.

---

## ✅ UC3 – Generic Length System

Replaced multiple classes with:

```csharp
public class QuantityLength : IEquatable<QuantityLength>
```

Introduced:

* `LengthUnit` enum
* Base-unit conversion strategy (Feet)
* Tolerance-based comparison
* Operator overloading (==, !=)

---

## ✅ UC4 – Yard Support

Extended generic system to support:

* Yards ↔ Feet
* Yards ↔ Inches
* Yards ↔ Centimeters

---

## ✅ UC5 – Unit Conversion

Added:

```csharp
public double ConvertTo(LengthUnit targetUnit)
```

✔ Cross-unit conversion
✔ Service abstraction layer
✔ Input validation

---

## ✅ UC6 – Unit Addition

Added support for:

```csharp
QuantityLength.Add(first, second)
```

Process:

1. Convert both to base unit
2. Add
3. Convert result back to first unit

---

## ✅ UC7 – Target Unit Addition

Explicit result unit support:

```csharp
QuantityLength.Add(first, second, targetUnit)
```

✔ Improved API usability
✔ Flexible result formatting

---

## ✅ UC8 – Standalone Unit Architecture

Moved conversion logic to enum extension methods:

* ConvertToBaseUnit()
* ConvertFromBaseUnit()
* GetConversionFactor()
* GetLabel()

✔ Separation of concerns
✔ Cleaner architecture

---

## ✅ UC9 – Weight Measurement System

Added:

```csharp
public class QuantityWeight : IEquatable<QuantityWeight>
```

### Supported Units:

* Kilogram (Base)
* Gram
* Pound

### Features:

✔ Cross-unit equality
✔ Conversion
✔ Addition
✔ Nullable unit validation
✔ Negative value support
✔ Hash normalization

---

# 🧠 Architecture Overview

## Length Architecture

```
              +----------------------+
              |  QuantityLength      |
              |----------------------|
              | double _value        |
              | LengthUnit _unit     |
              |----------------------|
              | ConvertTo()          |
              | Equals()             |
              | Add()                |
              +----------+-----------+
                         |
                         v
                +----------------+
                |  LengthUnit    |
                |----------------|
                | Feet (Base)    |
                | Inch           |
                | Yard           |
                | Centimeters    |
                +----------------+
```

---

## Weight Architecture

```
              +----------------------+
              |  QuantityWeight      |
              |----------------------|
              | double _value        |
              | WeightUnit _unit     |
              |----------------------|
              | ConvertTo()          |
              | Equals()             |
              | Add()                |
              +----------+-----------+
                         |
                         v
                +----------------+
                |  WeightUnit    |
                |----------------|
                | Kilogram(Base) |
                | Gram           |
                | Pound          |
                +----------------+
```

---

# 🧪 Testing Strategy

* 200+ NUnit test cases
* One assertion per test
* Edge case coverage
* Null validation
* Floating-point tolerance checks
* Equality contract verification

✔ Reflexive
✔ Symmetric
✔ Transitive
✔ Consistent
✔ Null-safe

---

# 🏗 Design Principles Applied

* Object-Oriented Programming
* Encapsulation
* Immutability
* DRY Principle
* SOLID Principles
* Equality Contract
* IEquatable<T>
* Operator Overloading
* Clean Git Workflow
* Separation of Concerns

---

# ▶️ How to Run

## Build

```bash
dotnet build
```

## Run Application

```bash
dotnet run --project torun
```

## Run Tests

```bash
dotnet test
```

---

# 📈 Project Progression Summary

Feet Equality
→ Multi-Unit Length
→ Generic Length
→ Conversion
→ Addition
→ Explicit Target Control
→ Unit Architecture Refinement
→ Weight Measurement System

This repository demonstrates structured, incremental system design — similar to real production evolution.


