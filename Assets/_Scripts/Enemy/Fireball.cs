using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Fireball : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float fireballSpeed;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] SpriteRenderer selectionSprite;
    [SerializeField] SpriteMask spriteMask;

    bool isSelected = false;

    void OnEnable() => ThrowScript.HeldObject += IsSelected;
    void OnDisable() => ThrowScript.HeldObject -= IsSelected;

    private void IsSelected(GameObject context)
    {
        if (context == this.gameObject)
        {
            isSelected = true;
        } else
        {
            isSelected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSelected)
        {
            transform.Translate(Vector2.right * Time.deltaTime * fireballSpeed);
        }
    }

    public void SpawnExplosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectionSprite.enabled = true;
        spriteMask.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectionSprite.enabled = false;
        spriteMask.enabled = false;
    }
}
