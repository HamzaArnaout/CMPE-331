using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    private PlayerControls playerControls;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerShotThisFrame()
    {
        return playerControls.Player.Shoot.triggered;
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerInteractedThisFrame()
    {
        return playerControls.Player.Interact.triggered;
    }

    public bool PlayerActivatedFlashlightThisFrame()
    {
        return playerControls.Player.Flashlight.triggered;
    }

    public bool PlayerReloadedThisFrame()
    {
        return playerControls.Player.Reload.triggered;
    }

    public Vector2 PlayerScrolledThisFrame()
    {
        return playerControls.Player.Scroll.ReadValue<Vector2>();
    }
}
