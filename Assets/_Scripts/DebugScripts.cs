using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugScripts : MonoBehaviour
{
    [SerializeField] InputReader inputReader;
    [SerializeField] GameObject enemyPrefab;

    Vector3 mousePoint;

    void OnEnable() => inputReader.LookEvent += UpdateMousePosition;
    void OnDisable() => inputReader.LookEvent -= UpdateMousePosition;

    void UpdateMousePosition(Vector2 pos)
    {
        mousePoint = pos;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Instantiate(enemyPrefab, mousePoint, Quaternion.identity);
        }
    }
}
