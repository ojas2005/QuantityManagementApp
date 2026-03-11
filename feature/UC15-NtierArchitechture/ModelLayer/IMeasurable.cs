using System;

namespace ModelLayer
{
    public interface IMeasurable
    {
        double ToBaseUnit(double value);
        double FromBaseUnit(double baseValue);
        string Label { get; }
        bool IsValidValue(double value);
        Func<bool> SupportsArithmetic { get; }
        bool HasArithmeticSupport();
        void ValidateOperationSupport(string operation);
    }
}
