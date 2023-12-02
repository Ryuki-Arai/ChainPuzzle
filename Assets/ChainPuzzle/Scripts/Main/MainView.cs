using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] PlayerPrecenter player;
        [SerializeField] PieceControlFactory factory;
        [SerializeField] ClearDialog clearDialog;

        public void SetUp()
        {
            player.OnInitialized();
            factory.OnInitialized();
            clearDialog.SetUp();
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
