using Unity.Netcode;
using UnityEngine;
namespace SP
{
    public class TitleSceenManager : MonoBehaviour
    {
        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }
        public void StartLoadingGameScene()
        {
            StartCoroutine(WorldSaveGameManager.Instance.LoadGameScene());
        }
    }
}