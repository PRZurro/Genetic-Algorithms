using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleGenerator : MonoBehaviour
{
    static MotorcycleGenerator m_instance;

    MotorcyclePrefabs m_motorcyclePrefabs;

    /// <summary>
    /// Return the instance
    /// </summary>
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

    /// <summary>
    /// Set the motorcycle prefabs for later creation of motorcycles
    /// </summary>
    /// <param name="motorcyclePrefabs"></param>
    public void SetMotorcyclePrefabs(MotorcyclePrefabs motorcyclePrefabs)
    {
        m_motorcyclePrefabs = motorcyclePrefabs;
    }

    /// <summary>
    /// Create a new Motorcycle in function of the genome received
    /// </summary>
    /// <param name="genome"></param>
    /// <returns></returns>
    public Motorcycle CreateMotorcycle(Genome genome)
    {
        return ConstructMotorcycle(genome);
    }

    /// <summary>
    /// Create a motorcycle by two parents
    /// </summary>
    /// <param name="vehicleParent1"></param>
    /// <param name="vehicleParent2"></param>
    /// <returns></returns>
    public Motorcycle CreateMotorcycle(Motorcycle vehicleParent1, Motorcycle vehicleParent2)
    {
        return CreateMotorcycle(new Genome(vehicleParent1.genome(), vehicleParent2.genome()));
    }

    /// <summary>
    /// Create a motorcycle in function of the separated lists of genes
    /// </summary>
    /// <param name="fGenes"></param>
    /// <param name="iGenes"></param>
    /// <param name="bGenes"></param>
    /// <returns></returns>
    public Motorcycle CreateMotorcycle(List<FGen> fGenes, List<IGen> iGenes, List<BGen> bGenes)
    {
        return CreateMotorcycle(new Genome(fGenes, iGenes, bGenes));
    }
    
    /// <summary>
    /// Construct a new motorcycle in function of the genome received
    /// </summary>
    /// <param name="genome"></param>
    /// <returns></returns>
    private Motorcycle ConstructMotorcycle(Genome genome)
    {
        // Create the motorcycle parts 
        // Save all transform that will be used for assemble later

        GameObject motorcycleBase = Instantiate(m_motorcyclePrefabs.BasePrefabs[genome.GetGen(IGenID.CHASIS_TYPE).Value()]); // For now only contains the anchors and the chasis
        TuneChasis(motorcycleBase, genome);

        GameObject swingarmParent = Instantiate(m_motorcyclePrefabs.SwingArmPrefabs[genome.GetGen(IGenID.SWINGARM_TYPE).Value()]);
        TuneSwingarm(swingarmParent, genome);

        GameObject leftWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);
        TuneWheel(leftWheel, genome, FGenID.LEFT_WHEEL_SCALE, BGenID.LEFT_WHEEL_IS_MOTOR, FGenID.LEFT_WHEEL_SPEED);

        GameObject rightWheel = Instantiate(m_motorcyclePrefabs.WheelPrefab);
        TuneWheel(rightWheel, genome, FGenID.RIGHT_WHEEL_SCALE, BGenID.RIGHT_WHEEL_IS_MOTOR, FGenID.RIGHT_WHEEL_SPEED);

        GameObject driver = Instantiate(m_motorcyclePrefabs.Driver);

        return AssembleMotorcycle(motorcycleBase.transform, swingarmParent.transform, leftWheel.transform, rightWheel.transform, driver.transform, genome);
    }

    /// <summary>
    /// Assemble all separated pieces into one object with connected joints
    /// </summary>
    /// <param name="motorcycleBase"></param>
    /// <param name="swingarmParent"></param>
    /// <param name="leftWheel"></param>
    /// <param name="rightWheel"></param>
    /// <param name="driver"></param>
    /// <param name="genome"></param>
    /// <returns></returns>
    private Motorcycle AssembleMotorcycle(Transform motorcycleBase, Transform swingarmParent, Transform leftWheel, Transform rightWheel, Transform driver, Genome genome)
    {
        // Save all necessary transforms for later
        Transform chasis = motorcycleBase.GetChild(0);
        Transform chasisAnchors = chasis.GetChild(0);
        Transform chasisRightWheelAnchor = chasisAnchors.Find("Front Wheel");
        Transform chasisSwingarmAnchor = chasisAnchors.Find("Swingarm");
        Transform chasisDriverAnchor = chasisAnchors.Find("Driver");

        Transform swingarm = swingarmParent.GetChild(0);
        Transform swingarmLeftWheelAnchor = swingarm.GetChild(0);

        Transform driverBody = driver.GetChild(0);

        // Set the transform and the hinge joint values of the swingarm 
        swingarmParent.parent = chasisSwingarmAnchor;
        swingarmParent.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        HingeJoint2D swingarmHingeJoint = swingarm.GetComponent<HingeJoint2D>();
        swingarmHingeJoint.connectedBody = chasis.GetComponent<Rigidbody2D>();
        swingarmHingeJoint.connectedAnchor = new Vector2(chasisSwingarmAnchor.localPosition.x, chasisSwingarmAnchor.localPosition.y);

        // Set the transform and the wheel joint values of the left wheel
        leftWheel.parent = swingarm;
        leftWheel.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        WheelJoint2D leftWheelJoint = leftWheel.GetComponent<WheelJoint2D>();
        leftWheelJoint.connectedBody = swingarm.GetComponent<Rigidbody2D>();
        leftWheelJoint.connectedAnchor = new Vector2(swingarmLeftWheelAnchor.localPosition.x, swingarmLeftWheelAnchor.localPosition.y);

        // Set the transform and the wheel joint values of the right wheel

        rightWheel.parent = chasis;
        rightWheel.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

        WheelJoint2D rightWheelJoint = rightWheel.GetComponent<WheelJoint2D>();
        rightWheelJoint.connectedBody = chasis.GetComponent<Rigidbody2D>();
        rightWheelJoint.connectedAnchor = new Vector2(chasisRightWheelAnchor.localPosition.x, chasisRightWheelAnchor.localPosition.y);

        // Set the driver's transform and hinge joint values
        driver.parent = chasisDriverAnchor;
        driver.localPosition = new Vector3(0.0f, 0.0f, 0.0f); ;

        driverBody.GetComponent<HingeJoint2D>().connectedBody = chasis.GetComponent<Rigidbody2D>();

        // Set parents of each part to the base object
        swingarm.parent = motorcycleBase;
        leftWheel.parent = motorcycleBase;
        rightWheel.parent = motorcycleBase;
        driver.parent = motorcycleBase;

        // Destroy all anchors

        Destroy(swingarmParent.gameObject);
        Destroy(swingarmLeftWheelAnchor.gameObject);
        Destroy(chasisRightWheelAnchor.gameObject);
        Destroy(chasisDriverAnchor.gameObject);
        Destroy(chasisAnchors.gameObject);

        // To finish, add a motorcycle controller component  to the parent object
        Motorcycle motorcycleComp = motorcycleBase.gameObject.AddComponent<Motorcycle>();
        Transform head = driver.Find("head").transform;
        motorcycleComp.Initialize(genome, leftWheel, rightWheel, driver, head);

        // Set the head collision detector (needs a delegate)
        CollisionDetector collisionDetector = head.gameObject.AddComponent<CollisionDetector>();
        collisionDetector.SetCollisionCommunication(motorcycleComp.HeadCollided);

        return motorcycleComp;
    }

    /// <summary>
    /// Tweak the chasis in function of the genome received
    /// </summary>
    /// <param name="chasis"></param>
    /// <param name="genome"></param>
    private void TuneChasis(GameObject chasis, Genome genome)
    {
        if (genome.ExistsGen(FGenID.CHASIS_SCALE))
            chasis.transform.localScale *= genome.GetGen(FGenID.CHASIS_SCALE).Value();
    }

    /// <summary>
    /// Tweak the swingarm in function of the genome received
    /// </summary>
    /// <param name="swingarm"></param>
    /// <param name="genome"></param>
    private void TuneSwingarm(GameObject swingarm, Genome genome)
    {
        if(genome.ExistsGen(FGenID.SWINGARM_ANGLE_MAX))
        {
            JointAngleLimits2D newLimits = new JointAngleLimits2D();
            newLimits.min = 0.0f;
            newLimits.max = genome.GetGen(FGenID.SWINGARM_ANGLE_MAX).Value();
            swingarm.transform.GetChild(0).GetComponent<HingeJoint2D>().limits = newLimits;
        }
        if(genome.ExistsGen(FGenID.SWINGARM_SCALE))
        {
            swingarm.transform.localScale *= genome.GetGen(FGenID.SWINGARM_SCALE).Value();
        }
    }

    /// <summary>
    /// Tweak the wheel values in function of the genome received
    /// </summary>
    /// <param name="wheel"></param>
    /// <param name="genome"></param>
    /// <param name="scaleID"></param>
    /// <param name="isMotorID"></param>
    /// <param name="speedID"></param>
    private void TuneWheel(GameObject wheel, Genome genome, FGenID scaleID, BGenID isMotorID, FGenID speedID)
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
                wheelJoint.useMotor = false;
            }
        }
    }
}
