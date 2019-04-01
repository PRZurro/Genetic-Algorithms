using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    int m_ID;
    float m_score;

    Genome m_genome;

    Transform m_leftWheel;
    Rigidbody2D m_leftWheelRB2D;
    WheelJoint2D m_leftWheelJoint;

    Transform m_rightWheel;
    Rigidbody2D m_rightWheelRB2D;
    WheelJoint2D m_rightWheelJoint;

    Transform   m_chasis;
    Transform   m_swingarm;

    Transform m_driver;
    Transform m_driverHead;
    int m_timesHeadCollided;
    float m_startHeadPositionX;

    public static float HeadCollisionPenalization;

    //float gasConsumption; // Per second, must be multiplied by the mass of each component
    //float gasCapacity;

    void Start()
    {
        m_score = 0.0f;
        m_ID = 0;
        m_startHeadPositionX = 0.0f;
        m_timesHeadCollided = 0;
    }

    // Update is called once per frame
    void Update()
    {
        m_score = m_driverHead.position.x -  m_startHeadPositionX - (HeadCollisionPenalization * m_timesHeadCollided);
    }

    public void SetGenome(Genome motoGenome)
    {
        m_genome = motoGenome;
    }

    public int ID()
    {
        return m_ID;
    }

    public float score()
    {
        return m_score;
    }


    public Genome genome()
    {
        return m_genome;
    }

    public void Initialize(Genome motoGenome, Transform leftWheel, Transform rightWheel, Transform driver, Transform driverHead)
    {
        SetGenome(motoGenome);

        m_leftWheel = leftWheel;
        m_leftWheelRB2D = m_leftWheel.GetComponent<Rigidbody2D>();
        m_leftWheelJoint = m_leftWheel.GetComponent<WheelJoint2D>();

        m_rightWheel = rightWheel;
        m_rightWheelRB2D = m_leftWheel.GetComponent<Rigidbody2D>();
        m_rightWheelJoint = m_rightWheel.GetComponent<WheelJoint2D>();

        m_driver = driver;
        m_driverHead = driverHead;
    }

    public float GetCurrentHeadPositionX()
    {
        return m_driverHead.position.x;
    }

    public void SetID(int ID)
    {
        m_ID = ID;
    }

    public void StopMotors()
    {
        m_leftWheelJoint.useMotor = false;
        m_rightWheelJoint.useMotor = false;
    }

    public void HeadCollided()
    {
        m_timesHeadCollided++;
    }
}
