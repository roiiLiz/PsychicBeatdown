using UnityEngine;

[CreateAssetMenu(menuName = "Combos/New Combo", fileName = "New Combo")]
public class Combo : ScriptableObject
{
    public string comboLetter;
    public float comboMultiplier;
    public float scoreThreshold;
    public Color color;
    public bool gradientEnabled;
    public float shakeMultiplier;
    [Range(0, 1f)] public float comboLengthMultiplier;
}