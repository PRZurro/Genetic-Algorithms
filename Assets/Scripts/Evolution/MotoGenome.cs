using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MotoGenome 
{
    public Dictionary<MotoFGenID, FGen> fGenes;
    public Dictionary<MotoIGenID, IGen> iGenes;
    public Dictionary<MotoBGenID, BGen> bGenes;

    public MotoGenome(MotoGenome parent1, MotoGenome parent2)
    {
        Recombine(parent1, parent2);
    }

    public MotoGenome(MotoGenome other)
    {
        fGenes = other.fGenes;
        iGenes = other.iGenes;
        bGenes = other.bGenes;
    }

    public MotoGenome()
    {}

    private bool Recombine(MotoGenome parent1, MotoGenome parent2)
    {
        if (parent1.fGenes.Count == parent2.fGenes.Count
                && parent1.iGenes.Count == parent2.iGenes.Count
                && parent1.bGenes.Count == parent2.bGenes.Count)
        {
            foreach (KeyValuePair<MotoFGenID, FGen> pair in parent1.fGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    fGenes[pair.Key] = new FGen(pair.Value, parent2.fGenes[pair.Key]);
                }
                else
                {
                    return false;
                }
            }
            foreach (KeyValuePair<MotoIGenID, IGen> pair in parent1.iGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    iGenes[pair.Key] = new IGen(pair.Value, parent2.iGenes[pair.Key]);
                }
                else
                {
                    return false;
                }
            }
            foreach (KeyValuePair<MotoBGenID, BGen> pair in parent1.bGenes)
            {
                if (parent2.ExistsGen(pair.Key))
                {
                    bGenes[pair.Key] = new BGen(pair.Value, parent2.bGenes[pair.Key]);
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
        fGenes[genID] = new FGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddIGen(MotoIGenID genID, int genValue, int genMinValue, int genMaxValue)
    {
        iGenes[genID] = new IGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddBGen(MotoBGenID genID, bool genValue, bool genMinValue, bool genMaxValue)
    {
        bGenes[genID] = new BGen(genID, genValue, genMinValue, genMaxValue);
    }

    public void AddFGen(FGen fGen)
    {
        fGenes[fGen.ID()] = new FGen(fGen);
    }

    public void AddIGen(IGen iGen)
    {
        iGenes[iGen.ID()] = new IGen(iGen);
    }

    public void AddBGen(BGen bGen)
    {
        bGenes[bGen.ID()] = new BGen(bGen);
    }
    ////////////////////////////////EXISTS GEN/////////////////////////////////////////////////

    public bool ExistsGen(MotoFGenID ID)
    {
        return fGenes.ContainsKey(ID);
    }

    public bool ExistsGen(MotoIGenID ID)
    {
        return iGenes.ContainsKey(ID);
    }

    public bool ExistsGen(MotoBGenID ID)
    {
        return bGenes.ContainsKey(ID);
    }
    ////////////////////////////////GETTERS/////////////////////////////////////////////////
    public FGen GetFGen(MotoFGenID genType)
    {
        return fGenes[genType];
    }

    public IGen GetIGen(MotoIGenID genType)
    {
        return iGenes[genType];
    }

    public BGen GetBGen(MotoBGenID genType)
    {
        return bGenes[genType];
    }
}
