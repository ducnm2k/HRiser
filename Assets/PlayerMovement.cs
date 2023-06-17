using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    //component
    private Rigidbody2D rb;
    private Collider2D collider2D;
    private Vector2 runVelocity;
    //public var
    public float jumpForce;
    public float runSpeed;
    public Object terrain;
    public GameObject camera;
    public GameObject wall;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Game Start!");
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //jumping up
        if (rb.velocity.y >= 0)
        {
            terrain.GetComponent<TilemapCollider2D>().isTrigger = true;
            if (terrain.GetComponent<TilemapCollider2D>().isTrigger == true && rb.velocity.y == 0)
            {
                //
                SmoothCameraFollow(0.5f);
            }
        }
        else
        {
            terrain.GetComponent<TilemapCollider2D>().isTrigger = false;
        }
        //running
        Vector2 runVelocity = new Vector2(runSpeed, rb.velocity.y);
        TurnAround(wall);
        rb.velocity = runVelocity;
        //jump
        JumpAction(jumpForce);
    }

    private void TurnAround(GameObject wall)
    {
        if (Mathf.Abs(rb.velocity.x) <= 1)
        {
            runSpeed = -runSpeed;
        }
    }

    private void SmoothCameraFollow(float smoothness)
    {
        Vector3 startPosition = camera.transform.position;
        Vector3 targetPosition = new Vector3(camera.transform.position.x, this.transform.position.y, -10);
        Vector3 smoothFollow = Vector3.Lerp(startPosition, targetPosition, smoothness);

        camera.transform.position = smoothFollow;
    }

    private void JumpAction(float jumpForce)
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
