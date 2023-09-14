using UnityEngine;

public class HatchController : MonoBehaviour
{
    [SerializeField] private float _closingSpeed;

    private bool _isClose;

    private void Update()
    {
        if (_isClose)
        {
            Transform leftDoor = transform.GetChild(0);
            Transform rightDoor = transform.GetChild(1);

            leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, new Vector3(-15f, 0, 0), Time.deltaTime * _closingSpeed);
            rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, new Vector3(15f, 0, 0), Time.deltaTime * _closingSpeed);
        }
    }
    public void Close()
    {
        _isClose = true;
    }

}
