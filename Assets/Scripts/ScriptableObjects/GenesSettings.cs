using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGenesSettings", menuName = "Genes Settings", order = 51)]
public class GenesSettings : ScriptableObject
{

    [Range(0.0f, 100.0f)]
    public float Mutability = 0.05f;

    [Header("Floating-point type genes")]
    [Space(10)]

    [Header("MASTER GENES")]
    public List<FGen> MasterFloatGenes;

    [Header("Integer type genes")]
    public List<IGen> MasterIntegerGenes;

    [Header("Boolean type genes")]
    public List<BGen> MasterBooleanGenes;
}
