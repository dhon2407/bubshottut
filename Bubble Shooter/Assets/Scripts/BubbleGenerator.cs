using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    public GameObject bubblePrefab = null;
    public BubbleController mainBubble { get => currentBubble; }
    
    [SerializeField]
    private BubbleController currentBubble = null;

    private void Start()
    {
        if (currentBubble == null)
            CreateNewBubble();
        else
            currentBubble.OnStopMove.AddListener(OnCurrentBubbleStop);
    }

    private void CreateNewBubble()
    {
        currentBubble = Instantiate(bubblePrefab, transform, false).GetComponent<BubbleController>();
        currentBubble.OnStopMove.AddListener(OnCurrentBubbleStop);
    }

    private void OnCurrentBubbleStop()
    {
        currentBubble.OnStopMove.RemoveListener(OnCurrentBubbleStop);
        CreateNewBubble();
    }
}
