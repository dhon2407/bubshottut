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

            rayCastOrigin.x += (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);

            bool trailDrawn = false;

            while (!trailDrawn)
            {
                var hits = Physics2D.RaycastAll(rayCastOrigin, rayCastDirection);

                Debug.Log("HITS : " + hits.Length);

                //var hit = Physics2D.Raycast(rayCastOrigin, rayCastDirection);
                int lastslotIndex = FindLastSlotIndex(hits);

                if (hits.Length > 0)
                {
                    var trueOrigin = rayCastOrigin;
                    trueOrigin.x -= (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);

                    Debug.DrawLine(trueOrigin, hits[lastslotIndex].point, Color.cyan, 3f);
                    rayCastOrigin = hits[lastslotIndex].point;
                    rayCastOrigin.x -= (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);
                    rayCastDirection = Vector2.Reflect(rayCastDirection, (rayCastDirection.x > 0) ? Vector2.right : Vector2.left);
                }

                if (hits.Length > 1)
                {
                    currentBubble.SetSlotLocation(hits[lastslotIndex].collider.GetComponent<BubbleSlot>());
                    trailDrawn = true;
                }

            }
            Debug.DrawRay(currentBubble.transform.position, (tapPosition - currentBubble.transform.position).normalized, Color.cyan);
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
