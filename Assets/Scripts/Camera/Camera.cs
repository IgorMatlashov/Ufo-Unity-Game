using UnityEngine;

public class Camera : MonoBehaviour
{
    private Quaternion _cameraRotation;

    public Transform _ufoTransform;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private float _cameraHight;
    private void Awake()
    {
        _cameraRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        SetCameraPosition();
        transform.rotation = _cameraRotation;
        transform.LookAt(_ufoTransform.position);
    }

    private void SetCameraPosition()
    {
        transform.position = new Vector3(_ufoTransform.position.x, _ufoTransform.position.y + _cameraHight, (_ufoTransform.position.z + _cameraDistance) * -1);
    }
}
