using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public GameObject _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null) _mainCamera = Camera.main.gameObject;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = _mainCamera.transform.position;

        cameraPosition.y = transform.position.y;

        transform.LookAt(cameraPosition);

        transform.Rotate(0f, 180f, 0f);
    }
}
