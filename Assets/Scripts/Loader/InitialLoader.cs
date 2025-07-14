using Managers;
using UnityEngine;

namespace Loader
{
    public class InitialLoader : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            ServiceLocator.Register(gameObject.AddComponent<GameManager>());
            ServiceLocator.Register(gameObject.AddComponent<SceneManager>());

            var loaderViewController = ServiceLocator.Resolve<LoaderViewController>();
            loaderViewController.EnableView();
            ServiceLocator.Resolve<SceneManager>().LoadScene(SceneConstants.MainMenuScene, onComplete: loaderViewController.OnLoadFinished);
        }
    }
}
