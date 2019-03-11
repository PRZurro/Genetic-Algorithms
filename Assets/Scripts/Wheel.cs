using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    WheelCollider Collider;
    float Mass { get; set; }
   
    Wheel(float posX, float posY, float radio)
    {
        transform.position = new Vector3(posX, posY, .0f);
        SpriteRenderer sRenderer = new SpriteRenderer();
        sRenderer.size = new Vector2(radio, radio);
    }

    Wheel(float posX, float posY, float radio, float motorTorque) : this(posX, posY, radio)
    {
        Collider.motorTorque = motorTorque;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
