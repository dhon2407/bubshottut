using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BubbleController : MonoBehaviour
{
    public UnityEvent OnStopMove;

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
            case "Bubble":
                StopMove();
                break;
            default:
                return;
        }
    }
}
