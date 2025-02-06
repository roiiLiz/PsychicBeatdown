using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeleeAttackerComponent : MonoBehaviour
{
    [Header("Melee Attack Settings")]
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackRate = 1f;
    [SerializeField] Transform weaponPivot;
    [SerializeField] BoxCollider2D weaponHitbox;
    [Header("Attack Animation Variables")]
    [SerializeField] float lerpRate = 2f;
    [SerializeField] AnimationCurve windUpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] Vector3 windUpPosition;
    [SerializeField] float windUpHoldDuration = 0.35f;
    [SerializeField] AnimationCurve swingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] Vector3 attackPosition;
    [SerializeField] float attackHoldDuration = 0.25f;
    [SerializeField] AnimationCurve resetCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] Vector3 defaultPosition;

    public bool isInRange { get; private set; } = false;

    GameObject player => GameObject.FindGameObjectWithTag("Player");
    float distanceToPlayer = 0f;
    bool allowAttack = true;
    bool suppressCheck = false;
    Vector3 previousPos;
    Vector3 currentPos;

    void OnDisable()
    {
        weaponHitbox.enabled = false;
        StopAllCoroutines();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePivot();
        CheckForPlayer();
        Attack();
    }

    void RotatePivot()
    {
        if (suppressCheck == false)
        {
            previousPos = currentPos;
            currentPos = transform.position;

            if (currentPos.x > previousPos.x)
            {
                weaponPivot.localScale = new Vector3(-1f, 1f, 1f);
            } else if (currentPos.x < previousPos.x)
            {
                weaponPivot.localScale = new Vector3(1f, 1f, 1f);
            } else
            {
                return;
            }
        }
    }

    void CheckForPlayer()
    {
        if (suppressCheck == false)
        {
            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= attackRange)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
        }
    }

    void Attack()
    {
        if (allowAttack && isInRange)
        {
            allowAttack = false;
            suppressCheck = true;
            StartCoroutine(BeginAttack());            
        }
    }

    IEnumerator BeginAttack()
    {

        float t = 0f;
        float w = 0f;

        float rate = 1f / lerpRate;

        while (t < 1f)
        {
            // Debug.Log("Wind Up");
            t += Time.deltaTime * rate;
            float windUpRot = Mathf.Lerp(weaponPivot.transform.rotation.z, windUpPosition.z * -weaponPivot.localScale.x, windUpCurve.Evaluate(t));
            weaponPivot.transform.rotation = Quaternion.Euler(0f, 0f, windUpRot);
            yield return null;
        }

        yield return new WaitForSeconds(windUpHoldDuration);
        
        weaponHitbox.enabled = true;

        while (w < 1f)
        {
            // Debug.Log("Attacking");
            w += Time.deltaTime * rate;
            float attackRot = Mathf.Lerp(windUpPosition.z * -weaponPivot.localScale.x, attackPosition.z * -weaponPivot.localScale.x, swingCurve.Evaluate(w));
            weaponPivot.transform.rotation = Quaternion.Euler(0f, 0f, attackRot);
            yield return null;
        }

        weaponHitbox.enabled = false;

        yield return new WaitForSeconds(attackHoldDuration);

        StartCoroutine(BeginCooldown(attackRate));
    }

    IEnumerator BeginCooldown(float attackRate)
    {
        float r = 0f;

        float cooldown = 1f / attackRate;
        float rate = 1f / lerpRate;

        while (r < cooldown)
        {
            // Debug.Log("Reseting");
            r += Time.deltaTime * rate;
            float resetRot = Mathf.Lerp(attackPosition.z * -weaponPivot.localScale.x, defaultPosition.z * -weaponPivot.localScale.x, resetCurve.Evaluate(r));
            weaponPivot.transform.rotation = Quaternion.Euler(0f, 0f, resetRot);
            yield return null;
        }

        allowAttack = true;
        suppressCheck = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
