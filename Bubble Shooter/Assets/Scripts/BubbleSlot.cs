﻿using System;
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
        if (other.tag == tag)
            adjacentSlots.Add(other.gameObject.GetComponent<BubbleSlot>());
        else
            Debug.Log(name + " triggered by" + other.name);
    }

    public IEnumerator UpdateCollider()
    {
        yield return null;
        collider.enabled = Active;
    }

    public void Activate()
    {
        if (Active) return;

        Active = true;
        StartCoroutine(UpdateCollider());
    }

    public void Occupy()
    {
        Empty = false;
        foreach (var slot in adjacentSlots)
            slot.Activate();

        Active = false;
        UpdateCollider();
    }
}
