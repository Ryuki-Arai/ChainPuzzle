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

        model = new MainModel();
    }

    private void OnUpdate()
    {
        this.UpdateAsObservable()
            .Subscribe(_ => Debug.Log("Update!"));
    }
}
