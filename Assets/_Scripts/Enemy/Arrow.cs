using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float arrowSpeed;

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
            transform.Translate(Vector2.right * Time.deltaTime * arrowSpeed);
        }
    }
}
