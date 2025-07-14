using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadScene(string sceneName, System.Action<float> onProgress = null, System.Action onComplete = null)
        {
            StartCoroutine(LoadSceneRoutine(sceneName, onProgress, onComplete));
        }

        private IEnumerator LoadSceneRoutine(string sceneName, System.Action<float> onProgress,
            System.Action onComplete)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                onProgress?.Invoke(operation.progress);
                yield return null;
            }

            onProgress?.Invoke(1f);
            operation.allowSceneActivation = true;

            while (!operation.isDone)
                yield return null;

            onComplete?.Invoke();
        }

        /// <summary>
        /// Loads a scene additively (adds on top of current scenes).
        /// </summary>
        public void LoadSceneAdditive(string sceneName, System.Action<float> onProgress = null,
            System.Action onComplete = null)
        {
            StartCoroutine(LoadAdditiveRoutine(sceneName, onProgress, onComplete));
        }

        private IEnumerator LoadAdditiveRoutine(string sceneName, System.Action<float> onProgress,
            System.Action onComplete)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            operation.allowSceneActivation = false;

            while (operation.progress < 0.9f)
            {
                onProgress?.Invoke(operation.progress);
                yield return null;
            }

            onProgress?.Invoke(1f);
            operation.allowSceneActivation = true;

            while (!operation.isDone)
                yield return null;

            onComplete?.Invoke();
        }

        /// <summary>
        /// Unloads a scene.
        /// </summary>
        public void UnloadScene(string sceneName, System.Action onComplete = null)
        {
            StartCoroutine(UnloadRoutine(sceneName, onComplete));
        }

        private IEnumerator UnloadRoutine(string sceneName, System.Action onComplete)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

            while (!operation.isDone)
                yield return null;

            onComplete?.Invoke();
        }
    }
}