using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGenesSettings", menuName = "Genes Settings", order = 51)]
public class GenesSettings : ScriptableObject
{
    [Range(0.0f, 100.0f)]
    public float mutability;

    [Header("Floating-point type genes")]
    [Space(10)]

    [Header("MASTER GENES")]
    public List<FGen> masterFloatGenes;

    [Header("Integer type genes")]
    public List<IGen> masterIntegerGenes;

    [Header("Boolean type genes")]
    public List<BGen> masterBooleanGenes;
}
