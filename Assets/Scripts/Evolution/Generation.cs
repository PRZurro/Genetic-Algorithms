using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Generation
{
    public int m_ID;

    public float m_bestScore;
    public Genome m_bestGenome;

    public float m_scoreAverage;
    public Genome m_genomeAverage;

    public Dictionary<FGenID, float> m_fGenesAverage;
    public Dictionary<IGenID, float> m_iGenesAverage;
    public Dictionary<BGenID, float> m_bGenesAverage;

    string m_bestGenomeString;
    string m_averageGenomeString;

    public Generation(List<Motorcycle> motorcycles, int ID)
    {
        m_ID = ID;

        m_bestGenome = new Genome(motorcycles[0].genome());
        m_bestScore = motorcycles[motorcycles.Count -1].score();

        List<FGenID> fGenIDs = motorcycles[0].genome().GetFGenesKeys();
        List<IGenID> iGenIDs = motorcycles[0].genome().GetIGenesKeys();
        List<BGenID> bGenIDs = motorcycles[0].genome().GetBGenesKeys();

        m_fGenesAverage = new Dictionary<FGenID, float>();
        m_iGenesAverage = new Dictionary<IGenID, float>();
        m_bGenesAverage = new Dictionary<BGenID, float>();

        foreach (FGenID fGenID in fGenIDs)
        {
            m_fGenesAverage.Add(fGenID, 0.0f);
        }
        foreach (IGenID iGenID in iGenIDs)
        {
            m_iGenesAverage.Add(iGenID, 0.0f);
        }
        foreach (BGenID bGenID in bGenIDs)
        {
            m_bGenesAverage.Add(bGenID, 0.0f);
        }

        m_scoreAverage = 0.0f;

        foreach (Motorcycle motorcycle in motorcycles)
        {
            m_scoreAverage += motorcycle.score();

            foreach (FGenID fGenID in fGenIDs)
            {
                m_fGenesAverage[fGenID] += motorcycle.genome().GetGen(fGenID).Value();
            }
            foreach (IGenID iGenID in iGenIDs)
            {
                m_iGenesAverage[iGenID] += motorcycle.genome().GetGen(iGenID).Value();
            }
            foreach (BGenID bGenID in bGenIDs)
            {
                if (motorcycle.genome().GetGen(bGenID).Value())
                {
                    m_bGenesAverage[bGenID] += 1.0f;
                }
            }
        }

        int nMotorcycles = motorcycles.Count;

        m_scoreAverage *= 1.0f / (float)nMotorcycles;

        foreach(FGenID fGenID in fGenIDs)
        {
            m_fGenesAverage[fGenID] *= 1.0f / (float)nMotorcycles;
        }
        foreach (IGenID iGenID in iGenIDs)
        {
            m_iGenesAverage[iGenID] *= 1.0f / (float)nMotorcycles;
        }
        foreach (BGenID bGenID in bGenIDs)
        {
            m_bGenesAverage[bGenID] *= 1.0f / (float)nMotorcycles;
        }

        m_bestGenomeString = BestGenomeToString();
        m_averageGenomeString = AverageToString();
    }

    public bool IsBetterThan(Generation other)
    {
        return (m_scoreAverage >= other.m_scoreAverage);
    }

    private string BestGenomeToString()
    {
        return "Score: " + m_bestScore + "\n\n" + m_bestGenome.ToString();
    }

    private string AverageToString()
    {
        string genomeString = "---- AVERAGE FLOAT GENES ----\n";

        foreach (KeyValuePair<FGenID, float> pair in m_fGenesAverage)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value + "\n";
        }

        genomeString += "\n ---- INTEGER GENES ---- \n";

        foreach (KeyValuePair<IGenID, float> pair in m_iGenesAverage)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value + "\n";
        }

        genomeString += "\n ---- BOOLEAN GENES ----\n";

        foreach (KeyValuePair<BGenID, float> pair in m_bGenesAverage)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value + "\n";
        }

        return "Average score: " + m_scoreAverage + "\n\n" + genomeString;
    }
    

    public override string ToString()
    {
        return m_bestGenomeString + "\n\n " + m_averageGenomeString;
    }
}

