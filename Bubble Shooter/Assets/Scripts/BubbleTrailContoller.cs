using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTrailContoller : MonoBehaviour
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
            var trailRayOrigin = currentBubble.transform.position;
            var trailDirection = (tapPosition - trailRayOrigin).normalized;

            bool endTrail = false;

            while (!endTrail)
            {
                var hit = Physics2D.Raycast(trailRayOrigin, trailDirection);
                var nextTrailOrigin = trailRayOrigin;

                if (!hit.collider.CompareTag("SideWall"))
                {
                    endTrail = true;

                    var hits = Physics2D.RaycastAll(trailRayOrigin, trailDirection);
                    var hitslotIndex = FindLastSlotIndex(hits);

                    nextTrailOrigin = hits[hitslotIndex].point;

                    currentBubble.SetSlotLocation(hits[hitslotIndex].collider.GetComponent<BubbleSlot>());
                }
                else
                {
                    var collisionSideOrigin = trailRayOrigin;
                    collisionSideOrigin.x += (Mathf.Sign(trailDirection.x) * currentBubble.radius);
                    hit = Physics2D.Raycast(collisionSideOrigin, trailDirection);
                    trailDirection = Vector2.Reflect(trailDirection, (trailDirection.x > 0) ? Vector2.right : Vector2.left);
                    nextTrailOrigin = hit.point;
                    nextTrailOrigin.x += (Mathf.Sign(trailDirection.x) * currentBubble.radius);
                }

                Debug.DrawLine(trailRayOrigin, nextTrailOrigin, Color.cyan);
                trailRayOrigin = nextTrailOrigin;
            }

        }
    }

    public int FindLastSlotIndex(RaycastHit2D[] hits)
    {
        int lastIndex = 0;
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "Slot")
            {
                if (hits[i].collider.GetComponent<BubbleSlot>().Empty)
                    lastIndex = i;
                else
                    break;
            }
        }

        return lastIndex;
    }
}
