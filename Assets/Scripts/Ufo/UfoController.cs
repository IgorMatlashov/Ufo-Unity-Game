using UnityEngine;

public class UfoController : MonoBehaviour
{
    private LayerMask _layerMask;

    [HideInInspector] public float maxSpeed;
    [HideInInspector] public float acceleration;
    [HideInInspector] public float rotationSpeed;

    private Rigidbody _ufoRigidbody;

    private float raycastDistance = 4f;
    private bool _isFalling;


    private void Awake()
    {
        _layerMask = LayerMask.GetMask("Ground");
        _ufoRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsGround() && !CheckFalling())
        {
            _ufoRigidbody.velocity = Vector3.ClampMagnitude(_ufoRigidbody.velocity, maxSpeed);
        }
        Controller();
    }

    private void Controller()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _ufoRigidbody.AddRelativeForce(Vector3.up * acceleration, ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.D))
        {
            _ufoRigidbody.AddRelativeTorque(Vector3.back * rotationSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _ufoRigidbody.AddRelativeTorque(Vector3.forward * rotationSpeed, ForceMode.Force);
        }
    }

    private bool IsGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, raycastDistance, _layerMask))
        {
            return false;
        }
        return true;
    }

    private bool CheckFalling()
    {
        _isFalling = Vector3.Dot(_ufoRigidbody.velocity, Vector3.down) > 0;
        return _isFalling;
    }
}
