using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New limitations", menuName = "Limitations Data", order = 51)]
public class Limitations : ScriptableObject
{
    [Header("Floating-point type genes")]
    [Space(10)]

    [Header("MASTER GENES")]
    public List<FGen> m_masterFloatGenes;

    [Header("Integer type genes")]
    public List<IGen> m_masterIntegerGenes;

    [Header("Boolean type genes")]
    public List<BGen> m_masterBooleanGenes;

    [Space(10)]
    [Header("OTHER")]
    [Range(300, 20000)]
    public float m_finishLinePosX = 300;

    [Tooltip("Must be an even number greater than zero")]
    [Range(100,1000)]
    public int nMotorcycles = 100;

    [Range(0.0f, 100.0f)]
    public float mutabilityProbability;
}
