using UnityEngine;
public class FinishPlatform : MonoBehaviour
{
    public GameObject _finishMenu;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") _finishMenu.SetActive(true);
    }
}
