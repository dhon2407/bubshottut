using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleController : MonoBehaviour
{
    public UnityEvent OnStopMove;

    //TODO should be set somewhere not in public
    public Vector3 endPosition;


    [SerializeField]
    private float moveSpeed = 0;

    private bool isMoving;
    private Vector2 moveDirection;

    private void Awake()
    {
        OnStopMove = new UnityEvent();
    }

    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (isMoving)
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void StartMove(Vector2 direction)
    {
        if (isMoving) return;

        isMoving = true;
        moveDirection = direction;
    }

    public void StopMove()
    {
        isMoving = false;
        moveDirection = Vector2.zero;
        transform.position = endPosition;
        OnStopMove.Invoke();
    }

    public void ReflectMovement()
    {
        Vector2 normalDirection = (moveDirection.x > 0) ? Vector2.right : Vector2.left;
        moveDirection = Vector2.Reflect(moveDirection, normalDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "SideWall":
                ReflectMovement();
                break;
            case "TopWall":
            case "ActiveSlot":
                StopMove();
                break;
            default:
                return;
        }
    }
}
