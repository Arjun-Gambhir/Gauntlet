using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;


public class PlayerInputManager : MonoBehaviour
{
    [SerializeField]
    private Players playerRef;
    private PlayerInput playerInput;

    [SerializeField]
    private GameObject inputParent;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var players = FindObjectsOfType<Players>();
        var index = playerInput.playerIndex;
        playerRef = players.FirstOrDefault(m => m.GetPlayerIndex() == index);
        inputParent = GameObject.FindGameObjectWithTag("InputManager");
        gameObject.transform.SetParent(inputParent.transform);
    }

    public void OnMove(CallbackContext context)
    {
        if (playerRef != null)
        {
            playerRef.SetMoveDirection(context.ReadValue<Vector2>());
        }
    }

    public void OnAttack(CallbackContext context)
    {
        if (playerRef != null)
        {
            playerRef.Attack();
        }
    }

    public void OnPotion(CallbackContext context)
    {
        if (playerRef != null)
        {
            playerRef.UsePotion();
        }
    }
}
