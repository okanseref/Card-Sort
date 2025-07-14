using UnityEngine;
using View.UI.Signal;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        void Start()
        {
            SignalBus.Instance.Subscribe<GameStartSignal>(OnGameStartSignal);
        }

        private void OnGameStartSignal()
        {
            ServiceLocator.Resolve<SceneManager>().UnloadScene(SceneConstants.MainMenuScene);
            ServiceLocator.Resolve<SceneManager>().LoadSceneAdditive(SceneConstants.CardGameScene);
        }
    }
}
