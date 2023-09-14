using UnityEngine;

[CreateAssetMenu(fileName = "New Ufo" ,menuName = "ScriptableObject/Ufo")]
public class Ufo : ScriptableObject
{
    [Header("3D Model")]
    public GameObject gameObjectModel;

    [Header("Parameters for Controller")]
    public string modelName;
    public int health;
    public float maxSpeed; 
    public float acceleration;
    public float rotationSpeed;

    [Header("Parameters for Beam")]
    public float beamWidth;
    public float beamLanght;
    public float beamSmoothTime;

    [Header("Arc Speed Rotation")]
    public float arcSpeedRotation;
}
