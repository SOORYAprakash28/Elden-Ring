using UnityEngine;
namespace SP
{
    public class CharacterManager : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}