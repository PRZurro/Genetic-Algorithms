using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    public delegate void CollisionCommunication();

    CollisionCommunication m_communication;

    void OnCollisionEnter2D(Collision2D collision)
    {
        m_communication();
    }

    public void SetCollisionCommunication(CollisionCommunication communication)
    {
        m_communication = communication;
    }

}
