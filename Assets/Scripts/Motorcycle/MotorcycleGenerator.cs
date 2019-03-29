using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleGenerator : MonoBehaviour
{
    static MotorcycleGenerator m_instance;

    MotorcyclePrefabs m_motorcyclePrefabs;

    public static MotorcycleGenerator Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject();
                m_instance = go.AddComponent<MotorcycleGenerator>();
                go.name = "Motorcycle Generator";
            }
            return m_instance;
        }
    }

    public void SetMotorcyclePrefabs(MotorcyclePrefabs motorcyclePrefabs)
    {
        m_motorcyclePrefabs = motorcyclePrefabs;
    }

    public GameObject CreateMotorcycle(MotoGenome genome)
    {
        return ConstructMotorcycle(m_motorcyclePrefabs.BasePrefabs[genome.GetGen(MotoIGenID.CHASIS_TYPE).Value()], genome);
    }

    public GameObject CreateMotorcycle(Motorcycle vehicleParent1, Motorcycle vehicleParent2)
    {
        return CreateMotorcycle(new MotoGenome(vehicleParent1.motoGenome(), vehicleParent2.motoGenome()));
    }

    GameObject CreateMotorcycle(List<FGen> fGenes, List<IGen> iGenes, List<BGen> bGenes)
    {
        return CreateMotorcycle(new MotoGenome(fGenes, iGenes, bGenes));
    }

    GameObject ConstructMotorcycle(GameObject basePrefab, MotoGenome genome)
    {
        // Create the motorcycle parts 

        GameObject motorcycle = Instantiate(basePrefab); // For now only contains the anchors and the chasis

        if(genome.ExistsGen(MotoFGenID.CHASIS_SCALE))
            motorcycle.transform.localScale *= genome.GetGen(MotoFGenID.CHASIS_SCALE).Value();

        GameObject leftWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);
        GameObject rightWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);

       

        if (genome.ExistsGen(MotoFGenID.RIGHT_WHEEL_SCALE))
            rightWheel.transform.localScale *= genome.GetGen(MotoFGenID.RIGHT_WHEEL_SCALE).Value();

        if (genome.ExistsGen(MotoFGenID.LEFT_WHEEL_SCALE))
            leftWheel.transform.localScale *= genome.GetGen(MotoFGenID.LEFT_WHEEL_SCALE).Value();

        if (genome.ExistsGen(MotoFGenID.RIGHT_WHEEL_SCALE))
            rightWheel.transform.localScale *= genome.GetGen(MotoFGenID.RIGHT_WHEEL_SCALE).Value();

        return motorcycle;
    }


    void TuneWheel(GameObject wheel, MotoGenome genome, MotoFGenID scaleID, MotoBGenID isMotorID, MotoFGenID speedID)
    {
        SpriteRenderer spriteRenderer = wheel.GetComponent<SpriteRenderer>();
        Transform wheelTr = wheel.transform;
        WheelJoint2D wheelJoint = wheel.GetComponent<WheelJoint2D>();

        if (genome.ExistsGen(scaleID))
            wheelTr.localScale *= genome.GetGen(scaleID).Value();

        if (genome.ExistsGen(dampingRatioID))
            wheelJoint.= genome.GetGen(scaleID).Value();


        if (genome.ExistsGen(isMotorID))
        {
            if (genome.GetGen(isMotorID).Value()) // If is motor
            {
                spriteRenderer.color = Color.green;

                if(genome.ExistsGen(speedID))
                {
                    JointMotor2D wheelJointMotor = wheelJoint.motor;
                    wheelJointMotor.motorSpeed = genome.GetGen(speedID).Value();
                }
            }
            else // If is not motor
            {
                spriteRenderer.color = Color.red;
            }
        }
    }
}
