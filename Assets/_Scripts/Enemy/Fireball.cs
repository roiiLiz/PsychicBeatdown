using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float fireballSpeed;
    [SerializeField] GameObject explosionPrefab;

    bool isSelected = false;

    void OnEnable() => ThrowScript.HeldObject += IsSelected;
    void OnDisable() => ThrowScript.HeldObject -= IsSelected;

    private void IsSelected(GameObject context)
    {
        if (context == this.gameObject)
        {
            isSelected = true;
        } else
        {
            isSelected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSelected)
        {
            transform.Translate(Vector2.right * Time.deltaTime * fireballSpeed);
        }
    }

    public void SpawnExplosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
