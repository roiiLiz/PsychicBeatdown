using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public float movementSpeed = 1f;

    public void MoveTowards(Vector2 dir, GameObject parent)
    {
        parent.transform.Translate(dir.normalized * movementSpeed * Time.deltaTime);
    }
}
