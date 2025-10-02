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
            playerLocomotionManager.HandelAllMovement();
        }
    }
}