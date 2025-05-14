using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJ2_Test
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerMovement _movement;
        [SerializeField] PlayerStatus _status;

        private void Update()
        {
            MoveTest();

            _status.IsAiming.Value = Input.GetKey(KeyCode.Mouse1);
        }
        public void MoveTest()
        {
            Vector3 camRotateDir = _movement.SetAimRotate();

            float moveSpeed;
            if (_status.IsAiming.Value)
            {
                moveSpeed = _status.walkSpeed;
            }
            else
            {
                moveSpeed = _status.runSpeed;
            }

            Vector3 moveDir = _movement.SetMove(moveSpeed);
            _status.IsMoving.Value = (moveDir != Vector3.zero);

            Vector3 avatarDir;
            if (_status.IsAiming.Value) avatarDir = camRotateDir;
            else avatarDir = moveDir;

            _movement.SetBodyRotate(avatarDir);
        }
    }
}
