using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting.AssemblyQualifiedNameParser;

public class CinemachinePOVExtension : CinemachineExtension
{
    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 10f;
    [SerializeField]
    private float clampAngle = 80f;

    public InputManager inputManager;
    private Vector3 startingRotation;

    public bool canLook = true;

    protected override void Awake()
    {
        //inputManager = InputManager.Instance;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    { 
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                // Get the starting rotation so it doesnt mess up calculations later
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;

                if (canLook)
                {
                    Vector2 deltaInput = inputManager.GetMouseDelta(); // Get the input from the player

                    // Rotate the player's camera according to input
                    startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                    startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                }
                // Clamping the Vertical rotaiton of the camera so the player cant do 360s vertically
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
                
            }
        }
    }
}
