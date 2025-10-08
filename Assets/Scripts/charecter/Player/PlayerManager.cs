using UnityEngine;
namespace SP
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            base.Awake();
            // this can have the player specific logic
            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }
        protected override void Update()
        {
            base.Update();
            if (!IsOwner)
                return;

            playerLocomotionManager.HandelAllMovement();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;

            base.LateUpdate();
            PlayerCamera.Instance.HandleAllCameraActions();
        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner)
            {
                PlayerCamera.Instance.player = this;
            }
        }
    }
}