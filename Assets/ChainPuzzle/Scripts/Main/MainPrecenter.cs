using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

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
        view.SetUp();
        model = new MainModel();
        this.UpdateAsObservable()
            .Subscribe(_ => OnUpdate());
    }

    private void OnUpdate()
    {
        view.OnUpdate();
    }
}
