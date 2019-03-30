using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotoGenome 
{
    Dictionary<MotoFGenID, FGen> m_fGenes;
    Dictionary<MotoIGenID, IGen> m_iGenes;
    Dictionary<MotoBGenID, BGen> m_bGenes;

    public MotoGenome(List<FGen> fGenes, List<IGen> iGenes, List<BGen> bGenes)
    {
        InitializeDictionaries();

        foreach(FGen fGen in fGenes)
        {
            AddGen(new FGen(fGen, true));
        }
        foreach (IGen iGen in iGenes)
        {
            AddGen(new IGen(iGen, true));
        }
        foreach (BGen bGen in bGenes)
        {
            AddGen(new BGen(bGen, true));
        }
    }

    public MotoGenome(MotoGenome parent1, MotoGenome parent2)
    {
        InitializeDictionaries();
        Recombine(parent1, parent2);
    }

    public MotoGenome(MotoGenome other)
    {
        m_fGenes = new Dictionary<MotoFGenID, FGen>(other.m_fGenes);
        m_iGenes = new Dictionary < MotoIGenID, IGen >(other.m_iGenes);
        m_bGenes = new Dictionary<MotoBGenID, BGen>(other.m_bGenes);
    }

    public MotoGenome()
    {
        InitializeDictionaries();
    }

    private bool Recombine(MotoGenome parent1, MotoGenome parent2)
    {
        if (parent1.m_fGenes.Count == parent2.m_fGenes.Count
                && parent1.m_iGenes.Count == parent2.m_iGenes.Count
                && parent1.m_bGenes.Count == parent2.m_bGenes.Count)
        {
            foreach (KeyValuePair<MotoFGenID, FGen> pair in parent1.m_fGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    m_fGenes.Add(pair.Key, new FGen(pair.Value, parent2.m_fGenes[pair.Key]));
                }
                else
                {
                    return false;
                }
            }
            foreach (KeyValuePair<MotoIGenID, IGen> pair in parent1.m_iGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    m_iGenes.Add(pair.Key, new IGen(pair.Value, parent2.m_iGenes[pair.Key]));
                }
                else
                {
                    return false;
                }
            }
            foreach (KeyValuePair<MotoBGenID, BGen> pair in parent1.m_bGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    m_bGenes.Add(pair.Key, new BGen(pair.Value, parent2.m_bGenes[pair.Key]));
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }

        return true;
    }
    ////////////////////////////////ADD GEN/////////////////////////////////////////////////

    public void AddGen(MotoFGenID genID, float genValue, float genMinValue, float genMaxValue)
    {
        AddGen(new FGen(genID, genValue, genMinValue, genMaxValue));
    }

    public void AddGen(MotoIGenID genID, int genValue, int genMinValue, int genMaxValue)
    {
        AddGen(new IGen(genID, genValue, genMinValue, genMaxValue));
    }

    public void AddGen(MotoBGenID genID, bool genValue, bool genMinValue, bool genMaxValue)
    {
        AddGen(new BGen(genID, genValue, genMinValue, genMaxValue));
    }

    public void AddGen(FGen fGen)
    {
        m_fGenes.Add(fGen.ID(), new FGen(fGen));
    }

    public void AddGen(IGen iGen)
    {
        m_iGenes.Add(iGen.ID(), new IGen(iGen));
    }

    public void AddGen(BGen bGen)
    {
        m_bGenes.Add(bGen.ID(), new BGen(bGen));
    }
    ////////////////////////////////EXISTS GEN/////////////////////////////////////////////////

    public bool ExistsGen(MotoFGenID ID)
    {
        return m_fGenes.ContainsKey(ID);
    }

    public bool ExistsGen(MotoIGenID ID)
    {
        return m_iGenes.ContainsKey(ID);
    }

    public bool ExistsGen(MotoBGenID ID)
    {
        return m_bGenes.ContainsKey(ID);
    }
    ////////////////////////////////GETTERS/////////////////////////////////////////////////
    public FGen GetGen(MotoFGenID genType)
    {
        return m_fGenes[genType];
    }

    public IGen GetGen(MotoIGenID genType)
    {
        return m_iGenes[genType];
    }

    public BGen GetGen(MotoBGenID genType)
    {
        return m_bGenes[genType];
    }

    private void InitializeDictionaries()
    {
        m_fGenes = new Dictionary<MotoFGenID, FGen>();
        m_iGenes = new Dictionary<MotoIGenID, IGen>();
        m_bGenes = new Dictionary<MotoBGenID, BGen>();
    }
}
