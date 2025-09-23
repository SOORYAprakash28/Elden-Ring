using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SP
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager Instance;

        void Awake()
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
        public IEnumerator LoadGameScene()
        {
            SceneManager.LoadScene(1);
            yield return null;
        }
    }
}
