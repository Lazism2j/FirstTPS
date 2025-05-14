using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public bool IsControlActivate { get; set; } = true;
    [SerializeField] PlayerMovement _movement;
    [SerializeField] PlayerStatus _status;

    [SerializeField] private CinemachineVirtualCamera _aimCamera;

    [SerializeField] private KeyCode _aimKey = KeyCode.Mouse1;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void Update()
    {
        HandlePlayerCotrol();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
        _movement = GetComponent<PlayerMovement>();
    }

    private void HandlePlayerCotrol()
    {
        if (!IsControlActivate) return;

        HandleMovement();
        HandleAiming();
    }

    private void HandleMovement()
    {
        Vector3 camRotateDir = _movement.SetAimRotate();

        float moveSpeed;
        if (_status.IsAiming.Value) moveSpeed = _status.walkSpeed;
            
        else  moveSpeed = _status.runSpeed;
            

        Vector3 moveDir = _movement.SetMove(moveSpeed);
        _status.IsMoving.Value = (moveDir != Vector3.zero);

        Vector3 avatarDir;
        if (_status.IsAiming.Value) avatarDir = camRotateDir;
        else avatarDir = moveDir;

        _movement.SetBodyRotate(avatarDir);
    }
        
    private void HandleAiming()
    {
        _status.IsAiming.Value = Input.GetKey(_aimKey);
    }

    private void SubscribeEvents()
    {
        _status.IsAiming.Subscribe(_aimCamera.gameObject.SetActive);
    }

    private void UnsubscribeEvents()
    {
        _status.IsAiming.Unsubscribe(_aimCamera.gameObject.SetActive);
    }

}
