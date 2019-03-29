using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MotoFGenID : byte
{
    LEFT_WHEEL_FORCE = 0,
    LEFT_WHEEL_DAMPING_RATIO = 1,
    LEFT_WHEEL_FREQUENCY = 2,
    LEFT_WHEEL_SIZE = 3,
    RIGHT_WHEEL_FORCE = 4,
    RIGHT_WHEEL_DAMPING_RATIO = 5,
    RIGHT_WHEEL_FREQUENCY = 6,
    RIGHT_WHEEL_SIZE = 7,
    CHASIS_MASS = 8,
    CHASIS_SIZE = 9,
    SWINGARM_ANGLE_MIN = 10,
    SWINGARM_ANGLE_MAX = 11,
}

public enum MotoIGenID : byte
{
    CHASIS_TYPE = 0
}

public enum MotoBGenID : byte
{
    LEFT_WHEEL_IS_MOTOR = 0,
    RIGHT_WHEEL_IS_MOTOR = 1,
}
