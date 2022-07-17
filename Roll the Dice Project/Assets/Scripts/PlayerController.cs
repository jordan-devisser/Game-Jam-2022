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
    private Animator animator;

    [SerializeField] float timeToMove = 0.2f; // time in seconds from A to B

    [SerializeField] Sprite upSprite;
    [SerializeField] Sprite downSprite;
    [SerializeField] Sprite leftSprite;
    [SerializeField] Sprite rightSprite;


    void Start()
    {
        isMoving = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // take input movements (also determines player "facing" for purposes of revealing new squares)
        if (Input.GetKey(KeyCode.W) && !isMoving) 
        {
            StartCoroutine(MovePlayer(Vector3.up));
            pointDirection = Vector3.up;
            //spriteRenderer.sprite = upSprite;
            //if (animator.GetInteger("Direction") != 0) animator.SetInteger("Direction", 0);
            if (QueryAnimation("Player_Up") != true) animator.SetTrigger("WalkUp");
        }
        if (Input.GetKey(KeyCode.A) && !isMoving) 
        {
            StartCoroutine(MovePlayer(Vector3.left));
            pointDirection = Vector3.left;
            //spriteRenderer.sprite = leftSprite;
            //if (animator.GetInteger("Direction") != 3) animator.SetInteger("Direction", 3);
            if (QueryAnimation("Player_Left") != true) animator.SetTrigger("WalkLeft");
        }
        if (Input.GetKey(KeyCode.S) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.down));
            pointDirection = Vector3.down;
            //spriteRenderer.sprite = downSprite;
            //if (animator.GetInteger("Direction") != 2) animator.SetInteger("Direction", 2);
            if (QueryAnimation("Player_Down") != true) animator.SetTrigger("WalkDown");
        }
        if (Input.GetKey(KeyCode.D) && !isMoving)
        {
            StartCoroutine(MovePlayer(Vector3.right));
            pointDirection = Vector3.right;
            //spriteRenderer.sprite = rightSprite;
            //if (animator.GetInteger("Direction") != 1) animator.SetInteger("Direction", 1);
            if (QueryAnimation("Player_Right") != true) animator.SetTrigger("WalkRight");
        }

        // allow player to "create" squares with a diceroll - calls coroutine on GameManager
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(gameManager.CreateTiles(pointDirection));
    }

    private bool QueryAnimation(string animation)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(animation);
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
