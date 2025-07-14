using Controller.Constant;
using UnityEngine;
using View.UI.Signal;

namespace Controller
{
    public class GameController : MonoBehaviour
    {
        void Start()
        {
            SignalBus.Instance.Subscribe<GameStartSignal>(OnGameStartSignal);
        }

        private void OnGameStartSignal()
        {
            ServiceLocator.Resolve<SceneController>().UnloadScene(SceneConstants.MainMenuScene);
            ServiceLocator.Resolve<SceneController>().LoadSceneAdditive(SceneConstants.CardGameScene);
        }
    }
}
