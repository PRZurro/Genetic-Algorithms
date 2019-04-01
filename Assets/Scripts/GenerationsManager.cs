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
    void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        m_generationsRegistry = new List<Generation>();
    }

    public void RegisterGeneration(List<Motorcycle> motorcycles)
    {
        m_generationsRegistry.Add(new Generation(motorcycles, m_currentGeneration));
        CreateButton();
        m_curGenerationText.text = "Generation: " + ++m_currentGeneration;
    }

    public void SetGenerationText(int generationID)
    {
        Debug.Log(generationID);
        m_infoText.text =  "GENERATION " + generationID + ":\n" + m_generationsRegistry[generationID].ToString();
    }

    public void CreateButton()
    {
        GameObject button = Instantiate(m_buttonPrefab, m_scrollViewContent);

        int nonStaticCurrentGeneration = m_currentGeneration;

        button.GetComponent<Button>().onClick.AddListener(() => SetGenerationText(nonStaticCurrentGeneration));
        button.transform.GetChild(0).GetComponent<Text>().text = "" + m_currentGeneration;
    }
}
