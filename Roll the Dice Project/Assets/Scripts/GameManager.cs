using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Tilemap tilemapWalkable;
    [SerializeField] Tilemap tilemapCollidable;

    [SerializeField] TileBase tile;
    [SerializeField] TileBase block;

    [SerializeField] int badBlockChance = 10;

    private Vector3 direction = Vector3.up;

    public void CreateTiles(Vector3 direction)
    {
        Vector3 newPosition = player.position;

        int length = Random.Range(1, 6);
        Debug.Log("Dice roll result: " + length);

        for (int i = 0; i < length; i++)
        {
            newPosition += direction;
            int percentage = Random.Range(1, 100);

            /** TO-DO:
                - Skip squares that have already been filled
            **/
            if (percentage <= badBlockChance)
            {
                tilemapCollidable.SetTile(tilemapCollidable.WorldToCell(newPosition), block);
            }
            else
            {
                tilemapWalkable.SetTile(tilemapWalkable.WorldToCell(newPosition), tile);
            }
        }
    }
}
