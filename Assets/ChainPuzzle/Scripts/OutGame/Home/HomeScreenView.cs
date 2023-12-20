using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OutGame
{
    public class HomeScreenView : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Button playButton;

        public void RegisterOnClickPlayButton(Action onClickPlayButton)
        {
            playButton.onClick.AddListener(() => onClickPlayButton?.Invoke());
        }

        public void SetLevelText(int level)
        {
            levelText.text = $"{level}";
        }
    }
}
