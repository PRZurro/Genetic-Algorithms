using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game Settings", order = 52)]
public class GameSettings : ScriptableObject
{
    [Range(300, 20000)]
    public float FinishLinePosX = 300;

    [Tooltip("Must be an even number greater than zero")]
    [Range(100, 1000)]
    public int NumberMotorcycles = 100;

}
