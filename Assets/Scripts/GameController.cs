using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Dictionary<byte, Motorcycle> motorcycles;

    Camera activeCamera;

    [SerializeField]
    Limitations geneLimitations;

    [SerializeField]
    List<Object> motoPrefabs;

    void Start()
    {
        activeCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
