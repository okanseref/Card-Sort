using UnityEngine;
using UnityEngine.UI;
using View.UI.Signal;

namespace View.UI
{
    public class MainSceneUIController : MonoBehaviour
    {
        public Button StartButton;

        void Start()
        {
            StartButton.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            SignalBus.Instance.Fire<GameStartSignal>();
        }
    }
}
