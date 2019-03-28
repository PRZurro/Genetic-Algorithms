using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Float Gen class template specialization
/// </summary>
public class FGen : Gen<float>
{
    public FGen(FGen parent1, FGen parent2) : base(parent1, parent2)
    {}

    public FGen(FGen other) : base(other)
    {}

    public FGen(float minMutationVal, float maxMutationVal) : base(minMutationVal, maxMutationVal)
    {}

    public FGen(float value, float minMutationVal, float maxMutationVal) : base(value, minMutationVal, maxMutationVal)
    {}

    public override void Mutate()
    {
        Value = Random.Range(MinMutationValue, MaxMutationValue);
    }
}

/// <summary>
/// Int Gen class template specialization
/// </summary>
public class IGen : Gen<int>
{
    public IGen(int value, int minMutationVal, int maxMutationVal) : base(value, minMutationVal, maxMutationVal)
    {}

    public IGen(int minMutationVal, int maxMutationVal) : base(minMutationVal, maxMutationVal)
    {}

    public IGen(IGen parent1, IGen parent2) : base(parent1, parent2)
    {}

    public IGen(IGen other) : base(other)
    {}

    public override void Mutate()
    {
        Value = Random.Range(MinMutationValue, MaxMutationValue);
    }
}

/// <summary>
/// Bool Gen class template specialization
/// </summary>
public class BGen : Gen<bool>
{
    public BGen(bool value, bool minMutationVal, bool maxMutationVal) : base(value, minMutationVal, maxMutationVal)
    {}

    public BGen(bool minMutationVal, bool maxMutationVal) : base(minMutationVal, maxMutationVal)
    {}

    public BGen(BGen other) : base(other)
    {}

    public BGen(BGen parent1, BGen parent2) : base(parent1, parent2)
    {}

    public override void Mutate()
    {
        Value = Random.Range(0, 2) == 0;
    }
}