using Managers;
using UnityEngine;
using View.Factory;

namespace Loader
{
    public class InitialLoader : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);

            Application.targetFrameRate = 60;
            
            ServiceLocator.Register(gameObject.AddComponent<GameManager>());
            ServiceLocator.Register(gameObject.AddComponent<SceneManager>());
            ServiceLocator.Register(gameObject.AddComponent<CardViewFactory>());
            ServiceLocator.Register(gameObject.AddComponent<AssetBundleManager>());

            var loaderViewController = ServiceLocator.Resolve<LoaderViewController>();
            loaderViewController.EnableView();
            ServiceLocator.Resolve<SceneManager>().LoadScene(SceneConstants.MainMenuScene, onComplete: loaderViewController.OnLoadFinished);
        }
    }
}
