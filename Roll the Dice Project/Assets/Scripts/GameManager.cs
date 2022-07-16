using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] Tilemap tilemapWalkable;
    [SerializeField] Tilemap tilemapCollidable;
    [SerializeField] Tilemap tilemapWave;

    [SerializeField] TileBase[] pathTiles;
    [SerializeField] TileBase[] blockTiles;
    [SerializeField] TileBase waveTile;

    [SerializeField] GameObject[] numberPrefabs;

    [SerializeField] int badBlockChance = 10;
    [SerializeField] float animationDelay = 0.2f;
    [SerializeField] float numberDelay = 0.5f;
    [SerializeField] float waveDelay = 1f;
    [SerializeField] TextMeshProUGUI scoreText;

    private bool revealReady;
    private int waveHeight;
    private float nextWaveMoveTime;
    private int score;

    void Start()
    {
        revealReady = true;
        waveHeight = -9;
        nextWaveMoveTime = 0.0f;
        score = 0;
        // set a starting tile beneath the player
        tilemapWalkable.SetTile(tilemapWalkable.WorldToCell(player.position), pathTiles[0]);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return)) SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        // restart the scene if the wave hits the player. TODO: Tell the user the game has ended
        if (WaveTileExists(tilemapWave.WorldToCell(player.position))) SceneManager.LoadScene("SampleScene");

        RenderWave();
        MoveWave();
        scoreText.text = score.ToString();
    }

    void RenderWave()
    {
        Vector3 camLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 camRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        for (float x = camLeft.x; x <= camRight.x+1; x++)
        {
            for (float y = camLeft.y; y <= waveHeight; y++)
            {
                Vector3 newPosition = new Vector3(x, y, 0);
                if (WaveTileExists(tilemapWave.WorldToCell(newPosition)) == false)
                {
                    tilemapWave.SetTile(tilemapWave.WorldToCell(newPosition), waveTile);
                }
            }
        }

       

    }

    void MoveWave()
    {
       if (Time.timeSinceLevelLoad > nextWaveMoveTime)
        {
            nextWaveMoveTime += waveDelay;
            waveHeight += 1;
            score += 1;
        }
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
                    // if a walkable tile already exists at this position, cease revealing tiles
                    break;
                }
                else if (percentage <= badBlockChance)
                {
                    int randomTileNum = Random.Range(0, blockTiles.Length);
                    tilemapCollidable.SetTile(tilemapCollidable.WorldToCell(newPosition), blockTiles[randomTileNum]);

                    // cease revealing tiles once a collidable tile is made
                    break;
                }
                else
                {
                    int randomTileNum = Random.Range(0, pathTiles.Length);
                    tilemapWalkable.SetTile(tilemapWalkable.WorldToCell(newPosition), pathTiles[randomTileNum]);
                }

                // this method is a co-routine (in order to stagger the tiles being revealed)
                yield return new WaitForSeconds(animationDelay);
            }

            revealReady = true;
        }
    }

    /**
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
    } **/

    public bool WalkTileExists(Vector3Int targetPosition)
    {
        return tilemapWalkable.HasTile(targetPosition);
    }

    public bool CollideTileExists(Vector3Int targetPosition)
    {
        return tilemapCollidable.HasTile(targetPosition);
    }

    public bool WaveTileExists(Vector3Int targetPosition)
    {
        return tilemapWave.HasTile(targetPosition);
    }
}
