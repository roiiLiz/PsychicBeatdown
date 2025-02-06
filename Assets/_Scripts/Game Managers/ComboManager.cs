using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Slider comboSlider;
    [SerializeField] Image comboFill;
    [SerializeField] TextMeshProUGUI letterText, multiplierText;
    [SerializeField] CanvasGroup comboIndicator;
    [Header("Interpolation Values")]
    [SerializeField] AnimationCurve lerpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] float lerpRate = 1f;
    [Header("Combo Variables")]
    [SerializeField] float maxComboTime = 7.5f;
    [SerializeField] float perEnemyComboExtension = 0.5f;
    [SerializeField] List<Combo> combos = new List<Combo>();
    [SerializeField] Color defaultColor;

    float currentComboTime, comboMultiplier;
    bool suppressUpdates, isComboActivated;
    int comboScore, totalComboScore;
    TextWobble letterWobble, multiplierWobble;

    public static event Action<int> ComboScore;

    void OnEnable()
    {
        EnemyDeathComponent.OnEnemyDeath += GainCombo;
        LoopAnimator.LoopInProgress += AreUpdatesSuppressed;
        PlayerDeathComponent.OnPlayerDeath += EndCombo;
    }

    void OnDisable()
    {
        EnemyDeathComponent.OnEnemyDeath -= GainCombo;
        LoopAnimator.LoopInProgress -= AreUpdatesSuppressed;
        PlayerDeathComponent.OnPlayerDeath -= EndCombo;
    }

    void EndCombo() => AddComboScore();

    void Start()
    {
        comboIndicator.alpha = 0f;
        comboSlider.maxValue = maxComboTime;
        comboSlider.value = comboSlider.maxValue;

        letterWobble = letterText.GetComponent<TextWobble>();
        multiplierWobble = multiplierText.GetComponent<TextWobble>();

        ResetSettings();
    }

    void Update()
    {
        if (!suppressUpdates)
        {
            if (currentComboTime > 0f)
            {
                currentComboTime -= Time.deltaTime;

                comboSlider.value = currentComboTime;
            } else
            {
                if (isComboActivated)
                {
                    isComboActivated = false;

                    AddComboScore();

                    ResetSettings();

                    StartCoroutine(LerpAlpha(comboIndicator.alpha, 0f));
                }
            }
        }
    }

    private void ResetSettings()
    {
        comboScore = 0;
        comboMultiplier = 0f;

        if (letterWobble != null)
        {
            letterWobble.EnableWobble(false);
        }

        if (multiplierWobble != null)
        {
            multiplierWobble.EnableWobble(false);
        }

        letterText.color = defaultColor;
        multiplierText.color = defaultColor;
        comboFill.color = defaultColor;
    }

    void GainCombo()
    {
        if (!isComboActivated)
        {
            currentComboTime = maxComboTime;
        } else
        {
            currentComboTime = Mathf.Clamp(currentComboTime + perEnemyComboExtension, 0f, maxComboTime);
        }

        if (comboIndicator.alpha != 1f)
        {
            StopAllCoroutines();
            StartCoroutine(LerpAlpha(comboIndicator.alpha, 1f));
        }
  
        isComboActivated = true;

        comboScore += 1;
        MapCombo();
    }

    void MapCombo()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo combo = combos[i];
            if (comboScore >= combo.scoreThreshold)
            {
                comboMultiplier = combo.comboMultiplier;

                letterText.text = $"{combo.comboLetter}";
                multiplierText.text = $"x{combo.comboMultiplier}";
                
                if (combo.enableShake)
                {

                    if (letterWobble != null)
                    {
                        letterWobble.EnableWobble(true);
                    }

                    if (multiplierWobble != null)
                    {
                        multiplierWobble.EnableWobble(true);
                    }
                }

                letterText.color = combo.color;
                multiplierText.color = combo.color;
                comboFill.color = combo.color;
            }
        }
    }

    void AreUpdatesSuppressed(bool updatesAreSuppressed) => suppressUpdates = updatesAreSuppressed;

    IEnumerator LerpAlpha(float from, float to)
    {
        float t = 0f;
        float rate = 1f / lerpRate;

        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            comboIndicator.alpha = Mathf.Lerp(from, to, lerpCurve.Evaluate(t));
            yield return null;
        }
    }

    void AddComboScore()
    {
        totalComboScore = Mathf.RoundToInt(comboScore * comboMultiplier);

        ComboScore?.Invoke(totalComboScore - comboScore);
    }

}
