using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownObjectComponent : MonoBehaviour
{
    Stats stats;
    Transform sprite;
    bool shouldRotate;
    int pierceAmount;

    public void ThrownConstructor(Stats stats, bool shouldRotate, Transform sprite)
    {
        this.stats = stats;
        this.shouldRotate = shouldRotate;
        this.sprite = sprite;
    }
    
    void Start() => pierceAmount = stats.pierceAmount;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * stats.thrownSpeed * Time.deltaTime);
        if (!shouldRotate) { return; }        
        sprite.transform.Rotate(0f, 0f, stats.thrownRotationRate * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            HealthComponent enemyHealthComponent = other.GetComponent<HealthComponent>();

            if (enemyHealthComponent != null)
            {
                enemyHealthComponent.Damage(stats.maxHealth);

                pierceAmount -= 1;

                if (pierceAmount > 0) { return; }

                GetComponent<HealthComponent>().Die();
            }
        }
    }
}
