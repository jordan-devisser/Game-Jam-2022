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

    [SerializeField] GameObject[] numberPrefabs;

    [SerializeField] int badBlockChance = 10;
    [SerializeField] float animationDelay = 0.2f;
    [SerializeField] float numberDelay = 0.5f;

    private Vector3 direction = Vector3.up;
    private bool revealReady = true;

    void Update()
    {
        // restart game if user presses Enter
        if (Input.GetKey(KeyCode.Return)) SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public IEnumerator CreateTiles(Vector3 direction)
    {
        // do not allow the player to reveal new tiles until the previous reveal is complete
        if (revealReady)
        {
            revealReady = false;

            Vector3 newPosition = player.position;

            // gets random value 1 - 6. note that raising this higher than 9 will cause issues with spawning numbers
            int length = Random.Range(1, 7);
            
            // DISABLED TEMPORARILY (? possibly for removal)
            // GenerateNumbers(newPosition, length);

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
                    // if a walkable tile already exists at this position, skip over it
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

                // this method is a co-routine (in order to stagger the tiles being revealed)
                yield return new WaitForSeconds(animationDelay);
            }

            revealReady = true;
        }
    }

    private void GenerateNumbers(Vector3 numPos, int diceRoll)
    {
        // gather created number objects to be destroyed in a coroutine after a countdown
        List<GameObject> numberObjects = new List<GameObject>();

        for (int i = 1; i <= diceRoll; i++)
        {
            numPos += direction;

            GameObject numberObject = Instantiate(numberPrefabs[i], numPos, Quaternion.identity);
            SpriteRenderer spriteRenderer = numberObject.GetComponent<SpriteRenderer>();
                
            if (spriteRenderer)
            {
                // DEBUG: Still not drawing on top of blocks, despite sorting layer ?
                spriteRenderer.sortingOrder = 1;
                spriteRenderer.sortingLayerName = "Numbers";
            }

            numberObjects.Add(numberObject);
            StartCoroutine(FadeNumber(numberObjects));
        }
    }

    private IEnumerator FadeNumber(List<GameObject> numberObjects)
    {
        for (int i = 0; i < numberObjects.Count; i++)
        {
            Destroy(numberObjects[i]);
            yield return new WaitForSeconds(numberDelay);
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
