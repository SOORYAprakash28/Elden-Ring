using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace SP
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera Instance;
        public Camera CameraObject;
        public PlayerManager player;
        [SerializeField]
        Transform cameraPivotTransform;

        [Header("Camera Settings")]
        private float _cameraSmoothSpeed = 1f;
        [SerializeField] float _laftAndRightRotationSpeed = 220f;
        [SerializeField] float _upAndDownRotationSpeed = 220f;
        [SerializeField] float _minimumPivote = -30f; // highest point u can look down
        [SerializeField] float _maximumPivote = 60f; // highest point u can loon up
        [SerializeField] float _cameraCollisionRadius = 0.2f;
        [SerializeField] LayerMask _collideWithLayers;

        [Header("Camera Values")]
        private Vector3 _cameraVelocity;
        private Vector3 _cameraObjectPosition;
        [SerializeField] float _leftAndRightLookAngle;
        [SerializeField] float _upAndDownLookAngle;
        private float _cameraZPosition;
        private float _targetCameraPosition;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            DontDestroyOnLoad(gameObject);
            _cameraZPosition = CameraObject.transform.localPosition.z;
        }
        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleFollowTarget();
                HandleRotation();
                HandleCollision();
            }
        }
        private void HandleFollowTarget()
        {
            Vector3 tragetCameraPostion = Vector3.SmoothDamp(transform.position,
            player.transform.position, ref _cameraVelocity, _cameraSmoothSpeed * Time.deltaTime);

            transform.position = tragetCameraPostion;
        }
        private void HandleRotation()
        {
            _leftAndRightLookAngle += PlayerInputManager.Instance.CameraHorizontalInput * _laftAndRightRotationSpeed * Time.deltaTime;
            _upAndDownLookAngle -= PlayerInputManager.Instance.CameraVerticleInput * _upAndDownRotationSpeed * Time.deltaTime;
            _upAndDownLookAngle = Mathf.Clamp(_upAndDownLookAngle, _minimumPivote, _maximumPivote);

            Quaternion targetRotation;
            Vector3 rotation = Vector3.zero;

            //Left and Right Rotation
            rotation.y = _leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform.rotation = targetRotation;

            //Up and Down Rotation
            rotation = Vector3.zero;
            rotation.x = _upAndDownLookAngle;
            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollision()
        {
            _targetCameraPosition = _cameraZPosition;
            RaycastHit hitInfo;
            // we cars a ray in the camera direction
            Vector3 direction = CameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            // is someting is colliding with the camera
            if (Physics.SphereCast(cameraPivotTransform.position, _cameraCollisionRadius, direction, out hitInfo, Mathf.Abs(_targetCameraPosition), _collideWithLayers))
            {
                float distanceFromHit = Vector3.Distance(cameraPivotTransform.position, hitInfo.point);
                _targetCameraPosition = -(distanceFromHit - _cameraCollisionRadius);

                // if the target camera position is less than the camera collision radius
                if (Mathf.Abs(_targetCameraPosition) < _cameraCollisionRadius)
                {
                    _targetCameraPosition = -_cameraCollisionRadius;
                }
                _cameraObjectPosition.z = Mathf.Lerp(CameraObject.transform.localPosition.z, _targetCameraPosition, 0.2f);

                CameraObject.transform.localPosition = _cameraObjectPosition;
            }
        }
    }
}