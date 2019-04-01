using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerationsManager : MonoBehaviour
{
    static int m_currentGeneration = 0;

    static GenerationsManager m_instance;

    List<Generation> m_generationsRegistry;

    [SerializeField]
    Text m_infoText, m_curGenerationText;

    [SerializeField]
    Transform m_scrollViewContent;

    [SerializeField]
    GameObject m_buttonPrefab;

    Vector3 m_lastButtonPosition;

    /// <summary>
    /// Get the instance
    /// </summary>
    public static GenerationsManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject();
                m_instance = go.AddComponent<GenerationsManager>();
                go.name = "Generations Manager";
            }
            return m_instance;
        }
    }

    /// <summary>
    /// Initialize some components
    /// </summary>
    void Awake()
    {
        m_instance = this;
    }

    /// <summary>
    /// Initialize some components
    /// </summary>
    private void Start()
    {
        m_generationsRegistry = new List<Generation>();
    }

    /// <summary>
    /// Register a new generation 
    /// </summary>
    /// <param name="motorcycles"></param>
    public void RegisterGeneration(List<Motorcycle> motorcycles)
    {
        m_generationsRegistry.Add(new Generation(motorcycles, m_currentGeneration));
        CreateButton();
        m_curGenerationText.text = "Generation: " + ++m_currentGeneration;
    }

    /// <summary>
    /// Method called by onClick method of buttons
    /// </summary>
    /// <param name="generationID"></param>
    public void SetGenerationText(int generationID)
    {
        Debug.Log(generationID);
        m_infoText.text =  "GENERATION " + generationID + ":\n" + m_generationsRegistry[generationID].ToString();
    }

    /// <summary>
    /// Create a new method with a delegate to SetGenerationText
    /// </summary>
    public void CreateButton()
    {
        GameObject button = Instantiate(m_buttonPrefab, m_scrollViewContent);

        int nonStaticCurrentGeneration = m_currentGeneration;

        button.GetComponent<Button>().onClick.AddListener(() => SetGenerationText(nonStaticCurrentGeneration));
        button.transform.GetChild(0).GetComponent<Text>().text = "" + m_currentGeneration;
    }
}
