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

        }
    }
}
