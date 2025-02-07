using System.Collections;
using UnityEngine;

public class Whee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(transform.position.x - 222, transform.position.y - 11, transform.position.z - 159);
        Debug.Log(transform.position);
        StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fly()
    {
        int count = 0;
        while (count < 10000)
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
            if (count == 0 || count == 100)
            {
                Debug.Log(transform.position);
            }
            count++;
        }
        Destroy(gameObject);
    }
}
