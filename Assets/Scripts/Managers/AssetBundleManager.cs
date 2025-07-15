using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    public class AssetBundleManager : MonoBehaviour
    {
        private AssetBundle _assetBundle;

        private void Start()
        {
            StartCoroutine(LoadBundle(AssetBundleConstants.PixelatedCardsBundleName, null));
        }
        
        private IEnumerator LoadBundle(string bundleName, Action onSuccess)
        {
            var bundleRequest = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + bundleName);
        
            yield return bundleRequest;
        
            if (bundleRequest.assetBundle == null)
            {
                yield break;
            }
        
            TryUnloadBundle();
            _assetBundle = bundleRequest.assetBundle;
            onSuccess?.Invoke();
        }
        
        private void TryUnloadBundle()
        {
            if(_assetBundle != null)
                _assetBundle.Unload(true);
        }
        
        public T LoadAssetFromBundle<T>(string assetName) where T : Object
        {
            T loadedAsset = _assetBundle.LoadAsset<T>(assetName);
        
            if (loadedAsset == null)
            {
                Debug.LogError($"Failed to load asset: {assetName}");
                return null;
            }
        
            return loadedAsset;
        }
        
        private void LoadAssetBundle(string bundleName)
        {
            StartCoroutine(LoadBundle(bundleName, () =>
            {
        
            }));
        }
    }
}