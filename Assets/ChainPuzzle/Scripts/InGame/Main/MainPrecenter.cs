using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace InGame
{
    public class MainPrecenter : SingletonMonoBehaviour<MainPrecenter>
    {
        [SerializeField] private MainView view;
        private MainModel model;

        protected override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            DataSaveUtility.I.GetData();
            DataManager.Instance.OnSetUp(); 

            view.SetUp();
            model = new MainModel();
            this.UpdateAsObservable()
                .Subscribe(_ => OnUpdate());
        }

        private void OnUpdate()
        {
            view.OnUpdate();
        }

        public void OnClear()
        {
            view.OnGameClear();
        }

        public void OnFaild()
        {
            view.OnGameOver();
        }
    }
}