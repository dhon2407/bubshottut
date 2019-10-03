using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSlot : MonoBehaviour
{
    public bool Empty { get; private set; }
    public bool Active { get => active; }

    [SerializeField]
    private bool active = false;
    private List<BubbleSlot> adjacents = new List<BubbleSlot>();

    // Start is called before the first frame update
    void Start()
    {
        Empty = true;
    }

    public void Occupy()
    {
        Empty = false;
        foreach (var adjacent in adjacents)
            adjacent.SetActive();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tag)
            adjacents.Add(collision.GetComponent<BubbleSlot>());
    }

    public void SetActive()
    {
        active = true;
    }
}
