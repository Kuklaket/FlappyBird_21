using UnityEngine;
using UnityEngine.Events;

public class InputReader : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string Attack = "Attack";

    public event UnityAction JumpPressed;
    public event UnityAction AttackPressed;

    private void Update()
    {
        HandleJumpInput();
        HandleAttackInput();
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown(Jump))
            JumpPressed?.Invoke();
    }

    private void HandleAttackInput()
    {
        if (Input.GetButtonDown(Attack))
            AttackPressed?.Invoke();
    }
}
