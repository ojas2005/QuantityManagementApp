using System;

public interface IMeasurable
{
    double ToBaseUnit(double value);
    double FromBaseUnit(double baseValue);
    string Label { get; }
    bool IsValidValue(double value);
}