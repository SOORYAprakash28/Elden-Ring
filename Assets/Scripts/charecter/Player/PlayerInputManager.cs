using SP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    InputSystem_Actions inputActions;
    [Header("Movement input")]
    [SerializeField] Vector2 moveInput;
    public float verticleInput;
    public float horizontalInput;
    [Header("Camera Input")]
    [SerializeField] Vector2 cameraInput;
    public float CameraVerticleInput;
    public float CameraHorizontalInput;
    public float moveAmount;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        instance.enabled = false;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        // inputs are only allowed in the game scene
        if (arg1.buildIndex == WorldSaveGameManager.Instance.GetGameSceneIndex())
        {
            instance.enabled = true;
        }
        else
        {
            instance.enabled = false;
        }
    }

    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
            // Subscribe to both performed and canceled events
            inputActions.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();
            inputActions.Player.Move.canceled += i => moveInput = Vector2.zero;

            inputActions.Player.Look.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.Player.Look.canceled += i => cameraInput = Vector2.zero;
        }
        inputActions.Enable();
    }

    void OnDisable()
    {
        // Unsubscribe from events to prevent memory leaks
        inputActions.Player.Move.performed -= i => moveInput = i.ReadValue<Vector2>();
        inputActions.Player.Move.canceled -= i => moveInput = Vector2.zero;

        inputActions.Player.Look.performed -= i => cameraInput = i.ReadValue<Vector2>();
        inputActions.Player.Look.canceled -= i => cameraInput = Vector2.zero;

        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    void Update()
    {
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
    }
    void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus)
            {
                inputActions.Enable();
            }
            else
            {
                inputActions.Disable();
            }
        }
    }
    private void HandlePlayerMovementInput()
    {
        verticleInput = moveInput.y;
        horizontalInput = moveInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticleInput) + Mathf.Abs(horizontalInput));

        if (moveAmount <= 0.5f && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5f && moveAmount <= 1)
        {
            moveAmount = 1f;
        }
    }
    private void HandleCameraMovementInput()
    {
        CameraVerticleInput = cameraInput.y;
        CameraHorizontalInput = cameraInput.x;
    }
}
