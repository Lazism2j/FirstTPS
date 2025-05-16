using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Gun : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField][Range(0, 100)] private float _attackRange;
    [SerializeField] private int _shootDamage;
    [SerializeField] private float _shootDelay;
    [SerializeField] private AudioClip _shootSFX;

    private CinemachineImpulseSource _impulse;
    private Camera _camera;

    private bool _canShoot { get => _currentCount <= 0; }
    private float _currentCount;

    private void Awake() => Init(); 
    private void Init()
    {
        _camera = Camera.main;
        _impulse = new CinemachineImpulseSource();
    }
    private void Update()
    {
        HandleCanShoot();
    }

    private void HandleCanShoot()
    {
        if (_canShoot) return;

        _currentCount -= Time.deltaTime;
    }
    public bool Shoot()
    {
        if (!_canShoot) return false;

        PlayCameraEffect();
        PlayShootEffect();
        PlayShootSound();

        _currentCount = _shootDelay;

        GameObject target = RayShoot();
        if (target == null) return true;

        Debug.Log($"{target.name} 피격");
        return true;    
        
    }

    private GameObject RayShoot()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,_attackRange, _targetLayer))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    private void PlayShootSound()
    {
        SFXController sfx = GameManager.instance.Audio.GetSFX();
        sfx.Play(_shootSFX);
    }

    private void PlayCameraEffect()
    {
        _impulse.GenerateImpulse();
    }

    private void PlayShootEffect()
    {
        // TODO 파티클로 구현
    }
}
