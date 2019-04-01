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
    Text m_timeScaleText, m_timeLeft;

    List<Motorcycle> m_motorcycles;

    static GameManager m_instance;

    MotorcycleGenerator m_motorcycleGenerator;

    Camera m_activeCamera;
    Vector3 m_activeCameraStartPosition;

    float m_curGenerationTime;

    Transform m_motorcyclesParent;

    void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        m_motorcycleGenerator = MotorcycleGenerator.Instance;
        m_motorcycleGenerator.SetMotorcyclePrefabs(m_motorcyclePrefabs);

        m_activeCamera = Camera.main;
        m_activeCameraStartPosition = m_activeCamera.transform.position;

        FGen.Mutability = m_genesSettings.mutability;
        IGen.Mutability = m_genesSettings.mutability;
        BGen.Mutability = m_genesSettings.mutability;

        m_motorcycles = new List<Motorcycle>();
        PrepareNextGeneration();
        m_curGenerationTime = m_gameSettings.m_generationTime;

        m_motorcyclesParent = GameObject.Find("Motorcycles").transform;

        Motorcycle.HeadCollisionPenalization = m_gameSettings.m_headCollisionPenalization;
    }

    void Update()
    {
        CheckInput();

        m_curGenerationTime -= Time.deltaTime;
        m_timeLeft.text = "Time left: " + m_curGenerationTime;

        m_motorcycles.Sort(SortByScore);

        if (m_curGenerationTime <= 0)
        {

            m_curGenerationTime = m_gameSettings.m_generationTime;
            FinishCurrentGeneration();
            PrepareNextGeneration();
        }

        SetCameraPositionX(m_motorcycles[m_motorcycles.Count-1].GetCurrentHeadPositionX());
    }

    IEnumerator CreateMotorcycle(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_mainCanvas.enabled = m_mainCanvas.isActiveAndEnabled ? false : true;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            m_audioContainer.SetActive(m_audioContainer.activeSelf ? false : true);
        }
    }


    private void FinishCurrentGeneration()
    {

        foreach(Motorcycle moto in m_motorcycles)
        {
            Debug.Log(moto.score());
        }

        if (m_curGenerationTime >= m_gameSettings.m_generationTime)
        {
            m_motorcycles.Sort(SortByScore);

            GenerationsManager.Instance.RegisterGeneration(m_motorcycles);

            int nMotorcycles = m_motorcycles.Count;
            int max = m_motorcycles.Count / 2;

            List<int> indices = new List<int>();

            // #ThanosSnap kill 50% of population based on the motorcycle performance. Better performance, lower probability to be snapped

            do
            {
                for (int i = 0, j = nMotorcycles; i < nMotorcycles; i++, j--)
                {
                    if (!indices.Contains(i))
                    {
                        float snapProbability = j * 1.0f / (float)m_gameSettings.m_numberMotorcycles;

                        if (Random.Range(0.0f, 100.0f) <= snapProbability)
                        {
                            indices.Add(i);
                            //i++;
                        }
                    }
                }
            } while (indices.Count <= max);

            indices.Sort();

            for(int i = indices.Count - 1; i > 0; i-- )
            {
                Debug.Log(indices[i]);
                Motorcycle snappedMotorcycle = m_motorcycles[indices[i]];
                m_motorcycles.RemoveAt(indices[i]);
                Destroy(snappedMotorcycle.gameObject); // #IDontWantToGo
            }
        }
    }

    private void PrepareNextGeneration()
    {
        int nChildren = m_gameSettings.m_numberMotorcycles;

        if (m_motorcycles.Count > 0)
        {
            int nParents = m_motorcycles.Count;

            List<Motorcycle> childrens = new List<Motorcycle>();

            for (int i = 0; i < nChildren; i++)
            {
                int parent1 = Random.Range(0, nParents), parent2 = Random.Range(0, nParents);
                Debug.Log(parent1 + "  " + parent2);
                while (parent1 == parent2)
                {
                    Debug.Log(parent1 + "  " + parent2);
                    parent2 = Random.Range(0, nParents);
                }

                childrens.Add(m_motorcycleGenerator.CreateMotorcycle(m_motorcycles[parent1], m_motorcycles[parent2]));
                childrens[i].transform.parent = m_motorcyclesParent;  
            }

            foreach (Motorcycle moto in m_motorcycles)
            {
                Destroy(moto.gameObject);
            }
            m_motorcycles.Clear();

            m_motorcycles = new List<Motorcycle>(childrens);
            childrens.Clear();
        }
        else
        {
            for (int i = 0; i < nChildren; i++)
            {
                m_motorcycles.Add(m_motorcycleGenerator.CreateMotorcycle(m_genesSettings.masterFloatGenes, m_genesSettings.masterIntegerGenes, m_genesSettings.masterBooleanGenes));
                m_motorcycles[i].transform.parent = m_motorcyclesParent;
            }
        }
    }

    public void SetGameTimeScale(Slider timeScaler)
    {
        Time.timeScale = timeScaler.value;
        m_timeScaleText.text = "Time scale: x" + timeScaler.value;
    }

    static int SortByScore(Motorcycle p1, Motorcycle p2)
    {
        return p1.score().CompareTo(p2.score());
    }

    private void SetCameraPositionX(float positionX)
    {
        Vector3 cameraPosition = m_activeCamera.transform.position;
        float newPositionX = positionX - m_gameSettings.m_cameraLeftOffset;

        if (newPositionX > m_activeCameraStartPosition.x)
        {
            cameraPosition.x = newPositionX;
            m_activeCamera.transform.position = cameraPosition;
        }
        else
        {
            m_activeCamera.transform.position = m_activeCameraStartPosition;
        }
    }
}
