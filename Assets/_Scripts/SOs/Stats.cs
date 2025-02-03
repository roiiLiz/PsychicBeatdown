using UnityEngine;

[CreateAssetMenu(menuName = "Stats/New Stat Block", fileName = "New Stats")]
public class Stats : ScriptableObject
{
    [Header("Gameplay Stats")]
    public int maxHealth;
    public float movementSpeed;
    public int damageAmount;
    public int pierceAmount;
    [Header("Thrown Stats")]
    public float thrownSpeed;
    public float thrownRotationRate;
    public float heldRotationMultiplier;
    public bool shouldRotate;
}
