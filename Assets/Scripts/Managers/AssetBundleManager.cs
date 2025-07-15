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
            StartCoroutine(LoadBundle(AssetBundleConstants.DefaultBundleName, null));
        }
        
        private IEnumerator LoadBundle(string bundleName, Action onSuccess)
        {
            if(_assetBundle != null && _assetBundle.name.Equals(bundleName))
                yield break;
            
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
        
        public void LoadAssetBundle(string bundleName)
        {
            StartCoroutine(LoadBundle(bundleName, () =>
            {
                SignalBus.Instance.Fire(new BundleChangedSignal());
            }));
        }
    }
}