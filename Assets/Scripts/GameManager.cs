using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameSettings m_gameSettings;

    [SerializeField]
    GenesSettings m_genesSettings;

    [SerializeField]
    MotorcyclePrefabs m_motorcyclePrefabs;

    [SerializeField]
    Canvas m_mainCanvas;

    [SerializeField]
    GameObject m_audioContainer;

    [SerializeField]
    Text m_timeScaleText;

    Dictionary<int, Motorcycle> m_motorcycles;

    static GameManager m_instance;

    MotorcycleGenerator m_motorcycleGenerator;

    Camera m_activeCamera;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_motorcycleGenerator = MotorcycleGenerator.Instance;
        m_motorcycleGenerator.SetMotorcyclePrefabs(m_motorcyclePrefabs);

        m_activeCamera = Camera.main;

        FGen.Mutability = m_genesSettings.Mutability;
        IGen.Mutability = m_genesSettings.Mutability;
        BGen.Mutability = m_genesSettings.Mutability;

        float timeInterval = 0.0f;
        for(int i = 0; i < 12; i++ )
            StartCoroutine(CreateMotorcycle(i * timeInterval));

        //MotoGenome masterMotoGenome = new MotoGenome(m_genesSettings.MasterFloatGenes, m_genesSettings.MasterIntegerGenes, m_genesSettings.MasterBooleanGenes);
        //m_motorcycleGenerator.CreateMotorcycle(masterMotoGenome).transform.position = new Vector3(posX, posY, 0.0f);
    }

    void Update()
    {
        CheckInput();


    }

    IEnumerator CreateMotorcycle(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        Genome masterMotoGenome = new Genome(m_genesSettings.MasterFloatGenes, m_genesSettings.MasterIntegerGenes, m_genesSettings.MasterBooleanGenes);
        Debug.Log(masterMotoGenome.ToString());
        m_motorcycleGenerator.CreateMotorcycle(masterMotoGenome).transform.position = new Vector3(0.0f, 20.0f, 0.0f);
    }

    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_mainCanvas.enabled = m_mainCanvas.isActiveAndEnabled ? false : true;
        } 

        if(Input.GetKeyDown(KeyCode.M))
        {
            m_audioContainer.SetActive(m_audioContainer.activeSelf ? false : true);
        }
    }

    public void SetGameTimeScale(Slider timeScaler)
    {
        Time.timeScale = timeScaler.value;
        m_timeScaleText.text = "Time scale: x"+ timeScaler.value;
    }
}
