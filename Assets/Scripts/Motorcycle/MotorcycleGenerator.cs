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
        return ConstructMotorcycle(genome);
    }

    public GameObject CreateMotorcycle(Motorcycle vehicleParent1, Motorcycle vehicleParent2)
    {
        return CreateMotorcycle(new MotoGenome(vehicleParent1.motoGenome(), vehicleParent2.motoGenome()));
    }

    public GameObject CreateMotorcycle(List<FGen> fGenes, List<IGen> iGenes, List<BGen> bGenes)
    {
        return CreateMotorcycle(new MotoGenome(fGenes, iGenes, bGenes));
    }

    private GameObject ConstructMotorcycle(MotoGenome genome)
    {
        // Create the motorcycle parts 

        GameObject motorcycleBasePrefab = Instantiate(m_motorcyclePrefabs.BasePrefabs[genome.GetGen(MotoIGenID.CHASIS_TYPE).Value()]); // For now only contains the anchors and the chasis
        GameObject swingArm = Instantiate(m_motorcyclePrefabs.SwingArmPrefabs[genome.GetGen(MotoIGenID.SWINGARM_TYPE).Value()]);
        GameObject leftWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);
        GameObject rightWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);

        TuneChasis(motorcycleBasePrefab, genome);

        TuneSwingarm(swingArm, genome);

        TuneWheel(leftWheel, genome, MotoFGenID.LEFT_WHEEL_SCALE, MotoBGenID.LEFT_WHEEL_IS_MOTOR, MotoFGenID.LEFT_WHEEL_SPEED);
        TuneWheel(rightWheel, genome, MotoFGenID.RIGHT_WHEEL_SCALE, MotoBGenID.RIGHT_WHEEL_IS_MOTOR, MotoFGenID.RIGHT_WHEEL_SPEED);

        return AssembleMotorcycle(motorcycleBasePrefab, swingArm, leftWheel, rightWheel);
    }

    private GameObject AssembleMotorcycle(GameObject motorcycleBasePrefab, GameObject swingarmParent, GameObject leftWheel, GameObject rightWheel)
    {
        // Save all transform that will be used for assemble later
        Transform chasis = motorcycleBasePrefab.transform.GetChild(0);
        Transform chasisAnchors = chasis.transform.GetChild(0);

        Transform chasisRightWheelAnchor = chasisAnchors.transform.Find("Front Wheel");
        Transform chasisSwingarmAnchor = chasisAnchors.transform.Find("Swingarm");
        Transform chasisDriverAnchor = chasisAnchors.transform.Find("Driver");

        Transform swingarm = swingarmParent.transform.GetChild(0);
        Transform swingarmLeftWheelAnchor = swingarm.GetChild(0);

        chasis.GetComponent<Rigidbody2D>().simulated = false;
        swingarm.GetComponent<Rigidbody2D>().simulated = false;
        leftWheel.GetComponent<Rigidbody2D>().simulated = false;
        rightWheel.GetComponent<Rigidbody2D>().simulated = false;

        // Positionate correctly all parts

        swingarmParent.transform.position = chasisSwingarmAnchor.position;
        swingarm.parent = motorcycleBasePrefab.transform;

        HingeJoint2D swingarmHingeJoint = swingarm.GetComponent<HingeJoint2D>();
        swingarmHingeJoint.connectedBody = chasis.GetComponent<Rigidbody2D>();
        swingarmHingeJoint.connectedAnchor = new Vector2(chasisSwingarmAnchor.localPosition.x, chasisSwingarmAnchor.localPosition.y);

        leftWheel.transform.position = swingarmLeftWheelAnchor.transform.position;
        leftWheel.transform.parent = motorcycleBasePrefab.transform;
        rightWheel.transform.position = chasisRightWheelAnchor.transform.position;
        rightWheel.transform.parent = motorcycleBasePrefab.transform;

        WheelJoint2D leftWheelJoint = leftWheel.GetComponent<WheelJoint2D>();
        leftWheelJoint.connectedBody = swingarm.GetComponent<Rigidbody2D>();
        leftWheelJoint.connectedAnchor = new Vector2(swingarmLeftWheelAnchor.localPosition.x, swingarmLeftWheelAnchor.localPosition.y);

        WheelJoint2D rightWheelJoint = rightWheel.GetComponent<WheelJoint2D>();
        rightWheelJoint.connectedBody = chasis.GetComponent<Rigidbody2D>();
        rightWheelJoint.connectedAnchor = new Vector2(chasisRightWheelAnchor.localPosition.x, chasisRightWheelAnchor.localPosition.y);

        //Add a driver  

        Destroy(swingarmParent.gameObject);

        Destroy(swingarmLeftWheelAnchor.gameObject);
        Destroy(chasisRightWheelAnchor.gameObject);

        Destroy(chasisAnchors.gameObject);

        return motorcycleBasePrefab;
    }

    private void TuneChasis(GameObject chasis, MotoGenome genome)
    {
        if (genome.ExistsGen(MotoFGenID.CHASIS_SCALE))
            chasis.transform.localScale *= genome.GetGen(MotoFGenID.CHASIS_SCALE).Value();
    }

    private void TuneSwingarm(GameObject swingarm, MotoGenome genome)
    {
        if(genome.ExistsGen(MotoFGenID.SWINGARM_ANGLE_MAX))
        {
            JointAngleLimits2D newLimits = new JointAngleLimits2D();
            newLimits.min = 0.0f;
            newLimits.max = genome.GetGen(MotoFGenID.SWINGARM_ANGLE_MAX).Value();
            swingarm.transform.GetChild(0).GetComponent<HingeJoint2D>().limits = newLimits;
        }
    }

    private void TuneWheel(GameObject wheel, MotoGenome genome, MotoFGenID scaleID, MotoBGenID isMotorID, MotoFGenID speedID)
    {
        SpriteRenderer spriteRenderer = wheel.GetComponent<SpriteRenderer>();
        Transform wheelTr = wheel.transform;
        WheelJoint2D wheelJoint = wheel.GetComponent<WheelJoint2D>();

        if (genome.ExistsGen(scaleID))
            wheelTr.localScale *= genome.GetGen(scaleID).Value();

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
