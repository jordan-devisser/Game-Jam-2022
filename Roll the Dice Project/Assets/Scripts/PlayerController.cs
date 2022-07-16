using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{ 
    private bool isMoving;
    private Vector3 origPos, targetPos;
    private Vector3 pointDirection = Vector3.up;

    [SerializeField] GameManager gameManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] float timeToMove = 0.2f; // time in seconds from A to B

    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite downSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;


    void Start()
    {
        isMoving = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // take input movements (also determines player "facing" for purposes of revealing new squares)
        if (Input.GetKey(KeyCode.W) && !isMoving) 
        {
            StartCoroutine(MovePlayer(Vector3.up));
            pointDirection = Vector3.up;
            spriteRenderer.sprite = upSprite;
        }
        if (Input.GetKey(KeyCode.A) && !isMoving) 
        {
            StartCoroutine(MovePlayer(Vector3.left));
            pointDirection = Vector3.left;
            spriteRenderer.sprite = leftSprite;
        }
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.down));
            pointDirection = Vector3.down;
            spriteRenderer.sprite = downSprite;
        }
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.right));
            pointDirection = Vector3.right;
            spriteRenderer.sprite = rightSprite;
        }

        // allow player to "create" squares with a diceroll - calls coroutine on GameManager
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(gameManager.CreateTiles(pointDirection));
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        // if there is a walkable tile at target, and there is not a blocking tile, move player to target pos
        if (gameManager.WalkTileExists(Vector3Int.FloorToInt(targetPos)) && 
        !gameManager.CollideTileExists(Vector3Int.FloorToInt(targetPos)))
        {
            while (elapsedTime < timeToMove)
            {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;
        }

        isMoving = false;
    }
}
