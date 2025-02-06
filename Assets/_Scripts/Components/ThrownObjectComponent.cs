using System;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectComponent : MonoBehaviour
{
    ThrowableType throwableType;
    Stats stats;
    Transform sprite;
    bool shouldRotate;
    int pierceAmount;

    public void ThrownConstructor(Stats stats, bool shouldRotate, Transform sprite, ThrowableType throwType)
    {
        this.stats = stats;
        this.shouldRotate = shouldRotate;
        this.sprite = sprite;
        this.throwableType = throwType;
    }
    
    void Start() => pierceAmount = stats.pierceAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * stats.thrownSpeed * Time.deltaTime);
        if (!shouldRotate) { return; }        
        sprite.transform.Rotate(0f, 0f, stats.thrownRotationRate * Time.deltaTime);
    }

    private ThrowableType GetThrowableType()
    {
        return throwableType;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            HealthComponent enemyHealthComponent = other.GetComponent<HealthComponent>();

            if (enemyHealthComponent != null)
            {
                pierceAmount--;
                
                switch (throwableType)
                {
                case ThrowableType.FIREBALL:
                    if (pierceAmount >= 0)
                    {
                        GetComponent<Fireball>().SpawnExplosion();
                        Destroy(gameObject);
                    } else
                    {
                        Destroy(gameObject);
                    }

                    break;
                case ThrowableType.ARROW:
                    if (pierceAmount >= 0)
                    {
                        enemyHealthComponent.Damage(stats.damageAmount);
                    } else
                    {
                        Destroy(gameObject);
                    }
                    
                    break;
                case ThrowableType.ENEMY:
                    if (pierceAmount >= 0)
                    {
                        enemyHealthComponent.Damage(stats.maxHealth);
                        GetComponent<HealthComponent>().Die();
                    } else
                    {
                        Destroy(gameObject);
                    }

                    break;
                default:
                    break;
                }
            }
        }
        
    }
}
