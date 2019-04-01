using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of the genes of type float
/// </summary>
public enum FGenID : byte
{
    LEFT_WHEEL_SPEED = 0,
    //LEFT_WHEEL_DAMPING_RATIO = 1,
    //LEFT_WHEEL_FREQUENCY = 2,
    LEFT_WHEEL_SCALE = 3,
    RIGHT_WHEEL_SPEED = 4,
    //RIGHT_WHEEL_DAMPING_RATIO = 5,
    //RIGHT_WHEEL_FREQUENCY = 6,
    RIGHT_WHEEL_SCALE = 7,
    CHASIS_SCALE = 8,
    // CHASIS_MASS = 8,
    //SWINGARM_ANGLE_MIN = 9,
    SWINGARM_SCALE = 10,
    SWINGARM_ANGLE_MAX = 11
}

/// <summary>
/// Enumeration of the genes of type integer
/// </summary>
public enum IGenID : byte
{
    CHASIS_TYPE = 0,
    SWINGARM_TYPE = 1
}

/// <summary>
/// Enumeration of the genes of type boolean
/// </summary>
public enum BGenID : byte
{
    LEFT_WHEEL_IS_MOTOR = 0,
    RIGHT_WHEEL_IS_MOTOR = 1
}
