QuantityManagementAppAuthor: Ojas
Date: 20-Feb-2026

 Project Overview:- QuantityManagementApp is a C# application designed to perform unit-based quantity comparisons and conversions.This repository currently includes:
- Project Initialization
- UC1: Feet Measurement Equality
- NUnit Test Coverage
- GitHub Version Control

---------------------------------------------------->✅ UC1: Feet Measurement Equality<-----------------------------------------------------------------------

Objective:- To verify equality between two measurements expressed in feet,ensuring:
- Correct value-based comparison
- Proper handling of floating-point numbers
- Null safety
- Type safety
- Compliance with the equality contract

 Problem StatementIn measurement-based systems,two objects representing the same value must be considered equal even if they are different instances.
Example:

1.0 ft == 1.0 ft → true
1.0 ft == 2.0 ft → false


🏗 Design Approach1️⃣ Class Structure- Feet class represents a measurement in feet.
- Encapsulates value using a private readonly double.
- Ensures immutability.
- Overrides Equals() for value-based comparison.
- Implements proper null and type checks.

🔍 Equality Implementation LogicThe Equals() method ensures:
- Reflexive property → object equals itself
- Null check → object compared with null returns false
- Type check → only compares with Feet
- Value comparison using Double.Compare()
Example logic:

public override bool Equals(object obj)
{
    if (this == obj) return true;
    if (obj == null || obj.GetType() != typeof(Feet)) return false;
    Feet other = (Feet)obj;
    return Double.Compare(this.value,other.value) == 0;
}


🔁 Equality Contract Verified
🧪 NUnit Test CoverageAll tests follow clean naming conventions and focus on one assertion per test.
Test Cases Implemented
✔ `testEquality_SameValue()`Verifies that two objects with the same value are equal.
✔ `testEquality_DifferentValue()`Verifies objects with different values are not equal.
✔ `testEquality_NullComparison()`Verifies object compared with null returns false.
✔ `testEquality_NonNumericInput()`Verifies comparison with different type returns false.
✔ `testEquality_SameReference()`Verifies reflexive property (object equals itself).

📘 Concepts Learned in UC1- Object-Oriented Design
- Encapsulation & Immutability
- Overriding Equals() properly
- Floating-point comparison using Double.Compare()
- Equality contract principles
- Null safety
- Type safety
- Writing clean NUnit test cases
- Given-When-Then test naming style

🖥 Example ExecutionInput:

1.0 ft and 1.0 ft

Output:

Equal (true)



🚀 How to RunBuild Project
dotnet build

Run Application
dotnet run --project torun

Run Tests
dotnet test


------------------------------------------------------>✅UC2: Feet and Inches Measurement Equality<------------------------------------------------------------

Description:- This Use Case extends UC1 to support equality checks for Inches in addition to Feet.
Important: This use case does NOT compare Feet with Inches.Both units are treated independently.
The implementation ensures:
- Accurate value-based equality comparison
- Proper handling of floating-point values
- Null safety
- Type safety
- Complete NUnit test coverage for edge cases

✅ Preconditions- The QuantityMeasurementApp class is instantiated.
- Two numerical values in Feet are provided.
- Two numerical values in Inches are provided.
- Values are hard-coded for comparison.

🔄 Main Flow
1. The Main method calls a static method to validate and compare two Feet values.
2. The Main method calls another static method to validate and compare two Inches values.
3. These static methods:
- Instantiate Feet and Inches objects.
- Call the overridden Equals() method.
4. Both classes:
- Validate input values (numeric check).
- Perform equality comparison using value-based logic.
5. The comparison result (true/false) is returned to the user.

📤 Postconditions
- The equality result (true or false) is returned.
- Feet-to-Feet comparisons are supported.
- Inch-to-Inch comparisons are supported.
- Cross-unit comparison (Feet vs Inches) is NOT performed.

🏗 Implementation Approach
Step 1 Create a separate Inches class similar to the Feet class.
Step 2 Verify equality:
- Two Inches objects with same value → Equal
- Two Inches objects with different values → Not Equal
Step 3 Reduce dependency on the Main method by:
- Creating separate service methods for Feet equality
- Creating separate service methods for Inches equality

🖥 Example Output
Input: 1.0 inch and 1.0 inch
Output: Equal (true)
Input: 1.0 ft and 1.0 ft
Output: Equal (true)

📚 Concepts Learned in UC2
UC2 reinforces concepts from UC1:
- Object Equality
- Floating-point comparison using Double.Compare()
- Null checking
- Type safety
- Encapsulation and immutability
- NUnit testing best practices

🔎 Key Concepts Tested 

Equality Contract
- Reflexive
- Symmetric
- Transitive
- Consistent
- Null-safe
  
Type Safety
Objects are only equal to objects of the same type.

Value-Based Equality
Objects with identical values are considered equal.

Null Safety
Comparison with null safely returns false.

🧪 Test Cases Implemented
Test cases are similar to UC1 and applied for both Feet and Inches:
- testEquality_SameValue()
- testEquality_DifferentValue()
- testEquality_NullComparison()
- testEquality_NonNumericInput()
- testEquality_SameReference()
All test cases ensure:
- Complete branch coverage
- Validation of edge cases
- Proper behavior of overridden Equals() method

⚠️ Design Limitation (Important Learning)
Disadvantage of Using Separate Feet and Inches ClassesThe current implementation violates the DRY (Don't Repeat Yourself) principle.
Both Feet and Inches classes contain:
- Same constructor pattern
- Identical Equals() implementation
- Same private value field
- Same validation logic
❌ Problems- Code duplication
- Harder maintenance
- Risk of inconsistency
- Any logic change must be updated in both classes
