using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSlot : MonoBehaviour
{
    public bool Active;
    public bool OnTarget { get; set; }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        tag = Active ? "ActiveSlot" : "Slot";
        gameObject.layer = Active ? 9 : 0;
    }

    void Update()
    {
        if (OnTarget && Active)
            sprite.enabled = true;
        else
            sprite.enabled = false;
    }
}
