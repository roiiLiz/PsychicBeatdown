using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] float lerpRate = .5f;
    [SerializeField] Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1f);
    [SerializeField] AnimationCurve animCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] TextWobble textWobble;

    Vector3 defaultScale;

    void Awake()
    {
        defaultScale = transform.localScale;
        textWobble.enabled = false;
    }

    void OnDisable()
    {
        transform.localScale = defaultScale;
        textWobble.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textWobble.enabled = true;
        StartCoroutine(LerpScale(defaultScale, hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textWobble.enabled = false;
        StartCoroutine(LerpScale(hoverScale, defaultScale));
    }

    IEnumerator LerpScale(Vector3 from, Vector3 to)
    {
        float t = 0f;
        float rate = 1f / lerpRate;

        while (t < 1.0f)
        {
            t += Time.unscaledDeltaTime * rate;
            transform.localScale = Vector3.Lerp(from, to, animCurve.Evaluate(t));
            yield return null;
        }
    }
}