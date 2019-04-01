using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable objects to store some game settings
/// </summary>
[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game Settings", order = 52)]
public class GameSettings : ScriptableObject
{
    //[Range(300, 20000)]
    //public float FinishLinePosX = 300; // We have an infinite 2D terrain, so be it. No finishLine

    [Tooltip("Must be an even number greater than zero")]
    [Range(2, 50)]
    public int NumberMotorcycles = 2;

    [Range(5.0f, 100.0f)]
    public float GenerationTime;

    [Range(0.0f, 100.0f)]
    public float CameraLeftOffset;

    public float HeadCollisionPenalization;
}

