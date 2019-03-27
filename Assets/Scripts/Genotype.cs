using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genotype : MonoBehaviour
{
    struct Cromosome
    {
        enum FloatGenes : byte
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

        enum IntGenes : byte
        {
            CHASIS_TYPE = 0
        }

        enum BoolGenes : byte
        {
            WHEEL_IS_MOTOR = 0
        }


        List<Gen<float>> fGenes;
        List<Gen<bool>> bGenes;
        List<Gen<int>> intGenes;

        public class Gen <T>
        {
            T Value { get; set; }
            static T MinValue { get; set; }
            static T MaxValue { get; set; }
        }

        struct GenWheel
        {
            public float force;
            public float dampingRatio;
            public float frequency;
            public float scale; // Size
            public bool isMotor; // dudoso
        }
        struct GenChasis
        {
            public float mass;
            public float scale; // Size
            public float type;
        }

        struct GenSwingArm
        {
            public float maxAngle;
            public float scaleX;
        }
    }

   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    ////////////////////////////////////////RECOMBINATION////////////////////////////////

    Cromosome Recombine(Cromosome cromosome1, Cromosome cromosome2, float mutationFactor)
    {
        return new Cromosome
        (
        );
    }

    bool ObtainGen(bool gen1, bool gen2, float probabilityGen1, float mutationFactor)
    {
        if (Random.Range(0.0f, 100f) < mutationFactor)
        {
            return Mutate();
        }
        else
        {
            return ChooseGen(gen1, gen2, probabilityGen1);
        }
    }

    float ObtainGen(float gen1, float gen2, float probabilityGen1, float mutationFactor, float minGenValue, float maxGenValue)
    {
        if (Random.Range(0.0f, 100f) < mutationFactor)
        {
            return Mutate(minGenValue, maxGenValue);
        }
        else
        {
            return ChooseGen(gen1, gen2, probabilityGen1);
        }
    }

    bool ChooseGen(bool gen1, bool gen2, float probabilityGen1)
    {
        if (Random.Range(0.0f, 100.0f) < probabilityGen1)
        {
            return gen1;
        }
        else
        {
            return gen2;
        }
    }

    float ChooseGen(float gen1, float gen2, float probabilityGen1)
    {
        if (Random.Range(0.0f, 100.0f) < probabilityGen1)
        {
            return gen1;
        }
        else
        {
            return gen2;
        }
    }

    float Mutate(float minValue, float maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    bool Mutate()
    {
        return (Random.value > 0.5f);
    }
}
