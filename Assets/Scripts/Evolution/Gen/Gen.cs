using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class template that corresponds with a generic gen that must be implemented
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Gen<T>
{
    public T Value { get; set; }

    public T MinMutationValue { get; set; }
    public T MaxMutationValue { get; set; }

    public static float Mutability { get; set; } // In percentage

    /// <summary>
    /// Recombination constructor, create a new Gen by inheriting the values of one gen parent (50% probability) or by mutating
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    protected Gen(Gen<T> parent1, Gen<T> parent2)
    {
        SetMutationValuesRange(parent1.MinMutationValue, parent1.MaxMutationValue); // Parents mutation limits must be equal

        if (Random.Range(0.0f, 100.0f) < Mutability)
        {
            Mutate();
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f) 
            {
                Value = parent1.Value; // Soft copy because limits have been initialized before
            }
            else
            {
                Value = parent2.Value; // Soft copy because limits have been initialized before
            }
        }
    }

    /// <summary>
    /// Create a new Gen initializing all members
    /// <param name="value"></param>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    protected Gen(T value, T minMutationVal, T maxMutationVal)
    {
        Set(value, minMutationVal, maxMutationVal);
    }

    /// <summary>
    /// Create a new Gen, set mutation range of values and mutate it's value member.
    /// </summary>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    protected Gen(T minMutationVal, T maxMutationVal)
    {
        SetMutationValuesRange(minMutationVal, maxMutationVal);
        Mutate();
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    protected Gen(Gen<T> other)
    {
        CopyFrom(other);
    }

    /// <summary>
    /// Method that must be implemented by children because it is unknown the template type in order to obtain a random value
    /// </summary>
    public abstract void Mutate();

    /// <summary>
    /// Copy the values of another Gen to this
    /// </summary>
    /// <param name="other"></param>
    protected void CopyFrom(Gen<T> other)
    {
        Set(other.Value, other.MinMutationValue, other.MaxMutationValue);
    }

    /// <summary>
    /// Set all members of this class  (except static Mutability member) that must be initialized to all type instance of this class template)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    public void Set(T value, T minMutationVal, T maxMutationVal)
    {
        Value = value;
        SetMutationValuesRange(minMutationVal, maxMutationVal);
    }

    /// <summary>
    /// Set the mutation value limits
    /// </summary>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    public void SetMutationValuesRange(T minMutationVal, T maxMutationVal)
    {
        MinMutationValue = minMutationVal;
        MaxMutationValue = maxMutationVal;
    }
}