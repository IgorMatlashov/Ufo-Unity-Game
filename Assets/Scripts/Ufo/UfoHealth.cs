using UnityEngine;

public class UfoHealth : MonoBehaviour
{
    [HideInInspector] public int health;
    private SceneLoader _sceneLoader;

    private void Start()
    {
        EventManager.SendHealthCount(health);
        _sceneLoader = FindAnyObjectByType<SceneLoader>();
    }

    private void Update()
    {
        CheckForDeath();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            health -= 1;
            EventManager.SendHealthCount(health);
        }
    }

    private void CheckForDeath()
    {
        if (health <= 0) _sceneLoader.RestartScene();
    }
}
