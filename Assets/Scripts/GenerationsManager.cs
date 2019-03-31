using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenerationsManager : MonoBehaviour
{
    static int currentGeneration = -1;

    List<Generation> m_generationsRegistry;
    

    private void Start()
    {
        m_generationsRegistry = new List<Generation>();
    }

    public void RegisterGeneration(List<Motorcycle> motorcycles)
    {
        m_generationsRegistry.Add(new Generation(motorcycles, ++currentGeneration));
    }

    public void SetGenerationText(int generationID)
    {

    }
}
