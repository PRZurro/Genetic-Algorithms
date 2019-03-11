using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Dictionary<byte, Motorcycle> motorcycles;

    Camera activeCamera;

    [SerializeField]
    Limitations geneLimitations;

    // Start is called before the first frame update
    void Start()
    {
        activeCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
