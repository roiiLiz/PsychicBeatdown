using UnityEngine;

[CreateAssetMenu(menuName = "Stats/New Stat Block", fileName = "New Stats")]
public class Stats : ScriptableObject
{
    public int maxHealth;
    public float movementSpeed;
    public int damageAmount;
    public float thrownSpeed;
}
