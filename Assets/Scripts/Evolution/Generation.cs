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
    
    /// <summary>
    /// Constructor of a generation info container. Save the best genome and do an arithmetic average of each gen
    /// </summary>
    /// <param name="motorcycles"></param>
    /// <param name="ID"></param>
    public Generation(List<Motorcycle> motorcycles, int ID)
    {
        m_ID = ID;

        // Save the best genome and best score
        m_bestGenome = new Genome(motorcycles[0].genome());
        m_bestScore = motorcycles[motorcycles.Count -1].score();

        // Get all available genes for later average calculation 
        List<FGenID> fGenIDs = motorcycles[0].genome().GetFGenesKeys();
        List<IGenID> iGenIDs = motorcycles[0].genome().GetIGenesKeys();
        List<BGenID> bGenIDs = motorcycles[0].genome().GetBGenesKeys();

        m_fGenesAverage = new Dictionary<FGenID, float>();
        m_iGenesAverage = new Dictionary<IGenID, float>();
        m_bGenesAverage = new Dictionary<BGenID, float>();

        // Add all the genes and inialize to 0
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

        // Summatory of each motorcycle gen
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

        // Divide each summatory by the number of motorcycles
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

        // Save the string for later
        m_bestGenomeString = BestGenomeToString();
        m_averageGenomeString = AverageToString();
    }

    /// <summary>
    /// Comparison method to compare which generation is better
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsBetterThan(Generation other)
    {
        return (m_scoreAverage >= other.m_scoreAverage);
    }

    /// <summary>
    /// Best genome to string
    /// </summary>
    /// <returns></returns>
    private string BestGenomeToString()
    {
        return "Score: " + m_bestScore + "\n\n" + m_bestGenome.ToString();
    }

    /// <summary>
    /// Average genome to string
    /// </summary>
    /// <returns></returns>
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
    
    /// <summary>
    /// Generation to string (use the strings saved before)
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return m_bestGenomeString + "\n\n " + m_averageGenomeString;
    }
}

