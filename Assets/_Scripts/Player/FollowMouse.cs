using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] float distanceThreshold;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.transform.position + mousePosition) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -distanceThreshold + player.transform.position.x, distanceThreshold + player.transform.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -distanceThreshold + player.transform.position.y, distanceThreshold + player.transform.position.y);

        transform.position = targetPos;
    }
}