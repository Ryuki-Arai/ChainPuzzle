using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearDialog : MonoBehaviour
{
    [SerializeField] Button button;

    public void SetUp()
    {
        gameObject.SetActive(false);
        if(button != null)
        {
            OnClickButton();
        }
    }

    public void ShowDialog()
    {
        gameObject.SetActive(true);
    }

    private void OnClickButton()
    {
        button.onClick.AddListener(() =>
        {
            DataSaveUtility.I.Data.PlayerData.LevelUp();
            SceneLoader.Instance.ChangeScene(SceneName.Home);
        });
    }
}
