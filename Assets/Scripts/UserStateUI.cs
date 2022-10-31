using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserStateUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI CoinsLabel;

    [SerializeField]
    TextMeshProUGUI LevelLabel;

    void Start()
    {
        DemoCore.OnUserDataChanged.AddListener(RefreshStateUI);
        RefreshStateUI();
    }

    public void RefreshStateUI()
    {
        CoinsLabel.text = DemoCore.User.Coins.ToString("N0");
        LevelLabel.text = DemoCore.User.Level.ToString("N0");
    }
}
