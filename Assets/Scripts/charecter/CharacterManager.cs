using Unity.Netcode;
using UnityEngine;
namespace SP
{
    public class CharacterManager : NetworkBehaviour
    {
        public CharacterNetworkManager characterNetworkManager;
        public CharacterController characterController;
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
            characterController = GetComponent<CharacterController>();
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