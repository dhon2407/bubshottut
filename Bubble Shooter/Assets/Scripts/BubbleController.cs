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

    private Vector2 moveDirection;
    private Vector2 targetSlotLocation;
    public bool isMoving { get; private set; }
    public float radius { get; private set; }

    private void Awake()
    {
        OnStopMove = new UnityEvent();
        radius = (GetComponent<CircleCollider2D>().bounds.size.x / 2);
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
        StartCoroutine(MoveToSlot());
    }

    private IEnumerator MoveToSlot()
    {
        float speed = 10;
        while (Vector3.Distance(transform.position, targetSlotLocation) > 0.001f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetSlotLocation, step);
            yield return null;
        }
    }

    public void ReflectMovement()
    {
        Vector2 normalDirection = (moveDirection.x > 0) ? Vector2.right : Vector2.left;
        moveDirection = Vector2.Reflect(moveDirection, normalDirection);
    }

    public void SetSlotLocation(Vector2 location)
    {
        targetSlotLocation = location;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "SideWall":
                ReflectMovement();
                break;
            case "TopWall":
            case "Slot":
                StopMove();
                break;
            default:
                return;
        }
    }
}
