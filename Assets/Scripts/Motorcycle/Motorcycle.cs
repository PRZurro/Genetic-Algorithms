using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motorcycle : MonoBehaviour
{
    int m_ID;
    MotoGenome m_motoGenome;

    WheelJoint2D m_leftWheelJoint;
    WheelJoint2D m_rightWheelJoint;

    //float gasConsumption; // Per second, must be multiplied by the mass of each component
    //float gasCapacity;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMotoGenome(MotoGenome motoGenome)
    {
        m_motoGenome = motoGenome;
    }

    public int ID()
    {
        return m_ID;
    }

    public MotoGenome motoGenome()
    {
        return m_motoGenome;
    }

    public void Initialize(MotoGenome motoGenome, WheelJoint2D leftWheelJoint, WheelJoint2D rightWheelJoint)
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
