using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Float Gen class template specialization
/// </summary>
[System.Serializable]
public class FGen : Gen<MotoFGenID, float>
{
    public FGen(FGen parent1, FGen parent2) : base(parent1, parent2)
    {}

    public FGen(FGen other) : base(other)
    {}

    public FGen(MotoFGenID id, float minMutationVal, float maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public FGen(MotoFGenID id, float value, float minMutationVal, float maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
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
public class IGen : Gen<MotoIGenID, int>
{
    public IGen(MotoIGenID id, int value, int minMutationVal, int maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
    {}

    public IGen(MotoIGenID id, int minMutationVal, int maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public IGen(IGen parent1, IGen parent2) : base(parent1, parent2)
    {}

    public IGen(IGen other) : base(other)
    {}

    public override void Mutate()
    {
        m_value = Random.Range(m_minMutationValue, m_maxMutationValue);
    }
}

/// <summary>
/// Bool Gen class template specialization
/// </summary>
[System.Serializable]
public class BGen : Gen<MotoBGenID, bool>
{
    public BGen(MotoBGenID id, bool value, bool minMutationVal, bool maxMutationVal) : base(id, value, minMutationVal, maxMutationVal)
    {}

    public BGen(MotoBGenID id, bool minMutationVal, bool maxMutationVal) : base(id, minMutationVal, maxMutationVal)
    {}

    public BGen(BGen other) : base(other)
    {}

    public BGen(BGen parent1, BGen parent2) : base(parent1, parent2)
    {}

    public override void Mutate()
    {
        m_value = Random.Range(0, 2) == 0;
    }
}
