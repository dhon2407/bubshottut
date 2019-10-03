using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapHandler : MonoBehaviour
{
    public LayerMask bubbleCollision;

    private BubbleGenerator bubbleGenerator;

    private void Start()
    {
        bubbleGenerator = GetComponent<BubbleGenerator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 worldTapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 sourcePosition = bubbleGenerator.mainBubble.transform.position;
            Vector2 direction = (worldTapPosition - sourcePosition).normalized;

            if (worldTapPosition.y > sourcePosition.y + 1.5f)
            {

                RaycastHit2D hit;
                do
                {
                    hit = Physics2D.Raycast(sourcePosition, direction, float.PositiveInfinity, bubbleCollision);
                    //RayCasting
                    if (hit.collider != null)
                    {

                        //if (hit.collider.tag.Contains("Wall"))
                        Debug.Log("Hitting...  " + hit.collider.name);
                        Debug.Log("Direction :" + direction);
                        
                        
                        Vector2 normalDirection = (direction.x > 0) ? Vector2.right : Vector2.left;
                        direction = Vector2.Reflect(direction, normalDirection);
                        Vector2 reflectPosition = hit.point;
                        reflectPosition.y -= bubbleGenerator.bubbleRad;

                        Debug.DrawLine(sourcePosition, reflectPosition, Color.cyan);

                        sourcePosition = reflectPosition;
                        
                    }

                    if (hit.collider.tag == "ActiveSlot")
                    {
                        bubbleGenerator.mainBubble.endPosition = hit.collider.transform.position;
                        break;
                    }
                } while (hit.collider.tag != "TopWall");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 worldTapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 bubblePosition = bubbleGenerator.mainBubble.transform.position;

            if (worldTapPosition.y > bubblePosition.y + 1.5f)
            {
                Vector2 direction = (worldTapPosition - bubblePosition).normalized;
                bubbleGenerator.mainBubble.StartMove(direction);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
            bubbleGenerator.mainBubble.ReflectMovement();

        if (Input.GetKeyUp(KeyCode.S))
            bubbleGenerator.mainBubble.StopMove();

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
