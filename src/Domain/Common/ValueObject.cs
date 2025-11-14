using System;

namespace Domain.Common;

public abstract class ValueObject
{
    protected abstract object[] GetEqualityComponents();

    public override bool Equals(object? obj)  // ✅ เปลี่ยนจาก object → object?
    {
        if (obj == null || obj.GetType() != GetType())
            return false;

        var other = (ValueObject)obj;

        var thisValues = GetEqualityComponents();
        var otherValues = other.GetEqualityComponents();

        if (thisValues.Length != otherValues.Length)
            return false;

        for (int i = 0; i < thisValues.Length; i++)
        {
            if (thisValues[i] == null ^ otherValues[i] == null) return false;
            if (thisValues[i] != null && !thisValues[i].Equals(otherValues[i])) return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var obj in GetEqualityComponents())
            {
                hash = hash * 23 + (obj?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}
