using System.Collections.Generic;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField] private float _cuttoffHight = 0f;
    [SerializeField] private float _cuttoffSpeed = 1f;
    private bool _isBeamOn = false;

    [HideInInspector] public float beamWidth;
    [HideInInspector] public float beamLanght;
    [HideInInspector] public float beamSmoothTime;

    private Vector3 _currentVelocity;
    private Material _material;

    private List<Rigidbody> _grabObjects = new List<Rigidbody>();
    private GameObject _beam;
    private Transform _point;

    private void Start()
    {
        _point = transform.Find("Point");
        _beam = GameObject.Find("Beam");

        _beam.transform.localScale = new Vector3(beamWidth, beamLanght, beamWidth);

        _material = _beam.GetComponent<Renderer>().material;
        _material.SetFloat("_CutoffHight", _cuttoffHight);
    }

    private void Update()
    {
        ReleaseBeam();

        if (Input.GetMouseButton(0))
        {
            _isBeamOn = true;
            if (_cuttoffHight >= 1)
            {
                foreach (Rigidbody obj in _grabObjects)
                {
                    _currentVelocity = obj.velocity;
                    obj.transform.position = Vector3.SmoothDamp(obj.transform.position, _point.transform.position, ref _currentVelocity, Time.deltaTime * beamSmoothTime);
                    obj.velocity = _currentVelocity;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _isBeamOn = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            Rigidbody obj = other.GetComponent<Rigidbody>();
            _grabObjects.Add(obj);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            Rigidbody obj = other.GetComponent<Rigidbody>();
            _grabObjects.Remove(obj);
        }
    }

    private void ReleaseBeam()
    {
        if (_isBeamOn) _cuttoffHight = Mathf.MoveTowards(_cuttoffHight, 1, _cuttoffSpeed * Time.deltaTime);
        else _cuttoffHight = Mathf.MoveTowards(_cuttoffHight, 0, _cuttoffSpeed * Time.deltaTime);

        _material.SetFloat("_CutoffHight", _cuttoffHight);
    }

    private void MeshSwitch()
    {
        if (_cuttoffHight != 0) _beam.SetActive(true);
        else _beam.SetActive(false);
    }
}
