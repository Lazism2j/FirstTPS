using System.Collections;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: SerializeField][field: Range(0, 10)]
    public float walkSpeed { get; set; }

    [field: SerializeField][field: Range (0, 10)]
    public float runSpeed { get; set; }

    [field: SerializeField][field: Range(0, 10)]
    public float rotateSpeed { get; set; }

    public ObservableProperty<bool> IsAiming { get; private set; } = new();
    public ObservableProperty<bool> IsMoving { get; private set; } = new();
    public ObservableProperty<bool> IsAttacking { get; private set; } = new();
}
