using System;
using System.Security.Claims;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public GameObject catapult;
    public Transform structure1;
    public Transform structure2;
    public Transform structure3;
    private Vector3 structure1pos;
    private Vector3 structure2pos;
    private Vector3 structure3pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        structure1pos = structure1.position;
        structure2pos = structure2.position;
        structure3pos = structure3.position;
        Instantiate(catapult, new Vector3(structure1pos.x, structure1pos.y + 12, structure1pos.z), Quaternion.identity, transform);
        Instantiate(catapult, new Vector3(structure2pos.x, structure2pos.y + 8, structure2pos.z), Quaternion.identity, transform);
        Instantiate(catapult, new Vector3(structure3pos.x, structure3pos.y + 12, structure3pos.z), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
