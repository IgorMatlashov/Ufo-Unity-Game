using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UfoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ufoModel;
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _maxSpeedBar;
    [SerializeField] private Image _accelerationBar;
    [SerializeField] private Image _rotationSpeedBar;

    private float _maxValue = 50;
    private void Awake()
    {
        EventManager.OnInfoPanel.AddListener(UfoDisplayInfo);
    }

    public void UfoDisplayInfo(Ufo ufoData)
    {
        _ufoModel.text = ufoData.modelName;
        _healthBar.fillAmount = ufoData.health / _maxValue;
        _maxSpeedBar.fillAmount = ufoData.maxSpeed / _maxValue;
        _accelerationBar.fillAmount = ufoData.acceleration / _maxValue;
        _rotationSpeedBar.fillAmount = ufoData.rotationSpeed / _maxValue;
    }
}
