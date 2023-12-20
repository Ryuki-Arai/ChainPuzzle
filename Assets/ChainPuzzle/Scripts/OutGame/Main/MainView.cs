using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class MainView : MonoBehaviour
    {
        [SerializeField] private HomeScreenPrecenter homeScreen;

        public void Initialize()
        {
            homeScreen.OnInitialized();
        }
    }

}