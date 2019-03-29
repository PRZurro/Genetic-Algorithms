using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<int, Motorcycle> motorcycles;

    MotorcycleGenerator motorcycleGenerator;

    Camera activeCamera;

    [SerializeField]
    GenesSettings genesSettings;
    [SerializeField]
    MotorcyclePrefabs motorcyclePrefabs;

    void Start()
    {
        motorcycleGenerator = MotorcycleGenerator.Instance;
        motorcycleGenerator.SetMotorcyclePrefabs(motorcyclePrefabs);

        activeCamera = Camera.main;

        FGen.Mutability = genesSettings.Mutability;
        IGen.Mutability = genesSettings.Mutability;
        BGen.Mutability = genesSettings.Mutability;
    }  
}
