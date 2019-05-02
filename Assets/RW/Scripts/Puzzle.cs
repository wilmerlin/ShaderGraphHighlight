using UnityEngine;

public class Puzzle : MonoBehaviour
{

    private GamePiece[] gamePieces;
    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;

    private void OnEnable()
    {
        // don't allow the user to edit this object accidentally
        gameObject.hideFlags = HideFlags.NotEditable;
    }

    // cache the original positions and rotation values of the GamePieces
    private void Awake()
    {
        SaveOriginalTransforms();
    }

    private void SaveOriginalTransforms()
    {
        gamePieces = GetComponentsInChildren<GamePiece>();
        originalPositions = new Vector3[gamePieces.Length];
        originalRotations = new Quaternion[gamePieces.Length];

        for (int i =0; i < gamePieces.Length; i++)
        {
            if (gamePieces[i] != null)
            {
                originalPositions[i] = gamePieces[i].transform.position;
                originalRotations[i] = gamePieces[i].transform.rotation;
            }
        }
    }

    // return GamePieces to their original positions
    public void ResetPieces()
    {
        for (int i = 0; i < gamePieces.Length; i++)
        {
            if (gamePieces[i] != null)
            {
                gamePieces[i].transform.position = originalPositions[i];
                gamePieces[i].transform.rotation = originalRotations[i];
            }
        }
    }

}
