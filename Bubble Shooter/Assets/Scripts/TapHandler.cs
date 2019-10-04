using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TapHandler : MonoBehaviour
{
    public class UnityEventOnTap : UnityEvent<Vector3> { }

    public UnityEventOnTap OnTapHold;

    private bool tapHold;
    private BubbleGenerator bubbleGenerator;
    private Vector3 bubblePosition { get => bubbleGenerator.mainBubble.transform.position; }
    private Vector3 tapPosition;

    private void Awake()
    {
        OnTapHold = new UnityEventOnTap();
    }

    private void Start()
    {
        bubbleGenerator = GetComponent<BubbleGenerator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (tapPosition.y > bubblePosition.y + 1.5f)
            {
                OnTapHold.Invoke(tapPosition);
                tapHold = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && tapHold)
        {
            Vector2 direction = (tapPosition - bubblePosition).normalized;
            bubbleGenerator.mainBubble.StartMove(direction);

            tapHold = false;
        }


        if (Input.GetKeyUp(KeyCode.Space))
            bubbleGenerator.mainBubble.ReflectMovement();

        if (Input.GetKeyUp(KeyCode.S))
            bubbleGenerator.mainBubble.StopMove();

        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
