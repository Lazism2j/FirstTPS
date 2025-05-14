using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform _avatar;
    [SerializeField] private Transform _aim;

    private Rigidbody _rigidbody;
    private PlayerStatus _playerStatus;

    [Header("Mouse Config")]
    [SerializeField][Range(-90, 0)] private float _minPitch;
    [SerializeField][Range(0, 90)] private float _maxPitch;
    [SerializeField][Range(0, 5)] private float _mouseSensitivity = 1;

    private Vector2 _currentRotation;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
        _playerStatus = GetComponent<PlayerStatus>();
    }

    public Vector3 SetMove(float moveSpeed)
    {
        Vector3 moveDirection = GetMoveDirection();

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        _rigidbody.velocity = velocity;

        return moveDirection;
    }

    public Vector3 SetAimRotate()
    {
        Vector2 mouseDir = GetMouseDirection();

        /*Vector2 currentRotation = new()
        {
            x = transform.rotation.eulerAngles.x,
            y = transform.rotation.eulerAngles.y
        };*/
        
        // y축에 각도 제한을 두어야 함
        _currentRotation.x += mouseDir.x;
        _currentRotation.y = Mathf.Clamp(
            _currentRotation.y + mouseDir.y,
            _minPitch,
            _maxPitch
            );

        transform.rotation = Quaternion.Euler(0, _currentRotation.x, 0);

        Vector3 currentEuler = _aim.localEulerAngles;
        _aim.localEulerAngles = new Vector3(_currentRotation.y, currentEuler.y, currentEuler.z);

        Vector3 rotateDirVector = transform.forward;
        rotateDirVector.y = 0;

        return rotateDirVector.normalized;
    }

    public void SetBodyRotate()
    {

    }



    public Vector3 GetInputDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z);
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 input = GetInputDirection();

        Vector3 Direction =
           (transform.right * input.x) + 
           (transform.forward * input.z);

        return Direction.normalized;
    }

    private Vector2 GetMouseDirection()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = -Input.GetAxis("Mouse Y") * _mouseSensitivity;

        return new Vector2(mouseX, mouseY);
    }
}
