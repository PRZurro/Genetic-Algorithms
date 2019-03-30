using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<int, Motorcycle> motorcycles;

    MotorcycleGenerator motorcycleGenerator;

    Camera activeCamera;

    [SerializeField]
    GameSettings gameSettings;
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


        float posX = 0.0f;
        float posY = 0.0f;

        for (int i = 0; i < 10; i++, posY = 0.0f, posX += 250.0f)
        {
            for (int j = 0; j < 10; j++, posY += 250.0f)
            {
                MotoGenome masterMotoGenome = new MotoGenome(genesSettings.MasterFloatGenes, genesSettings.MasterIntegerGenes, genesSettings.MasterBooleanGenes);
                motorcycleGenerator.CreateMotorcycle(masterMotoGenome).transform.position = new Vector3(posX, posY, 0.0f);
            }
        }
    }
}
