using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MotoFGenID : byte
{
    LWHEEL_FORCE = 0,
    RWHEEL_FORCE = 1,
    LWHEEL_DAMPING_RATIO = 2,
    RWHEEL_DAMPING_RATIO = 3,
    LWHEEL_FREQUENCY = 4,
    RWHEEL_FREQUENCY = 5,
    LWHEEL_SIZE = 6,
    RWHEEL_SIZE = 7,
    CHASIS_MASS = 8,
    CHASIS_SIZE = 9,
    SWINGARM_ANGLE_MIN = 10,
    SWINGARM_ANGLE_MAX = 11,
}

enum MotoIGenID : byte
{
    CHASIS_TYPE = 0
}

enum MotoBGenID : byte
{
    LWHEEL_IS_MOTOR = 0,
    RWHEEL_IS_MOTOR = 1,
}