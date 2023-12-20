using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class HomeScreenPrecenter : MonoBehaviour
    {
        [SerializeField] private HomeScreenView view;
        private HomeScreenModel model;

        public void OnInitialized()
        {
            model = new HomeScreenModel();

            view.RegisterOnClickPlayButton(OnClickPlayButton);
            view.SetLevelText(model.Level);
        }

        private void OnClickPlayButton()
        {
            SceneLoader.Instance.ChangeScene(SceneName.InGame);
        }
    }
}
