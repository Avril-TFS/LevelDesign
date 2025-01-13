using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject[] torches;
    public Color[] torchColours = { Color.red, Color.green, Color.blue, Color.yellow };
    public Color[] correctColours;
    public GameObject keyPrefab;
    public Transform keySpawnPoint;
    public int[] currentColorIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentColorIndex = new int[torches.Length];

        for (int i = 0; i < torches.Length; i++)
        {
            SetTorchColour(torches[i], torchColours[0]);
            currentColorIndex[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                for (int i = 0; i < torches.Length; i++)
                {
                    if (hit.collider.gameObject == torches[i])
                    {
                        CycleTorchColour(i);
                        CheckForCorrectCombination();
                        break;
                    }
                }
            }
        }
    }
    void CycleTorchColour(int torchIndex)
    {
        currentColorIndex[torchIndex] = (currentColorIndex[torchIndex] + 1) % torchColours.Length;
        SetTorchColour(torches[torchIndex], torchColours[currentColorIndex[torchIndex]]);
    }
    void SetTorchColour(GameObject torch, Color color)
    {
        Renderer renderer = torch.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }
    void CheckForCorrectCombination()
    {
        for (int i = 0; i < torches.Length; i++)
        {
            if (torchColours[currentColorIndex[i]] != correctColours[i])
            {
                return;
            }
        }

        if (keyPrefab != null && keySpawnPoint != null)
        {
            Instantiate(keyPrefab, keySpawnPoint.position, keySpawnPoint.rotation);
        }
    }
}
