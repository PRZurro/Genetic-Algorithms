using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    int m_ID;
    float m_score;

    Genome m_genome;

    WheelJoint2D m_leftWheelJoint;
    WheelJoint2D m_rightWheelJoint;
    GameObject   m_chasis;
    GameObject   m_swingarm;

    GameObject m_driver;

    //float gasConsumption; // Per second, must be multiplied by the mass of each component
    //float gasCapacity;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMotoGenome(Genome motoGenome)
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

    public void Initialize(Genome motoGenome, WheelJoint2D leftWheelJoint, WheelJoint2D rightWheelJoint)
    {
        SetMotoGenome(motoGenome);

        m_leftWheelJoint = leftWheelJoint;
        m_rightWheelJoint = rightWheelJoint; 
    }

    public void StopMotors()
    {
        m_leftWheelJoint.useMotor = false;
        m_rightWheelJoint.useMotor = false;
    }
}
