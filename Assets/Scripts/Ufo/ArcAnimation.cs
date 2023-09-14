using UnityEngine;

public class ArcAnimation : MonoBehaviour
{
    private Transform _arc;
    [HideInInspector] public float rotationSpeed;

    private void Start()
    {
        _arc = transform.Find("Arc");
    }

    private void Update()
    {
        if (_arc != null)
        {
            _arc.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
