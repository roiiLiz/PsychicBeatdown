using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    [SerializeField] Transform enemyContainer;
    [SerializeField] float lerpRate = 2f;
    [SerializeField] AnimationCurve grabCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    IThrowable throwable = null;
    public GameObject throwableObject { get; private set; } = null;

    void OnEnable() { ; }
    void OnDisable() { ; }

    private void HandleThrow(IThrowable throwSelection)
    {
        if (throwable == null)
        {
            throwable = throwSelection;
        }
    }
}
