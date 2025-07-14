using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class LoaderViewController : MonoBehaviour
{
    [SerializeField] private Transform canvasRoot;
    [SerializeField] private Transform viewRoot;
    [SerializeField] private CanvasGroup canvasGroup;

    private void Awake()
    {
        ServiceLocator.Register(this);
        DontDestroyOnLoad(canvasRoot);
    }

    public void EnableView()
    {
        canvasGroup.alpha = 1f;
        viewRoot.gameObject.SetActive(true);
    }

    public void OnLoadFinished()
    {
        canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            viewRoot.gameObject.SetActive(false);
        });
    }
}
