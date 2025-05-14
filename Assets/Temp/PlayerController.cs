using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LJ2_Test
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] PlayerMovement movement;
        [SerializeField] PlayerStatus status;

        private void Update()
        {
            MoveTest();
        }
        public void MoveTest()
        {
            Vector3 camRotateDir = movement.SetAimRotate();

            float moveSpeed;
            if (status.IsAiming.Value)
            {
                moveSpeed = status.walkSpeed;
            }
            else
            {
                moveSpeed = status.runSpeed;
            }

            Vector3 moveDir = movement.SetMove(moveSpeed);
            status.IsMoving.Value = (moveDir != Vector3.zero);


        }
    }
}
