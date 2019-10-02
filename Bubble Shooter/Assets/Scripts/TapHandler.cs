using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapHandler : MonoBehaviour
{
    [SerializeField]
    private BubbleController mainBubble = null;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 worldTapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 bubblePosition = mainBubble.transform.position;

            if (worldTapPosition.y > bubblePosition.y + 1.5f)
            {
                Vector2 direction = (worldTapPosition - bubblePosition).normalized;
                mainBubble.StartMove(direction);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
            mainBubble.ReflectMovement();

        if (Input.GetKeyUp(KeyCode.S))
            mainBubble.StopMove();

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
