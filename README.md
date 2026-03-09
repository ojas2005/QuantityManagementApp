-----------------------------------QuantityManagementApp--------------------------------

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

Here is your **updated section to append to your README after UC9**, keeping the **same style, formatting, and architectural explanation** you already used. I integrated **QM10–QM13 concepts cleanly into UC10–UC13** so it looks consistent and professional for GitHub.

---

# 📚 Continued Feature Evolution

---

# ✅ UC10 – Generic Quantity Class with Unit Interface (Multi-Category Support)

Introduced a **generic quantity system** capable of supporting multiple measurement categories (Length, Weight, Volume) through **interface-driven design**.

### Key Additions

```csharp
public class Quantity<TUnit> where TUnit : IMeasurable
```

Introduced:

* `IMeasurable` interface for unit behavior
* Generic `Quantity<TUnit>` class
* Cross-category type safety
* Delegated conversion logic

### Architecture Change

Instead of separate implementations for each measurement category:

```
QuantityLength
QuantityWeight
```

The system now supports:

```
Quantity<TUnit>
```

This enables **scalable multi-category measurement systems**.

### Example

```csharp
Quantity<LengthUnit> length
Quantity<WeightUnit> weight
Quantity<VolumeUnit> volume
```

### Design Benefits

✔ Interface-based architecture
✔ Open-Closed Principle (OCP) compliance
✔ Liskov Substitution Principle (LSP) compatibility
✔ Composition over inheritance
✔ Enum as behavior carrier
✔ Runtime type safety with generics
✔ Polymorphic unit behavior

### Additional Improvements

* Consolidated demonstration logic
* Functional immutability maintained
* Generics type erasure considerations handled
* Wildcard-safe generic constraints

📌 Learning Outcomes:

* Generic programming in C#
* Interface-based polymorphism
* Cross-category measurement architecture
* Scalable unit system design

---

# ✅ UC11 – Volume Measurement Equality, Conversion, and Addition

Extended the **generic quantity system** to support **Volume measurements**.

### Supported Units

* Litre (Base Unit)
* Millilitre
* Gallon

### Implementation

```csharp
public enum VolumeUnit : IMeasurable
{
    Litre,
    Millilitre,
    Gallon
}
```

### Features

✔ Cross-unit equality
✔ Conversion between all volume units
✔ Addition across different units
✔ Base-unit normalization strategy

### Example

```csharp
var water = new Quantity<VolumeUnit>(1, VolumeUnit.Litre);
var milk = new Quantity<VolumeUnit>(500, VolumeUnit.Millilitre);

var result = water.Add(milk);
```

### Design Enhancements

* Reusable generic logic across categories
* Conversion precision validation
* Floating-point consistency across measurements
* Method overloading consistency
* DRY principle validation across categories

📌 Learning Outcomes:

* Generic type constraints
* Base-unit selection strategy
* Enum-driven polymorphism
* Architectural scalability validation

---

# ✅ UC12 – Subtraction and Division Operations

Extended quantity arithmetic operations beyond addition.

### New Operations

```csharp
Quantity.Subtract()
Quantity.Divide()
```

### Behavior

#### Subtraction

```csharp
result = first - second
```

Both quantities are converted to the **base unit before subtraction**.

#### Division

```csharp
ratio = quantity1 / quantity2
```

Returns a **scalar value** representing the ratio.

### Validation Rules

✔ Cross-category safety enforced
✔ Division by zero protection
✔ Target unit specification support
✔ Floating-point precision maintained

### Example

```csharp
var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
var q2 = new Quantity<LengthUnit>(5, LengthUnit.Feet);

var result = q1.Subtract(q2);
```

### Design Improvements

* Immutable arithmetic operations
* Non-commutative operation validation
* Helper methods for code reuse
* Consistent validation across operations

📌 Learning Outcomes:

* Arithmetic design for immutable objects
* Defensive programming
* Precision handling in measurement systems

---

# ✅ UC13 – Centralized Arithmetic Logic (DRY Enforcement)

Refactored arithmetic operations to remove duplication and enforce **DRY principle** across the system.

### Strategy

Centralized arithmetic logic into a **single operation dispatcher**.

### Implementation Concept

```csharp
private Quantity<TUnit> PerformOperation(
    Quantity<TUnit> other,
    Func<double, double, double> operation)
```

Operations now use **lambda expressions**:

```csharp
Add → (a, b) => a + b
Subtract → (a, b) => a - b
Multiply → (a, b) => a * b
Divide → (a, b) => a / b
```

### Enum-Driven Operation Dispatch

Arithmetic behavior is coordinated through enums.

### Advantages

✔ Single source of truth
✔ Simplified arithmetic extension
✔ Reduced code duplication
✔ Improved readability
✔ Centralized error handling

### Design Enhancements

* Functional interfaces
* Parametric polymorphism
* Operation strategy pattern
* Refactoring without behavioral change

---



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

```
Feet Equality
→ Multi-Unit Length
→ Generic Length System
→ Conversion
→ Addition
→ Target Unit Control
→ Unit Architecture Refinement
→ Weight Measurement
→ Generic Quantity System
→ Volume Measurements
→ Advanced Arithmetic Operations
→ DRY-Optimized Arithmetic Architecture
```
This repository demonstrates structured, incremental system design — similar to real production evolution.


