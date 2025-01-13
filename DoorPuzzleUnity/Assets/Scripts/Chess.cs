using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    public Transform[] pieces;
    public Transform[] originalPositions;       // I couldnt actually get piece resetting to work
    public GameObject keyPrefab;
    public Transform keySpawnPoint;

    private Transform selectedPiece;
    private Vector3 offset;
    private Plane movementPlane;
    public Transform player;
    public float maxDistance = 2f;

    private void Update()
    {
        HandlePieceDragging();
    }

    private void HandlePieceDragging()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("WhitePiece"))
                {
                    selectedPiece = hit.transform;
                    // offset = selectedPiece.position - hit.point;
                    movementPlane = new Plane(Vector3.up, selectedPiece.position);


                    if (movementPlane.Raycast(ray, out float enter))
                    {
                        offset = selectedPiece.position - ray.GetPoint(enter);
                    }
                }
            }
        }
        else if (Input.GetMouseButton(0) && selectedPiece != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            /* if (Physics.Raycast(ray, out RaycastHit hit))
             {
                 selectedPiece.position = hit.point + offset;
             }*/
            if (movementPlane.Raycast(ray, out float enter))
            {
                Vector3 piecePosition = ray.GetPoint(enter) + offset;

                if (Vector3.Distance(player.position, piecePosition) > maxDistance)
                {
                    Vector3 direction = (piecePosition - player.position).normalized;
                    piecePosition = player.position + direction * maxDistance;
                }
                selectedPiece.position = piecePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0) && selectedPiece != null)
        {
            CheckPlacement(selectedPiece);
            selectedPiece = null;
        }
    }

    private void CheckPlacement(Transform piece)
    {
        Collider[] colliders = Physics.OverlapSphere(piece.position, 0.5f);
        foreach (var col in colliders)
        {
            if (col.CompareTag("CheckMate"))
            {
                CheckMate check = col.GetComponent<CheckMate>();
                if (check != null && check.correctPieceTag == piece.tag)
                {
                    PieceType pieceType = piece.GetComponent<PieceType>();
                    if (pieceType != null && pieceType.isQueen)
                    {
                        Debug.Log("Checkmate!");
                        SpawnKey();
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }


        ResetBoard();
    }

    private void SpawnKey()
    {
        if (keyPrefab != null && keySpawnPoint != null)
        {
            Instantiate(keyPrefab, keySpawnPoint.position, Quaternion.identity);
        }
    }

    private void ResetBoard()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            pieces[i].position = originalPositions[i].position;
        }
    }
}
