using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BubbleProjectionContoller : MonoBehaviour
{
    private BubbleGenerator bubbleGenerator;
    private TapHandler tapHandler;
    private BubbleController currentBubble { get => bubbleGenerator.mainBubble; }

    private void Start()
    {
        bubbleGenerator = GetComponent<BubbleGenerator>();
        tapHandler = GetComponent<TapHandler>();

        tapHandler.OnTapHold.AddListener(CreateTrail);
    }

    public void CreateTrail(Vector3 tapPosition)
    {
        if (!currentBubble.isMoving)
        {
            var rayCastOrigin = currentBubble.transform.position;
            var rayCastDirection = (tapPosition - rayCastOrigin).normalized;

            bool hitTopWall = false;
            
            do
            {
                var collisionSideOrigin = rayCastOrigin;
                collisionSideOrigin.x += (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);
                var hit = Physics2D.Raycast(collisionSideOrigin, rayCastDirection);

                if (hit.collider.CompareTag("SideWall"))
                {
                    rayCastDirection = Vector2.Reflect(rayCastDirection, (rayCastDirection.x > 0) ? Vector2.right : Vector2.left);
                    rayCastOrigin = hit.point;
                    rayCastOrigin.x += (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);
                }

            } while (!hitTopWall);

        }
    }

    public int FindLastSlotIndex(RaycastHit2D[] hits)
    {
        for (int i = hits.Length - 1; i >= 0; i--)
        {
            if (hits[i].collider.tag == "Slot")
                return i;
        }

        return 0;
    }
}
