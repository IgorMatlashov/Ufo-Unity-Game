using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    private float _health;

    private void Awake()
    {
        _health = GameManager.Instance.selectedUfo.health;

        EventManager.OnHealthCount.AddListener(healthcount =>
        {
            Debug.Log(healthcount);
            GetComponent<Image>().fillAmount = healthcount / _health;
        });
    }
}
