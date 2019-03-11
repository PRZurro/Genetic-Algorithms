using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New limitations", menuName = "Limitations Data", order = 51)]
public class Limitations : ScriptableObject
{
    #region Genetic Algorithm

    [SerializeField]
    [Range(0.0f, 100.0f)]
    double mutability = 0.05;

    #endregion

    #region Motorcycles

    [Header("Motorcycles")]

    [SerializeField]
    [Range(2, 4)]
    byte wheelsNumber = 2;

    #endregion  

    #region Wheels

    [Header("Wheels")]

    [SerializeField]
    [Range(0.0f, 100f)]
    Vector2 wheelPosX; // Wheel 

    [SerializeField]
    [Range(0.0f, 100f)]
    Vector2 wheelPosY;

    [SerializeField]
    [Range(0.0f, 100f)]
    Vector2 wheelRadio;

    [SerializeField]
    [Range(0.0f, 100f)]
    Vector2 wheelSuspension;

    [SerializeField]
    [Range(0.0f, 100f)]
    Vector2 wheelMass;
    #endregion

    #region Getters

    public double Mutability { get => mutability; }

    public byte WheelsNumber { get => wheelsNumber; }

    public Vector2 WheelPosX { get => wheelPosX; }
    public Vector2 WheelPosY { get => wheelPosY; }
    public Vector2 WheelRadio { get => wheelRadio; }
    public Vector2 WheelSuspension { get => wheelSuspension; }
    public Vector2 WheelMass { get => wheelMass; }


    #endregion
}
