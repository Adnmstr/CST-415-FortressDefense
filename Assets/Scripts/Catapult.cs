using System;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject catapult;
    public int waitTime = 5;
    public Transform structure1;
    public Transform structure2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(catapult, new Vector3(structure1.position.x, structure1.position.y + 100, structure1.position.z), Quaternion.identity, transform);
        Instantiate(catapult, new Vector3(structure2.position.x, structure2.position.y + 100, structure2.position.z), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
