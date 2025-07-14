using Controller;
using Controller.Constant;
using UnityEngine;

namespace Loader
{
    public class InitialLoader : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            
            ServiceLocator.Register(gameObject.AddComponent<GameController>());
            ServiceLocator.Register(gameObject.AddComponent<SceneController>());
            
            ServiceLocator.Resolve<SceneController>().LoadSceneAdditive(SceneConstants.MainMenuScene);
        }
    }
}
