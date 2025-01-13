using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (playerController.CheckHasKeyOne() && playerController.CheckHasKeyTwo() && playerController.CheckHasKeyThree())
            {
                Destroy(gameObject);
            }
        }
    }
}
