﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickUpItem : MonoBehaviour {

    public Item item;

    private SpriteRenderer itemSpriteRenderer;

    private void OnValidate()
    {
        itemSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        gameObject.name = item.name + " pick up";
        itemSpriteRenderer.sprite = item.itemIcon;
        itemSpriteRenderer.drawMode = SpriteDrawMode.Sliced;
        itemSpriteRenderer.size = new Vector2(2, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
