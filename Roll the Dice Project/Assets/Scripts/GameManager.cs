using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Tilemap tilemapWalkable;
    [SerializeField] Tilemap tilemapCollidable;

    [SerializeField] TileBase tile;
    [SerializeField] TileBase block;

    [SerializeField] int badBlockChance = 10;

    private Vector3 direction = Vector3.up;

    void Update()
    {
        // restart game if user presses Enter
        if (Input.GetKey(KeyCode.Return)) SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public IEnumerator CreateTiles(Vector3 direction)
    {
        Vector3 newPosition = player.position;

        int length = Random.Range(1, 7);
        Debug.Log("Dice roll result: " + length);

//        float elapsedTime = 0;

        for (int i = 0; i < length; i++)
        {
            newPosition += direction;
            int percentage = Random.Range(1, 101);

            if (CollideTileExists(Vector3Int.FloorToInt(newPosition)))
            {
                // if a collidable tile already exists in the path, cease revealing tiles
                break;
            }
            else if (WalkTileExists(Vector3Int.FloorToInt(newPosition)))
            {
                continue;
            }
            else if (percentage <= badBlockChance)
            {
                tilemapCollidable.SetTile(tilemapCollidable.WorldToCell(newPosition), block);

                // cease revealing tiles once a collidable tile is made
                break;
            }
            else
            {
                tilemapWalkable.SetTile(tilemapWalkable.WorldToCell(newPosition), tile);
            }
        }
    }

    public bool WalkTileExists(Vector3Int targetPosition)
    {
        return tilemapWalkable.HasTile(targetPosition);
    }

    public bool CollideTileExists(Vector3Int targetPosition)
    {
        return tilemapCollidable.HasTile(targetPosition);
    }
}
