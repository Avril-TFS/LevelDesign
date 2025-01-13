using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    private Rigidbody rgbd;
    public Camera playerCamera;

    public bool hasKeyOne = false;
    public bool hasKeyTwo = false;
    public bool hasKeyThree = false;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.forward * verticalMovement) + (transform.right * horizontalMovement);

        Vector3 movement = moveDirection.normalized * moveSpeed;
        rgbd.velocity = new Vector3(movement.x, rgbd.velocity.y, movement.z);

    }

    public void HasKeyOne()
    {
        hasKeyOne = true;
    }
    public void HasKeyTwo()
    {
        hasKeyTwo = true;
    }
    public void HasKeyThree()
    {
        hasKeyThree = true;
    }
    public bool CheckHasKeyOne()
    {
        return hasKeyOne;
    }

    public bool CheckHasKeyTwo()
    {
        return hasKeyTwo;
    }

    public bool CheckHasKeyThree()
    {
        return hasKeyThree;
    }
}
