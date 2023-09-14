using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UfoChanger : MonoBehaviour
{
    private GameManager _gameManager;
    public Ufo[] _ufoModelsPref;

    private List<GameObject> _ufoModelsSpawned = new List<GameObject>();
    private Animator _animatorPrefab;

    private int _currentUfoIndex = 0;
    private int _prevCurrentUfoIndex = 0;

    private int _toSelect;
    private int _outSelect;

    private bool _canSwitchUfo = true;

    private void Awake()
    {
        _animatorPrefab = GetComponent<Animator>();
        _toSelect = Animator.StringToHash("ToSelect");
        _outSelect = Animator.StringToHash("OutSelect");

        SpawnModels();
        CopyAnimation();
        

        _gameManager = GameManager.Instance;
    }

    private void Start()
    {
        SendUfoInfo(_currentUfoIndex);
    }

    private void Update()
    {
        if (_canSwitchUfo)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) NextUfoButton();
            if (Input.GetKeyDown(KeyCode.LeftArrow)) PrevUfoButton();
        }
    }

    private void ArcRotation(GameObject ufo, Ufo model)
    {
        ArcAnimation rotation = ufo.AddComponent<ArcAnimation>();
        rotation.rotationSpeed = model.arcSpeedRotation;
    }
    private void SpawnModels()
    {
        foreach (var modelPrefab in _ufoModelsPref)
        {
            GameObject ufo = Instantiate(modelPrefab.gameObjectModel, transform.position, Quaternion.identity, transform);
            ArcRotation(ufo, modelPrefab);
            _ufoModelsSpawned.Add(ufo);
        }
    }

    private void CopyAnimation()
    {
        RuntimeAnimatorController sourceController = _animatorPrefab.runtimeAnimatorController;

        foreach (GameObject obj in _ufoModelsSpawned)
        {
            Animator animation = obj.GetComponent<Animator>();

            if (animation == null)
            {
                Animator targetAnimator = obj.AddComponent<Animator>();
                targetAnimator.runtimeAnimatorController = sourceController;
            }
        }
    }

    public void NextUfoButton()
    {
        _prevCurrentUfoIndex = _currentUfoIndex;
        if (_canSwitchUfo) StartCoroutine(SwitchUfoWithDelay(_currentUfoIndex + 1));
        SendUfoInfo(_currentUfoIndex);
    }

    public void PrevUfoButton()
    {
        _prevCurrentUfoIndex = _currentUfoIndex;
        if (_canSwitchUfo) StartCoroutine(SwitchUfoWithDelay(_currentUfoIndex - 1));
        SendUfoInfo(_currentUfoIndex);
        
    }

    private void SwitchUfo(int newIndex)
    {
        if (newIndex < 0) newIndex = _ufoModelsSpawned.Count - 1;
        else if (newIndex >= _ufoModelsSpawned.Count) newIndex = 0;

        PlayAnimation(_ufoModelsSpawned[_currentUfoIndex], _outSelect);
        _currentUfoIndex = newIndex;
        PlayAnimation(_ufoModelsSpawned[_currentUfoIndex], _toSelect);
    }

    private void PlayAnimation(GameObject obj, int animationClipHashName)
    {
        Animator animator = obj.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play(animationClipHashName);
        }
    }

    private void SendUfoInfo(int Index)
    {
        EventManager.SendUfoInfo(_ufoModelsPref[Index]);
    }

    private IEnumerator SwitchUfoWithDelay(int newIndex)
    {
        _canSwitchUfo = false;
        SwitchUfo(newIndex);
        yield return new WaitForSeconds(1f);
        _canSwitchUfo = true;
    }

    private void OnEnable()
    {
        Animator animator = _ufoModelsSpawned[_prevCurrentUfoIndex].GetComponent<Animator>();

        if (animator != null)
        {
            animator.Play(_outSelect, 0, 1f);
        }

        StartCoroutine(SwitchUfoWithDelay(_currentUfoIndex));
    }

    public void SelectUfo() { _gameManager.selectedUfo = _ufoModelsPref[_currentUfoIndex]; }

}