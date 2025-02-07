using System.Collections;
using UnityEngine;

public class Launch : MonoBehaviour
{
    public GameObject projectile;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.Rotate(0, -90, -30);
        StartCoroutine(Fire());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fire()
    {
        while (1 < 2)
        {
            yield return new WaitForSeconds(5);
            Instantiate(projectile, transform.position, Quaternion.identity, transform);
        }
    }
}
