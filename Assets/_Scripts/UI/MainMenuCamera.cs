using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] float lerpRate = .5f;
    [SerializeField] AnimationCurve animCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [SerializeField] float xRange, yRange = -20f;

    Vector3 previousPos;
    Vector3 newPos;

    void Start()
    {
        SelectNewPoint();
    }

    void SelectNewPoint()
    {
        previousPos = transform.position;

        newPos = new Vector3(UnityEngine.Random.Range(-xRange, xRange), UnityEngine.Random.Range(-yRange, yRange), transform.position.z);

        StartCoroutine(LerpToPoint(previousPos, newPos));
    }

    IEnumerator LerpToPoint(Vector3 from, Vector3 to)
    {
        float t = 0f;
        float rate = 1f / lerpRate;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(from, to, animCurve.Evaluate(t));
            yield return null;
        }

        SelectNewPoint();
    }
}
