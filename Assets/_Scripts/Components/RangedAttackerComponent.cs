using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackerComponent : MonoBehaviour
{
    [SerializeField] float attackRange = 1f;
    [SerializeField] float attackRate = 1f;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform pivot;
    [SerializeField] Transform firingPoint;
    [SerializeField] AudioClip attackSound;

    public bool isInRange { get; private set; } = false;

    GameObject player => GameObject.FindGameObjectWithTag("Player");
    float distanceToPlayer = 0f;
    bool allowAttack = true;

    // Update is called once per frame
    void Update()
    {
        RotatePivot(player.transform.position);
        CheckForPlayer();
        Attack();
    }

    void RotatePivot(Vector3 playerPos)
    {
        float lookAngle = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x) * Mathf.Rad2Deg;

        pivot.transform.rotation = Quaternion.AngleAxis(lookAngle, Vector3.forward);
    }

    void CheckForPlayer()
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

    void Attack()
    {
        if (allowAttack && isInRange)
        {
            // fire projectile
            var projectile = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
            // turn off attack
            allowAttack = false;
            // play sound
            AudioManager.instance.PlaySFX(attackSound, transform, 1f);
            // cooldown
            StartCoroutine(BeginCooldown(attackRate));
        }
    }

    IEnumerator BeginCooldown(float attackRate)
    {
        float t = 0f;
        float cooldown = 1f / attackRate;

        while (cooldown > t)
        {
            t += Time.deltaTime;
            yield return null;
        }
        
        allowAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
