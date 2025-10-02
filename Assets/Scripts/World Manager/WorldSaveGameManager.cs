using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SP
{
    public class WorldSaveGameManager : MonoBehaviour
    {
        public static WorldSaveGameManager Instance;
        private int _gameSceneIndex = 1;
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
            SceneManager.LoadScene(_gameSceneIndex);
            yield return null;
        }

        public int GetGameSceneIndex()
        {
            return _gameSceneIndex;
        }
    }
}
