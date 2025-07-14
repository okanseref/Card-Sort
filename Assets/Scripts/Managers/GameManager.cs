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
            var loaderViewController = ServiceLocator.Resolve<LoaderViewController>();
            loaderViewController.EnableView();
            ServiceLocator.Resolve<SceneManager>().LoadScene(SceneConstants.CardGameScene, onComplete: loaderViewController.OnLoadFinished);
        }
    }
}
