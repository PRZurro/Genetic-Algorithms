using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<int, Motorcycle> m_motorcycles;

    MotorcycleGenerator m_motorcycleGenerator;

    Camera m_activeCamera;

    [SerializeField]
    GameSettings m_gameSettings;
    [SerializeField]
    GenesSettings m_genesSettings;
    [SerializeField]
    MotorcyclePrefabs m_motorcyclePrefabs;

    void Start()
    {
        m_motorcycleGenerator = MotorcycleGenerator.Instance;
        m_motorcycleGenerator.SetMotorcyclePrefabs(m_motorcyclePrefabs);

        m_activeCamera = Camera.main;

        FGen.Mutability = m_genesSettings.Mutability;
        IGen.Mutability = m_genesSettings.Mutability;
        BGen.Mutability = m_genesSettings.Mutability;

        float timeInterval = 2.0f;
        for(int i = 0; i < 12; i++ )
            StartCoroutine(CreateMotorcycle(i * timeInterval));

        //MotoGenome masterMotoGenome = new MotoGenome(m_genesSettings.MasterFloatGenes, m_genesSettings.MasterIntegerGenes, m_genesSettings.MasterBooleanGenes);
        //m_motorcycleGenerator.CreateMotorcycle(masterMotoGenome).transform.position = new Vector3(posX, posY, 0.0f);
        Time.timeScale = 1.0f;
    }

    IEnumerator CreateMotorcycle(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Genome masterMotoGenome = new Genome(m_genesSettings.MasterFloatGenes, m_genesSettings.MasterIntegerGenes, m_genesSettings.MasterBooleanGenes);
        Debug.Log(masterMotoGenome.ToString());
        m_motorcycleGenerator.CreateMotorcycle(masterMotoGenome).transform.position = new Vector3(0.0f, 20.0f, 0.0f);
    }
}
