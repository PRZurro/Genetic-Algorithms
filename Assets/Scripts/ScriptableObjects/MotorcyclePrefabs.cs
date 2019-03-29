using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMotorcyclePrefabsContainer", menuName = "Motorcycle Prefabs Container", order = 52)]
public class MotorcyclePrefabs : ScriptableObject
{
    public List<GameObject> BasePrefabs;
    public GameObject WheelPrefab;
    public GameObject SwingArmPrefab;
}
