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

                var hit = Physics2D.Raycast(rayCastOrigin, rayCastDirection);

                if (hit.collider != null)
                {
                    var trueOrigin = rayCastOrigin;
                    trueOrigin.x -= (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);

                    Debug.DrawLine(trueOrigin, hit.point, Color.cyan, 3f);
                    rayCastOrigin = hit.point;
                    rayCastOrigin.x -= (Mathf.Sign(rayCastDirection.x) * currentBubble.radius);
                    rayCastDirection = Vector2.Reflect(rayCastDirection, (rayCastDirection.x > 0) ? Vector2.right : Vector2.left);

                    if (!hit.collider.tag.Contains("SideWall"))
                    {
                        trailDrawn = true;
                        currentBubble.SetSlotLocation(hit.collider.transform.position);
                    }
                }

            }
            Debug.DrawRay(currentBubble.transform.position, (tapPosition - currentBubble.transform.position).normalized, Color.cyan);
        }
    }
}
