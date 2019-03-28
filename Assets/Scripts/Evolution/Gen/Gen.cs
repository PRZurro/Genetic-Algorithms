using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gen<T>
{
    public T Value { get; set; }

    public T MinMutationValue { get; set; }
    public T MaxMutationValue { get; set; }

    public static float ProbMutation { get; set; }

    public abstract void Mutate();

    protected Gen(Gen<T> parent1, Gen<T> parent2)
    {
        if (Random.Range(0.0f, 100.0f) < ProbMutation)
        {
            Mutate();
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                Copy(parent1);
            }
            else
            {
                Copy(parent2);
            }
        }
    }

    protected Gen(T value, T minMutationVal, T maxMutationVal)
    {
        Set(value, minMutationVal, maxMutationVal);
    }

    protected Gen(Gen<T> other)
    {
        Copy(other);
    }

    protected void Copy(Gen<T> other)
    {
        Set(other.Value, other.MinMutationValue, other.MaxMutationValue);
    }

    public void Set(T value, T minMutationVal, T maxMutationVal)
    {
        Value = value;
        MinMutationValue = minMutationVal;
        MaxMutationValue = maxMutationVal;
    }
}