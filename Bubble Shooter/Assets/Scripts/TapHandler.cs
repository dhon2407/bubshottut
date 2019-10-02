using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapHandler : MonoBehaviour
{
    private BubbleGenerator bubbleGenerator;

    private void Start()
    {
        bubbleGenerator = GetComponent<BubbleGenerator>();
    }

    void Update()
    {
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
