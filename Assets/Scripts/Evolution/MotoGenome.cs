using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MotoGenome 
{
    Dictionary<MotoFGenID, FGen> fGenes;
    Dictionary<MotoIGenID, IGen> iGenes;
    Dictionary<MotoBGenID, BGen> bGenes;

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

    public void AddFGen(MotoFGenID genID, float genValue, float genMinValue, float genMaxValue)
    {
        fGenes[genID] = new FGen(genValue, genMinValue, genMaxValue);
    }

    public void AddIGen(MotoIGenID genID, int genValue, int genMinValue, int genMaxValue)
    {
        iGenes[genID] = new IGen(genValue, genMinValue, genMaxValue);
    }

    public void AddBGen(MotoBGenID genType, bool genValue, bool genMinValue, bool genMaxValue)
    {
        bGenes[genType] = new BGen(genValue, genMinValue, genMaxValue);
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

    public bool ExistsGen(byte genID, byte dictionary)
    {
        switch(dictionary)
        {
            case 0:
                return fGenes.ContainsKey((MotoFGenID)genID);
            case 1:
                return iGenes.ContainsKey((MotoIGenID)genID);
            case 2:
                return bGenes.ContainsKey((MotoBGenID)genID);
            default:
                return false;
        }
    }

    private bool Recombine(MotoGenome parent1, MotoGenome parent2)
    {
        if(parent1.fGenes.Count == parent2.fGenes.Count
                && parent1.iGenes.Count == parent2.iGenes.Count
                && parent1.bGenes.Count == parent2.bGenes.Count)
        {
            if(parent1.fGenes.Count == System.Enum.GetValues(typeof(MotoFGenID)).Length
                && parent1.iGenes.Count == System.Enum.GetValues(typeof(MotoIGenID)).Length
                && parent1.bGenes.Count == System.Enum.GetValues(typeof(MotoBGenID)).Length)
            {
                foreach (MotoFGenID fGenID in (MotoFGenID[])System.Enum.GetValues(typeof(MotoFGenID)))
                {
                    fGenes[fGenID] = new FGen(parent1.fGenes[fGenID], parent2.fGenes[fGenID]);
                }
                foreach (MotoIGenID iGenID in (MotoIGenID[])System.Enum.GetValues(typeof(MotoIGenID)))
                {
                    iGenes[iGenID] = new IGen(parent1.iGenes[iGenID], parent2.iGenes[iGenID]);
                }
                foreach (MotoBGenID bGenID in (MotoBGenID[])System.Enum.GetValues(typeof(MotoBGenID)))
                {
                    bGenes[bGenID] = new BGen(parent1.bGenes[bGenID], parent2.bGenes[bGenID]);
                }

                return true;
            }
        }
        return false;
    }
}