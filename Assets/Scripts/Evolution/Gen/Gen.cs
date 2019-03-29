using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class template that corresponds with a generic gen that must be implemented
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public abstract class Gen<EnumOfIDs, T>
{
    [SerializeField]
    protected EnumOfIDs m_ID;

    [System.NonSerialized]
    protected T m_value; //Value in serialization will be ignored

    [SerializeField]
    protected T m_minMutationValue;
    [SerializeField]
    protected T m_maxMutationValue;

    public static float Mutability { get; set; } // In percentage

    /// <summary>
    /// Default constructor
    /// </summary>
    protected Gen()
    {}

    /// <summary>
    /// Recombination constructor, create a new Gen by inheriting the values of one gen parent (50% probability) or by mutating
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    protected Gen(Gen<EnumOfIDs, T> parent1, Gen<EnumOfIDs, T> parent2)
    {
        // Parents mutation limits and ID must be equal
        Set(parent1.m_ID, parent1.m_minMutationValue, parent1.m_maxMutationValue);

        // Inheritate or mutate
        if (Random.Range(0.0f, 100.0f) < Mutability)
        {
            Mutate();
        }
        else
        {
            if (Random.Range(0.0f, 1.0f) < 0.5f)
            {
                m_value = parent1.m_value; // Soft copy because limits have been initialized before
            }
            else
            {
                m_value = parent2.m_value; // Soft copy because limits have been initialized before
            }
        }
    }

    /// <summary>
    /// Create a new Gen initializing all members
    /// <param name="value"></param>
    /// <param name="minMutationValue"></param>
    /// <param name="maxMutationValue"></param>
    protected Gen(EnumOfIDs id, T value, T minMutationValue, T maxMutationValue)
    {
        Set(id, value, minMutationValue, maxMutationValue);
    }

    /// <summary>
    /// Create a new Gen, set mutation range of values and mutate it's value member.
    /// </summary>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    protected Gen(EnumOfIDs id, T minMutationVal, T maxMutationVal)
    {
        Set(id, minMutationVal, maxMutationVal);
        Mutate();
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    protected Gen(Gen<EnumOfIDs, T> other)
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
    protected void CopyFrom(Gen<EnumOfIDs, T> other)
    {
        Set(other.m_ID, other.m_value, other.m_minMutationValue, other.m_maxMutationValue);
    }

    /////////////////////////////////////////////////SETTERS///////////////////////////////////////////////////////////////////////////////
    /// <summary>
    /// Set all members of this class  (except static Mutability member) that must be initialized to all type instance of this class template)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="minMutationVal"></param>
    /// <param name="maxMutationVal"></param>
    public void Set(EnumOfIDs id, T value, T minMutationVal, T maxMutationVal)
    {
        m_value = value;
        Set(id, minMutationVal, maxMutationVal);
    }

    /// <summary>
    /// Set the mutation value limits
    /// </summary>
    /// <param name="minMutationValue"></param>
    /// <param name="maxMutationValue"></param>
    public void Set(EnumOfIDs id, T minMutationValue, T maxMutationValue)
    {
        m_ID = id;
        SetMutationValueRange(minMutationValue, maxMutationValue);
    }

    public void SetID(EnumOfIDs ID)
    {
        m_ID = ID;
    }

    public void SetValue(EnumOfIDs ID)
    {
        m_ID = ID;
    }

    public void SetMinMutationValue(T minMutationValue)
    {
        m_minMutationValue = minMutationValue;
    }

    public void SetMaxMutationValue(T maxMutationValue)
    {
        m_maxMutationValue = maxMutationValue;
    }

    public void SetMutationValueRange(T minMutationValue , T maxMutationValue)
    {
        m_minMutationValue = minMutationValue;
        m_maxMutationValue = maxMutationValue;
    }

    /////////////////////////////////////////////////GETTERS///////////////////////////////////////////////////////////////////////////////

    public EnumOfIDs ID()
    {
        return m_ID;
    }

    public T Value()
    {
        return m_value;
    }

    public T MinMutationValue()
    {
        return m_minMutationValue;
    }

    public T MaxMutationValue()
    {
        return m_maxMutationValue;
    }
}
