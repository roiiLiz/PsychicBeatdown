using System;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] float distanceThreshold = 5f;

    GameObject player;
    Vector2 lookInput;

    void OnEnable() => input.LookEvent += UpdateMousePosition;
    void OnDisable() => input.LookEvent -= UpdateMousePosition;

    void UpdateMousePosition(Vector2 pos)
    {
        lookInput = pos;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(lookInput);
        Vector3 targetPos = (player.transform.position + mousePosition) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, -distanceThreshold + player.transform.position.x, distanceThreshold + player.transform.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -distanceThreshold + player.transform.position.y, distanceThreshold + player.transform.position.y);

        transform.position = targetPos;
    }
}