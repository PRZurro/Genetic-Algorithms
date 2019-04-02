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

    [SerializeField]
    Transform m_motorcyclesParent;

    /// <summary>
    /// Initialize some components  and members
    /// </summary>
    void Awake()
    {
        m_instance = this;

        // Static members of each class initialization
        FGen.Mutability = m_genesSettings.mutability;
        IGen.Mutability = m_genesSettings.mutability;
        BGen.Mutability = m_genesSettings.mutability;

        Motorcycle.HeadCollisionPenalization = m_gameSettings.HeadCollisionPenalization;
    }

    /// <summary>
    /// Initialize some components  and members
    /// </summary>
    void Start()
    {
        m_motorcycleGenerator = MotorcycleGenerator.Instance;
        m_motorcycleGenerator.SetMotorcyclePrefabs(m_motorcyclePrefabs);

        m_activeCamera = Camera.main;
        m_activeCameraStartPosition = m_activeCamera.transform.position;

        m_motorcycles = new List<Motorcycle>();

        CreateNextGeneration(); // Create the first generation of motorcycles

        m_curGenerationTime = m_gameSettings.GenerationTime;
    }

    /// <summary>
    /// Core application's method
    /// </summary>
    void Update()
    {
        CheckInput();

        m_curGenerationTime -= Time.deltaTime; //Timer
        m_timeLeft.text = "Time left: " + m_curGenerationTime;

        m_motorcycles.Sort(SortByScore); // Sort the motorcycles by them scores

        if (m_curGenerationTime <= 0) // Time's up
        {
            m_curGenerationTime = m_gameSettings.GenerationTime;
            FinishCurrentGeneration();
            CreateNextGeneration();
        }

        SetCameraPositionX(m_motorcycles[m_motorcycles.Count - 1].GetCurrentHeadPositionX()); // Set the camera position to the best motorcycle
    }

    /// <summary>
    /// Hide or show the canvas and Mute or Unmute the audio
    /// </summary>
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

    /// <summary>
    /// Finish current generation: Register it + 50% population killed
    /// </summary>
    private void FinishCurrentGeneration()
    {
        if (m_curGenerationTime >= m_gameSettings.GenerationTime)
        {
            m_motorcycles.Sort(SortByScore);

            GenerationsManager.Instance.RegisterGeneration(m_motorcycles);

            int nMotorcycles = m_motorcycles.Count;
            int max = m_motorcycles.Count / 2;

            List<int> indices = new List<int>();

            // #ThanosSnap kill 50% of population based on the motorcycle performance. Better performance, lower probability to be snapped
            {
                do
                {
                    for (int i = 0, j = nMotorcycles; i < nMotorcycles; i++, j--)
                    {
                        if (!indices.Contains(i))
                        {
                            float snapProbability = j * 1.0f / (float)m_gameSettings.NumberMotorcycles * 100.0f;

                            if (Random.Range(0.0f, 100.0f) <= snapProbability)
                            {
                                Debug.Log("Snap Probability: " + snapProbability + " index: "+ i);
                                indices.Add(i);
                            }
                        }
                    }
                } while (indices.Count <= max);

                indices.Sort();

                for (int i = indices.Count - 1; i > 0; i--)
                {
                    Debug.Log(indices[i]);
                    Motorcycle snappedMotorcycle = m_motorcycles[indices[i]];
                    m_motorcycles.RemoveAt(indices[i]); // #IDontWantToGo
                    Destroy(snappedMotorcycle.gameObject); // #RIP
                }
            }
        }
    }

    /// <summary>
    /// Create a new generation
    /// </summary>
    private void CreateNextGeneration()
    {
        int nChildren = m_gameSettings.NumberMotorcycles;

        if (m_motorcycles.Count > 0) // if is not the first generation (0)
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
        else // First generation
        {
            for (int i = 0; i < nChildren; i++)
            {
                m_motorcycles.Add(m_motorcycleGenerator.CreateMotorcycle(m_genesSettings.masterFloatGenes, m_genesSettings.masterIntegerGenes, m_genesSettings.masterBooleanGenes));
                m_motorcycles[i].transform.parent = m_motorcyclesParent;
            }
        }
    }

    /// <summary>
    /// Method used by the canvas's slider to scale game time
    /// </summary>
    /// <param name="timeScaler"></param>
    public void SetGameTimeScale(Slider timeScaler)
    {
        Time.timeScale = timeScaler.value;
        m_timeScaleText.text = "Time scale: x" + timeScaler.value;
    }

    /// <summary>
    /// Sort motorcycles by score
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    static int SortByScore(Motorcycle p1, Motorcycle p2)
    {
        return p1.score().CompareTo(p2.score());
    }

    /// <summary>
    /// Set camera position X to the input received (with some conditions)
    /// </summary>
    /// <param name="positionX"></param>
    private void SetCameraPositionX(float positionX)
    {
        Vector3 cameraPosition = m_activeCamera.transform.position;
        float newPositionX = positionX - m_gameSettings.CameraLeftOffset; // Input position + desired offset 

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
