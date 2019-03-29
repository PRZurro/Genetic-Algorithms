using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGenesSettings", menuName = "Genes Settings", order = 51)]
public class GenesSettings : ScriptableObject
{
    [Header("Floating-point type genes")]
    [Space(10)]

    [Header("MASTER GENES")]
    public List<FGen> MasterFloatGenes;

    [Header("Integer type genes")]
    public List<IGen> MasterIntegerGenes;

    [Header("Boolean type genes")]
    public List<BGen> MasterBooleanGenes;

    [Space(10)]
    [Header("OTHER")]
    [Range(300, 20000)]
    public float FinishLinePosX = 300;

    [Tooltip("Must be an even number greater than zero")]
    [Range(100,1000)]
    public int NumberMotorcycles = 100;

    [Range(0.0f, 100.0f)]
    public float Mutability;
}
