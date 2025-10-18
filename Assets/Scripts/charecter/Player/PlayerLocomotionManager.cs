using SP;
using UnityEngine;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;
    public float VerticleMovement;
    public float HorizantalMovement;
    public float moveAmount;
    private Vector3 _tragetRotationDirection;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float _walkingSpeed = 2;
    [SerializeField] float _runningSpeed = 5;
    private Vector3 moveDirection;
    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }
    protected override void Update()
    {
        base.Update();
        if (player.IsOwner)
        {
            player.characterNetworkManager.HorizontalMovement.Value = HorizantalMovement;
            player.characterNetworkManager.VerticalMovement.Value = VerticleMovement;
            player.characterNetworkManager.MoveAmount.Value = moveAmount;
        }
        else
        {
            HorizantalMovement = player.characterNetworkManager.HorizontalMovement.Value;
            VerticleMovement = player.characterNetworkManager.VerticalMovement.Value;
            moveAmount = player.characterNetworkManager.MoveAmount.Value;

            player.playerAnimationManager.UpdateAnimationMovementParameter(0, moveAmount);
        }
    }
    public void HandelAllMovement()
    {
        PlayerGroundedMovement();
        HandelRotation();
    }
    private void GetMovementValues()
    {
        HorizantalMovement = PlayerInputManager.Instance.horizontalInput;
        VerticleMovement = PlayerInputManager.Instance.verticleInput;
        moveAmount = PlayerInputManager.Instance.moveAmount;
    }
    public void PlayerGroundedMovement()
    {
        // move direction is based on camera
        GetMovementValues();
        moveDirection = PlayerCamera.Instance.transform.forward * VerticleMovement;
        moveDirection += PlayerCamera.Instance.transform.right * HorizantalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        if (PlayerInputManager.Instance.moveAmount > 0.5f)
        {
            // Moving in a Running speed
            player.characterController.Move(moveDirection * _runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.Instance.moveAmount <= 0.5f)
        {
            // Moving in a walking speed
            player.characterController.Move(moveDirection * _walkingSpeed * Time.deltaTime);
        }
    }
    public void HandelRotation()
    {
        _tragetRotationDirection = Vector3.zero;
        _tragetRotationDirection = PlayerCamera.Instance.CameraObject.transform.forward * VerticleMovement;
        _tragetRotationDirection += PlayerCamera.Instance.CameraObject.transform.right * HorizantalMovement;
        _tragetRotationDirection.Normalize();
        _tragetRotationDirection.y = 0f;

        if (_tragetRotationDirection == Vector3.zero)
        {
            _tragetRotationDirection = transform.forward;
        }
        Quaternion newRotation = Quaternion.LookRotation(_tragetRotationDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
    }
}
