using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimator : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] Transform sprite;
    // Update is called once per frame
    void Update()
    {
        sprite.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
