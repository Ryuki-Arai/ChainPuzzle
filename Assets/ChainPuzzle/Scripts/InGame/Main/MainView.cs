using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InGame
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] PlayerPrecenter player;
        [SerializeField] PieceControlFactory factory;
        [SerializeField] ClearDialog clearDialog;
        [SerializeField] Button exitButton;

        public void SetUp()
        {
            factory.OnInitialized();
            player.OnInitialized(factory);
            clearDialog.SetUp();
            exitButton.onClick.AddListener(() =>
            {
                SceneLoader.Instance.ChangeScene(SceneName.Home);
            });
        }

        public void OnUpdate()
        {
            player.OnUpdate();
            factory.OnUpdate();
        }

        public void OnGameClear()
        {
            clearDialog.ShowDialog();
        }

        public void OnGameOver()
        {
            Debug.Log("GameOver");
        }
    }
}
