using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;

    private bool isMoving;
    private Vector2 moveDirection;

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
    }

    public void ReflectMovement()
    {
        Vector2 normalDirection = (moveDirection.x > 0) ? Vector2.right : Vector2.left;
        moveDirection = Vector2.Reflect(moveDirection, normalDirection);
    }
}
