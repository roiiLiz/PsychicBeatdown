using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    [SerializeField] Transform enemyContainer;
    [SerializeField] float lerpRate = 2f;
    [SerializeField] AnimationCurve grabCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    // [field: SerializeField] public int throwManaCost { get; private set; } = 25;
    // [SerializeField] float attackCooldown = 0.15f;

    public GameObject currentSelection { get; private set; } = null;
    public GameObject heldObject { get; private set; } = null;
    public bool canAttack { get; private set; } = true;

    public static event Action<int> OnGrabObject;
    public static event Action<GameObject> HeldObject;

    void OnEnable() { ThrowableComponent.OnThrowableSelected += SetCurrentSelection; }
    void OnDisable() { ThrowableComponent.OnThrowableSelected -= SetCurrentSelection; }

    void SetCurrentSelection(MonoBehaviour context) => currentSelection = context != null ? context.gameObject : null;

    public void HandleFireInput()
    {
        // if already has held throwable, then throw it
        if (heldObject != null)
        {
            ThrowCurrentObject();
            return;
        }

        if (currentSelection != null && heldObject == null)
        {
            SetHeldObject(currentSelection);
        }
    }

    void ThrowCurrentObject()
    {
        heldObject.transform.rotation = heldObject.transform.parent.transform.rotation;
        heldObject.layer = LayerMask.NameToLayer("ThrownObjects");
        heldObject.transform.SetParent(null);

        if (!heldObject.TryGetComponent<ThrownObjectComponent> (out ThrownObjectComponent thrownComponent))
        {
            ThrowableComponent throwableComponent = heldObject.GetComponent<ThrowableComponent>();

           heldObject.AddComponent<ThrownObjectComponent>();
            heldObject.GetComponent<ThrownObjectComponent>().ThrownConstructor(throwableComponent.stats, throwableComponent.shouldRotate, throwableComponent.sprite, throwableComponent.throwType);
        }

        heldObject = null;
        HeldObject?.Invoke(null);
    }

    void SetHeldObject(GameObject currentSelection)
    {
        heldObject = currentSelection;
        heldObject.layer = LayerMask.NameToLayer("HeldObjects");

        Enemy enemy = heldObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.ChangeState(EnemyState.HELD);
        }

        heldObject.transform.parent = enemyContainer;
        StartCoroutine(LerpToDefault(heldObject, heldObject.transform.localPosition, Vector3.zero, lerpRate));
        StartCoroutine(RotateToDefault(heldObject, heldObject.transform.localRotation, heldObject.transform.parent.transform.localRotation, lerpRate));

        HeldObject?.Invoke(heldObject);
    }

    IEnumerator RotateToDefault(GameObject thrownObject, Quaternion fromRot, Quaternion toRot, float duration)
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

    IEnumerator LerpToDefault(GameObject thrownObject, Vector3 from, Vector3 to, float duration)
    {
        canAttack = false;

        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thrownObject.transform.localPosition = Vector3.Lerp(from, to, grabCurve.Evaluate(i));
            yield return null;
        }

        canAttack = true;
    }
}
