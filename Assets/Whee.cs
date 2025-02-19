using System.Collections;
using UnityEngine;

public class Whee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3((1.0f / 3.0f), (10.0f / 3.0f), (10.0f / 3.0f));
        transform.Rotate(0, 90, 30);
        StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fly()
    {
        while (transform.position.y > 0)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z - 1);
        }
        Destroy(gameObject);
    }
}