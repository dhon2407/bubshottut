using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BubbleSlot : MonoBehaviour
{
    private new CircleCollider2D collider;
    private List<BubbleSlot> adjacentSlots;

    [SerializeField]
    private bool Active;
    private bool Empty;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        adjacentSlots = new List<BubbleSlot>();
        Empty = true;
    }

    private void Start()
    {
        StartCoroutine(UpdateCollider());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Slot")
            adjacentSlots.Add(other.gameObject.GetComponent<BubbleSlot>());
    }

    public IEnumerator UpdateCollider()
    {
        yield return null;
        collider.enabled = Active;
    }


}
