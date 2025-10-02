using UnityEngine;
namespace SP
{
    public class CharacterManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        protected virtual void Update()
        {

        }
    }
}