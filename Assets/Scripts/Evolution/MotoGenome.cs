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
        foreach(FGen fGen in fGenes)
        {
            m_fGenes[fGen.ID()] = new FGen(fGen, true);
        }
        foreach (IGen iGen in iGenes)
        {
            m_iGenes[iGen.ID()] = new IGen(iGen, true);
        }
        foreach (BGen bGen in bGenes)
        {
            m_bGenes[bGen.ID()] = new BGen(bGen, true);
        }
    }

    public MotoGenome(MotoGenome parent1, MotoGenome parent2)
    {
        Recombine(parent1, parent2);
    }

    public MotoGenome(MotoGenome other)
    {
        m_fGenes = other.m_fGenes;
        m_iGenes = other.m_iGenes;
        m_bGenes = other.m_bGenes;
    }

    public MotoGenome()
    { }

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
                    m_fGenes[pair.Key] = new FGen(pair.Value, parent2.m_fGenes[pair.Key]);
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
                    m_iGenes[pair.Key] = new IGen(pair.Value, parent2.m_iGenes[pair.Key]);
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
                    m_bGenes[pair.Key] = new BGen(pair.Value, parent2.m_bGenes[pair.Key]);
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

    public void AddFGen(MotoFGenID genID, float genValue, float genMinValue, float genMaxValue)
    {
        m_fGenes[genID] = new FGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddIGen(MotoIGenID genID, int genValue, int genMinValue, int genMaxValue)
    {
        m_iGenes[genID] = new IGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddBGen(MotoBGenID genID, bool genValue, bool genMinValue, bool genMaxValue)
    {
        m_bGenes[genID] = new BGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddFGen(FGen fGen)
    {
        m_fGenes[fGen.ID()] = new FGen(fGen);
    }

    public void AddIGen(IGen iGen)
    {
        m_iGenes[iGen.ID()] = new IGen(iGen);
    }

    public void AddBGen(BGen bGen)
    {
        m_bGenes[bGen.ID()] = new BGen(bGen);
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
}
