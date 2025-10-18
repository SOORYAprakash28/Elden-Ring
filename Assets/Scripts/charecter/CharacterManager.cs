using Unity.Netcode;
using UnityEngine;
namespace SP
{
    public class CharacterManager : NetworkBehaviour
    {
        [HideInInspector] public CharacterNetworkManager characterNetworkManager;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterController characterController;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            characterNetworkManager = GetComponent<CharacterNetworkManager>();
        }
        protected virtual void Update()
        {
            if (IsOwner)
            {
                characterNetworkManager.NetworkPosition.Value = transform.position;
                characterNetworkManager.NetworkRotation.Value = transform.rotation;
            }
            else
            {
                // position for the network
                transform.position = Vector3.SmoothDamp(transform.position,
                characterNetworkManager.NetworkPosition.Value, ref characterNetworkManager.NetworkPositionVelocity,
                characterNetworkManager.NetworkPositionSmoothTime);

                // rotation for the network
                transform.rotation = Quaternion.Slerp(transform.rotation,
                characterNetworkManager.NetworkRotation.Value, characterNetworkManager.NetworkRotationSmoothTime);
            }
        }
        protected virtual void LateUpdate()
        {

        }
    }

}