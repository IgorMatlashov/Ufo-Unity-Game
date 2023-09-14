using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animation _doorAnimation;
    private string _animationStateName = "OpenDoor";

    private void Start()
    {
        _doorAnimation = GetComponent<Animation>();
    }

    public void OpenDoor()
    {
        _doorAnimation[_animationStateName].speed = 1f;
        _doorAnimation.Play(_animationStateName);
    }

    public void CloseDoor()
    {
        if (!_doorAnimation.IsPlaying(_animationStateName))
        {
            _doorAnimation[_animationStateName].time = _doorAnimation.clip.length;
        }
        _doorAnimation[_animationStateName].speed = -1f;
        _doorAnimation.Play(_animationStateName);
    }
}