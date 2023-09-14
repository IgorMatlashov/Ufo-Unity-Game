using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    [SerializeField] private Material _meshButton;

    public UnityEvent onPress;
    public UnityEvent onRelease;

    private bool _isPressed;

    private void Start()
    {
        _isPressed = false;

        _meshButton = _button.GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!_isPressed && other.CompareTag("Cube"))
        {
            _isPressed = true;
            _meshButton.SetColor("_EmissionColor", Color.green);
            _button.transform.DOLocalMoveY(0.2f, 0.5f);
            onPress.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_isPressed && other.CompareTag("Cube"))
        {
            _isPressed = false;
            _meshButton.SetColor("_EmissionColor", Color.red);
            _button.transform.DOLocalMoveY(1f, 0.5f);
            onRelease.Invoke();
        }
    }
}
