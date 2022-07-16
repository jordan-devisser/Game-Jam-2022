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
        // take input movements
        if (Input.GetKey(KeyCode.W) && !isMoving) StartCoroutine(MovePlayer(Vector3.up));
        if (Input.GetKey(KeyCode.A) && !isMoving) StartCoroutine(MovePlayer(Vector3.left));
        if (Input.GetKey(KeyCode.S) && !isMoving) StartCoroutine(MovePlayer(Vector3.down));
        if (Input.GetKey(KeyCode.D) && !isMoving) StartCoroutine(MovePlayer(Vector3.right));

        // take inputs for "pointing direction" - relevant to revealing new squares
        if (Input.GetKey(KeyCode.UpArrow)) 
        {
            pointDirection = Vector3.up;
            spriteRenderer.sprite = upSprite;
        }
        else if (Input.GetKey(KeyCode.DownArrow)) {
            pointDirection = Vector3.down;
            spriteRenderer.sprite = downSprite;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pointDirection = Vector3.left;
            spriteRenderer.sprite = leftSprite;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            pointDirection = Vector3.right;
            spriteRenderer.sprite = rightSprite;
        }

        // allow player to "create" squares with a diceroll - calls method on GameManager
        if (Input.GetKeyDown(KeyCode.Space)) gameManager.CreateTiles(pointDirection);
    }

    private IEnumerator MovePlayer(Vector3 direction)
    {
        isMoving = true;

        float elapsedTime = 0;

        origPos = transform.position;
        targetPos = origPos + direction;

        while (elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        isMoving = false;
    }
}
