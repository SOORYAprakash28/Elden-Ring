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
    void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }
    public void HandelAllMovement()
    {
        PlayerGroundedMovement();
        HandelRotation();
    }
    private void GetHorizantalAndVerticalMovement()
    {
        HorizantalMovement = PlayerInputManager.instance.horizontalInput;
        VerticleMovement = PlayerInputManager.instance.verticleInput;
    }
    public void PlayerGroundedMovement()
    {
        // move direction is based on camera
        GetHorizantalAndVerticalMovement();
        moveDirection = PlayerCamera.Instance.transform.forward * VerticleMovement;
        moveDirection += PlayerCamera.Instance.transform.right * HorizantalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0f;

        if (PlayerInputManager.instance.moveAmount > 0.5f)
        {
            // Moving in a Running speed
            player.characterController.Move(moveDirection * _runningSpeed * Time.deltaTime);
        }
        else if (PlayerInputManager.instance.moveAmount <= 0.5f)
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
