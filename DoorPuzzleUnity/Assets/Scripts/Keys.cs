using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public enum KeyType { KeyOne, KeyTwo, KeyThree }
    public KeyType keyType;

    public float rotationSpeed = 25.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);

            if (keyType == KeyType.KeyOne)
            {
                col.GetComponent<PlayerController>().HasKeyOne();
            }

            if (keyType == KeyType.KeyTwo)
            {
                col.GetComponent<PlayerController>().HasKeyTwo();
            }

            if (keyType == KeyType.KeyThree)
            {
                col.GetComponent<PlayerController>().HasKeyThree();
            }
        }
    }
}
