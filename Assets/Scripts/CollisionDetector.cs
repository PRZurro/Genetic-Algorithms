﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public delegate void CollisionCommunication();

    CollisionCommunication m_communication;

    /// <summary>
    /// On collision2d enter...
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        m_communication();
    }

    /// <summary>
    /// Set the delegate's function
    /// </summary>
    /// <param name="communication"></param>
    public void SetCollisionCommunication(CollisionCommunication communication)
    {
        m_communication = communication;
    }
}
