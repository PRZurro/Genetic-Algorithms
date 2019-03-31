using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GenerationsManager : MonoBehaviour
{
    static int m_currentGeneration = -1;
    static GenerationsManager m_instance;

    List<Generation> m_generationsRegistry;

    [SerializeField]
    Text m_infoText;

    [SerializeField]
    Transform m_scrollViewContent;

    [SerializeField]
    GameObject m_buttonPrefab;

    Vector3 m_lastButtonPosition;

    void Awake()
    {
        m_instance = this;
    }

    private void Start()
    {
        m_lastButtonPosition = new Vector3(0.0f, 0.0f, 0.0f);
        m_generationsRegistry = new List<Generation>();
    }

    public void RegisterGeneration(List<Motorcycle> motorcycles)
    {
        m_generationsRegistry.Add(new Generation(motorcycles, ++m_currentGeneration));
        CreateButton();
    }

    public void SetGenerationText(int generationID)
    {
        m_infoText.text =  m_generationsRegistry[generationID].ToString();
    }

    public void CreateButton()
    {
        GameObject button = Instantiate(m_buttonPrefab, m_scrollViewContent);
        button.GetComponent<Button>().onClick.AddListener(() => SetGenerationText(m_currentGeneration));
        button.transform.GetChild(0).GetComponent<Text>().text = "" + m_currentGeneration;
    }
}
