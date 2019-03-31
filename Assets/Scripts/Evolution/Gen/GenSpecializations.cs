using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Float Gen class template specialization
/// </summary>
[System.Serializable]
public class FGen : Gen<FGenID, float>
{
    public FGen(FGen parent1, FGen parent2) : base(parent1, parent2)
    {}

    public FGen(FGen other, bool mutate = false) : base(other, mutate)
    {}

    public FGen(FGenID id, float minMutationVal, float maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public FGen(FGenID id, float value, float minMutationVal, float maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
    {}

    public override void Mutate()
    {
        m_value = Random.Range(m_minMutationValue, m_maxMutationValue);
    }
}

/// <summary>
/// Int Gen class template specialization
/// </summary>
[System.Serializable]
public class IGen : Gen<IGenID, int>
{
    public IGen(IGenID id, int value, int minMutationVal, int maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
    {}

    public IGen(IGenID id, int minMutationVal, int maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public IGen(IGen parent1, IGen parent2) : base(parent1, parent2)
    {}

    public IGen(IGen other, bool mutate = false) : base(other, mutate)
    {}

    public override void Mutate()
    {
        m_value = Random.Range(m_minMutationValue, m_maxMutationValue + 1);
    }
}

/// <summary>
/// Bool Gen class template specialization
/// </summary>
[System.Serializable]
public class BGen : Gen<BGenID, bool>
{
    public BGen(BGenID id, bool value, bool minMutationVal, bool maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
    {}

    public BGen(BGenID id, bool minMutationVal, bool maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public BGen(BGen other, bool mutate = false) : base(other, mutate)
    {}

    public BGen(BGen parent1, BGen parent2) : base(parent1, parent2)
    {}

    public override void Mutate()
    {
        m_value = Random.Range(0, 2) == 0;
    }
}
