using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Genome 
{
    Dictionary<FGenID, FGen> m_fGenes;
    Dictionary<IGenID, IGen> m_iGenes;
    Dictionary<BGenID, BGen> m_bGenes;

    /// <summary>
    /// Constructor that needs separated lists of genes to be initialized
    /// </summary>
    /// <param name="fGenes"></param>
    /// <param name="iGenes"></param>
    /// <param name="bGenes"></param>
    public Genome(List<FGen> fGenes, List<IGen> iGenes, List<BGen> bGenes)
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

    /// <summary>
    /// Recombination constructor
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    public Genome(Genome parent1, Genome parent2)
    {
        InitializeDictionaries();
        Recombine(parent1, parent2);
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    public Genome(Genome other)
    {
        m_fGenes = new Dictionary<FGenID, FGen>(other.m_fGenes);
        m_iGenes = new Dictionary < IGenID, IGen >(other.m_iGenes);
        m_bGenes = new Dictionary<BGenID, BGen>(other.m_bGenes);
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public Genome()
    {
        InitializeDictionaries();
    }

    /// <summary>
    /// Combine two parents into this using the gen class methods
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    /// <returns></returns>
    private bool Recombine(Genome parent1, Genome parent2)
    {
        if (parent1.m_fGenes.Count == parent2.m_fGenes.Count
                && parent1.m_iGenes.Count == parent2.m_iGenes.Count
                && parent1.m_bGenes.Count == parent2.m_bGenes.Count)
        {
            foreach (KeyValuePair<FGenID, FGen> pair in parent1.m_fGenes)
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
            foreach (KeyValuePair<IGenID, IGen> pair in parent1.m_iGenes)
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
            foreach (KeyValuePair<BGenID, BGen> pair in parent1.m_bGenes)
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

    /// <summary>
    /// Create a gen of type float with the input paramaters
    /// </summary>
    /// <param name="genID"></param>
    /// <param name="genValue"></param>
    /// <param name="genMinValue"></param>
    /// <param name="genMaxValue"></param>
    public void AddGen(FGenID genID, float genValue, float genMinValue, float genMaxValue)
    {
        AddGen(new FGen(genID, genValue, genMinValue, genMaxValue));
    }

    /// <summary>
    /// Create a gen of type integer with the input paramaters
    /// </summary>
    /// <param name="genID"></param>
    /// <param name="genValue"></param>
    /// <param name="genMinValue"></param>
    /// <param name="genMaxValue"></param>
    public void AddGen(IGenID genID, int genValue, int genMinValue, int genMaxValue)
    {
        AddGen(new IGen(genID, genValue, genMinValue, genMaxValue));
    }

    /// <summary>
    /// Create a gen of type boolean with the input paramaters
    /// </summary>
    /// <param name="genID"></param>
    /// <param name="genValue"></param>
    /// <param name="genMinValue"></param>
    /// <param name="genMaxValue"></param>
    public void AddGen(BGenID genID, bool genValue, bool genMinValue, bool genMaxValue)
    {
        AddGen(new BGen(genID, genValue, genMinValue, genMaxValue));
    }

    /// <summary>
    /// Add a gen of type float (copy)
    /// </summary>
    /// <param name="fGen"></param>
    public void AddGen(FGen fGen)
    {
        m_fGenes.Add(fGen.ID(), new FGen(fGen));
    }

    /// <summary>
    /// Add a gen of type integer (copy)
    /// </summary>
    /// <param name="iGen"></param>
    public void AddGen(IGen iGen)
    {
        m_iGenes.Add(iGen.ID(), new IGen(iGen));
    }

    /// <summary>
    /// Add a gen of type integer (copy)
    /// </summary>
    /// <param name="bGen"></param>
    public void AddGen(BGen bGen)
    {
        m_bGenes.Add(bGen.ID(), new BGen(bGen));
    }
    ////////////////////////////////EXISTS GEN/////////////////////////////////////////////////

    /// <summary>
    /// Check if exists a gen of type float with input key
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public bool ExistsGen(FGenID ID)
    {
        return m_fGenes.ContainsKey(ID);
    }

    /// <summary>
    /// Check if exists a gen of type integer with input key
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public bool ExistsGen(IGenID ID)
    {
        return m_iGenes.ContainsKey(ID);
    }

    /// <summary>
    /// Check if exists a gen of type boolean with input key 
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public bool ExistsGen(BGenID ID)
    {
        return m_bGenes.ContainsKey(ID);
    }
    ////////////////////////////////GETTERS/////////////////////////////////////////////////
    
    /// <summary>
    /// Get a fGen by key
    /// </summary>
    /// <param name="genType"></param>
    /// <returns></returns>
    public FGen GetGen(FGenID genType)
    {
        return m_fGenes[genType];
    }

    /// <summary>
    /// Get an iGen by key
    /// </summary>
    /// <param name="genType"></param>
    /// <returns></returns>
    public IGen GetGen(IGenID genType)
    {
        return m_iGenes[genType];
    }

    /// <summary>
    /// Get an bGen by key
    /// </summary>
    /// <param name="genType"></param>
    /// <returns></returns>
    public BGen GetGen(BGenID genType)
    {
        return m_bGenes[genType];
    }

    /// <summary>
    /// Initialize all the collections
    /// </summary>
    private void InitializeDictionaries()
    {
        m_fGenes = new Dictionary<FGenID, FGen>();
        m_iGenes = new Dictionary<IGenID, IGen>();
        m_bGenes = new Dictionary<BGenID, BGen>();
    }

    /// <summary>
    /// Get a list of the fGen keys available
    /// </summary>
    /// <returns></returns>
    public List<FGenID> GetFGenesKeys()
    {
        List<FGenID> keys = new List<FGenID>();
        foreach(KeyValuePair<FGenID, FGen> pair in m_fGenes)
        {
            keys.Add(pair.Key);
        }

        return keys;
    }

    /// <summary>
    /// Get a list of the iGen keys availables
    /// </summary>
    /// <returns></returns>
    public List<IGenID> GetIGenesKeys()
    {
        List<IGenID> keys = new List<IGenID>();
        foreach (KeyValuePair<IGenID, IGen> pair in m_iGenes)
        {
            keys.Add(pair.Key);
        }

        return keys;
    }

    /// <summary>
    /// Get a list of the bGen keys available
    /// </summary>
    /// <returns></returns>
    public List<BGenID> GetBGenesKeys()
    {
        List<BGenID> keys = new List<BGenID>();
        foreach (KeyValuePair<BGenID, BGen> pair in m_bGenes)
        {
            keys.Add(pair.Key);
        }

        return keys;
    }
    
    /// <summary>
    /// Genome to String
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string genomeString = "---- FLOAT GENES ----\n";

        foreach(KeyValuePair<FGenID, FGen> pair in m_fGenes)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value.Value() + "\n";
        }

        genomeString += "\n ---- INTEGER GENES ---- \n";

        foreach (KeyValuePair<IGenID, IGen> pair in m_iGenes)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value.Value() + "\n";
        }

        genomeString += "\n ---- BOOLEAN GENES ----\n";

        foreach (KeyValuePair<BGenID, BGen> pair in m_bGenes)
        {
            genomeString += pair.Key.ToString() + ": " + pair.Value.Value() + "\n";
        }

        return genomeString;
    }
}
