using SP;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;
    InputSystem_Actions inputActions;
    [SerializeField] Vector2 moveInput;
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
            inputActions.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();
        }
        inputActions.Enable();
    }
    void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
