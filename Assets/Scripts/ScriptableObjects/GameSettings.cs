using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game Settings", order = 52)]
public class GameSettings : ScriptableObject
{
    //[Range(300, 20000)]
    //public float FinishLinePosX = 300; // We have an infinite 2D terrain, so be it. No finishLine

    [Tooltip("Must be an even number greater than zero")]
    [Range(2, 50)]
    public int m_numberMotorcycles = 2;

    [Range(5.0f, 100.0f)]
    public float m_generationTime;

    [Range(0.0f, 100.0f)]
    public float m_cameraLeftOffset;

    public float m_headCollisionPenalization;
}
