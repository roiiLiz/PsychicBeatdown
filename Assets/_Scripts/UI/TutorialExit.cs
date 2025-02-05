using UnityEngine;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] string levelToGoTo;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (levelToGoTo != null)
            {
                LevelLoader.instance.LoadLevel(levelToGoTo);
            }
        }
    }
}
