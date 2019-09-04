using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    public KeyCode upKey, downKey, leftKey, rightKey;
    public KeyCode dropBombKey;
    [Range(1, 2)]
    public int playerNumber = 1;
    public float moveSpeed = 5f;
    public bool canDropBombs = true;
    public bool canMove = true;
    public bool isDead = false;

    public GameObject bombPrefab;

    private Rigidbody rigidBody;
    private Transform myTransform;
    private Animator animator;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        myTransform = transform;
        animator = myTransform.Find("PlayerModel").GetComponent<Animator>();
    }

    private void Update()
    {
        bool up = Input.GetKey(upKey);
        bool down = Input.GetKey(downKey);
        bool left = Input.GetKey(leftKey);
        bool right = Input.GetKey(rightKey);
        Move(up, down, left, right);

        if (canDropBombs && Input.GetKeyDown(dropBombKey))
        {
            DropBomb();
        }
    }

    public void Move(bool up, bool down, bool left, bool right)
    {
        animator.SetBool("Walking", up || down || left || right);

        if (!canMove)
        {
            return;
        }

        if (up)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (left)
        {
            rigidBody.velocity = new Vector3(-moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 270, 0);
        }

        if (down)
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, -moveSpeed);
            myTransform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (right)
        {
            rigidBody.velocity = new Vector3(moveSpeed, rigidBody.velocity.y, rigidBody.velocity.z);
            myTransform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void DropBomb()
    {
        if (bombPrefab)
        {
            Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), bombPrefab.transform.position.y, Mathf.RoundToInt(myTransform.position.z)), bombPrefab.transform.rotation);
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (!isDead && col.CompareTag("Explosion"))
        {
            Debug.Log("P" + playerNumber + " hit by explosion!");
            isDead = true;
            GameManager.Instance.PlayerDied(playerNumber);
            Destroy(gameObject);
        }
    }
}