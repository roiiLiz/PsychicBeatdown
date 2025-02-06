using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    [SerializeField] float expansionRate = 0.5f;
    [SerializeField] float explosionLingerDuration = 1f;
    [SerializeField] float destructionSpeedMultiplier = 2f;
    [SerializeField] float screenShakeMultiplier;
    [SerializeField] Vector3 maxExplosionScale = new Vector3(2f, 2f, 1f);
    [SerializeField] AnimationCurve easingCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] AudioClip explosionAudio;
    Vector3 defaultScale;

    void Awake() => defaultScale = transform.localScale;

    void Start()
    {
        StartCoroutine(LerpScale(defaultScale, maxExplosionScale, false));
        ScreenShakeManager.instance.CameraShake(GetComponent<CinemachineImpulseSource>(), screenShakeMultiplier);
        AudioManager.instance.PlaySFX(explosionAudio, transform, 1f);
    }

    IEnumerator LerpScale(Vector3 from, Vector3 to, bool doubleSpeed)
    {
        float t = 0f;
        float rate = 1f / expansionRate;

        rate *= doubleSpeed ? destructionSpeedMultiplier : 1f;

        while (t < 1.0f)
        {
            t += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(from, to, easingCurve.Evaluate(t));
            yield return null;
        }

        if (transform.localScale == Vector3.zero) 
        { 
            Destroy(gameObject); 
        } else
        {
            yield return new WaitForSeconds(explosionLingerDuration);
            DestroyExplosion();
        }
    }

    void DestroyExplosion() => StartCoroutine(LerpScale(transform.localScale, Vector3.zero, true));

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies") || collision.gameObject.tag == "Player")
        {
            HealthComponent healthComponent = collision.GetComponent<HealthComponent>();
            if (healthComponent != null)
            {
                healthComponent.Damage(damageAmount);
            }
        } else if (collision.gameObject.GetComponent<Fireball>())
        {
            Fireball fireball = collision.gameObject.GetComponent<Fireball>();
            fireball.SpawnExplosion();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, (maxExplosionScale.x + maxExplosionScale.y) / 2f);
    }
}
