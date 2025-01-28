using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    [SerializeField] Transform enemyContainer;
    [SerializeField] float lerpRate = 2f;
    [SerializeField] AnimationCurve grabCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [field: SerializeField] public int throwManaCost { get; private set; } = 25;

    IThrowable throwable = null;
    public GameObject throwableObject { get; private set; } = null;
    public bool allowAttack { get; private set; } = true;

    void OnEnable() { Enemy.OnSelected += HandleThrow; }
    void OnDisable() { Enemy.OnSelected += HandleThrow; }

    private void HandleThrow(IThrowable throwSelection)
    {
        // Grab enemy if no enemy is held
        if (throwable == null)
        {
            throwable = throwSelection;
            throwableObject = throwable.GetThrowableObject();
            throwableObject.GetComponent<Enemy>().ChangeState(EnemyState.HELD);

            throwableObject.transform.parent = enemyContainer;
            StartCoroutine(LerpToDefault(throwableObject, throwableObject.transform.localPosition, Vector3.zero, lerpRate));
            StartCoroutine(RotateToDefault(throwableObject, throwableObject.transform.localRotation, throwableObject.transform.parent.transform.localRotation, lerpRate));
        } else // Throw enemy if enemy is held
        {
            throwableObject.transform.rotation = throwableObject.transform.parent.transform.rotation;
            throwableObject.transform.SetParent(null);
            throwableObject.GetComponent<Enemy>().ChangeState(EnemyState.THROWN);

            throwable = null;
            throwableObject = null;
        }
    }

    private IEnumerator RotateToDefault(GameObject thrownObject, Quaternion fromRot, Quaternion toRot, float duration)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thrownObject.transform.localRotation = Quaternion.Lerp(fromRot, toRot, grabCurve.Evaluate(i));
            yield return null;
        }
    }

    private IEnumerator LerpToDefault(GameObject thrownObject, Vector3 from, Vector3 to, float duration)
    {
        allowAttack = false;

        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thrownObject.transform.localPosition = Vector3.Lerp(from, to, grabCurve.Evaluate(i));
            yield return null;
        }

        // throwableObject.transform.localPosition = Vector3.zero;
        allowAttack = true;
    }

    public void ThrowObject(GameObject objectToThrow)
    {
        objectToThrow.transform.rotation = objectToThrow.transform.parent.transform.rotation;
        objectToThrow.transform.SetParent(null);
        objectToThrow.GetComponent<Enemy>().ChangeState(EnemyState.THROWN);

        throwable = null;
        throwableObject = null;
    }
}
