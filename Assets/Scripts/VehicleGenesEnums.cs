using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MotorcycleFGenes : byte
{
    WHEEL_FORCE = 0,
    WHEEL_DAMPING_RATIO = 1,
    WHEEL_FREQUENCY = 2,
    WHEEL_SIZE = 3,
    CHASIS_MASS = 4,
    CHASIS_SIZE = 5,
    SWINGARM_ANGLE_MIN = 6,
    SWINGARM_ANGLE_MAX = 7
}

enum MotorcycleIGenes : byte
{
    CHASIS_TYPE = 0
}

enum MotorcycleBGenes : byte
{
    WHEEL_IS_MOTOR = 0
}