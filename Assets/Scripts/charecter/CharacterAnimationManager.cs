using UnityEngine;

namespace SP
{

    public class CharacterAnimationManager : MonoBehaviour
    {
        CharacterManager character;
        float horizontalValue, verticleValue;
        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void UpdateAnimationMovementParameter(float horizontalValue, float verticleValue)
        {
            character.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", verticleValue, 0.1f, Time.deltaTime);
        }
    }
}
