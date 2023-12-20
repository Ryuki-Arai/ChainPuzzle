using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame
{
    public class MainPrecenter : SingletonMonoBehaviour<MainPrecenter>
    {
        [field: SerializeField] public MainView View { get; set; }
        public MainModel Model { get; set; }


        protected override void Awake()
        {
            Initialized();
        }

        private void Initialized()
        {

            Model = new MainModel();
            View.Initialize();
        }
    }
}
