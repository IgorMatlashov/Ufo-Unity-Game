using UnityEngine;

public class UfoInstantiate : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Ufo _ufoData;

    private GameObject _player;
    private Rigidbody _playerRB;


    private Vector3 _spawnPoint;

    private void Awake()
    {
        _spawnPoint = transform.position;
        
        _gameManager = GameManager.Instance;
        if (_gameManager != null) _ufoData = _gameManager.selectedUfo;

        _player = Instantiate(_ufoData.gameObjectModel, _spawnPoint, Quaternion.identity);

        _player.tag = "Player";

        _playerRB = _player.AddComponent<Rigidbody>();

        _playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

        _playerRB.constraints |= RigidbodyConstraints.FreezePositionZ;

        UfoController(_player, _ufoData);
        UfoHeealth(_player, _ufoData);
        BeamController(_player, _ufoData);
        ArcRotation(_player, _ufoData);

        SetCamera();
    }
    private void UfoController(GameObject player, Ufo ufo)
    {
        UfoController controller = player.AddComponent<UfoController>();
        controller.maxSpeed = ufo.maxSpeed;
        controller.acceleration = ufo.acceleration;
        controller.rotationSpeed = ufo.rotationSpeed;
    }

    private void UfoHeealth(GameObject player, Ufo ufo)
    {
        UfoHealth health = player.AddComponent<UfoHealth>();
        health.health = ufo.health;
    }
    private void BeamController(GameObject player, Ufo ufo)
    {
        BeamController beam = player.AddComponent<BeamController>();
        beam.beamLanght = ufo.beamLanght;
        beam.beamWidth = ufo.beamWidth;
        beam.beamSmoothTime = ufo.beamSmoothTime;

    }

    private void ArcRotation(GameObject player, Ufo ufo)
    {
        ArcAnimation arc = player.AddComponent<ArcAnimation>();
        arc.rotationSpeed = ufo.arcSpeedRotation;
    }

    private void SetCamera()
    {
        Camera camera = FindObjectOfType<Camera>();
        camera._ufoTransform = _player.transform;
    }
}

