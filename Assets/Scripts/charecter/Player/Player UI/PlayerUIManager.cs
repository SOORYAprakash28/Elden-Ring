using Unity.Netcode;
using UnityEngine;
namespace SP
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager Instance;
        [SerializeField]
        private bool _startGameasClient = false;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Update()
        {
            if (_startGameasClient)
            {
                _startGameasClient = false;
                NetworkManager.Singleton.Shutdown();
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
