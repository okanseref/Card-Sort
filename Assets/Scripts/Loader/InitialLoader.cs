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
            
            ServiceLocator.Resolve<SceneManager>().LoadSceneAdditive(SceneConstants.MainMenuScene);
        }
    }
}
