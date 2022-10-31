using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DemoCore : MonoBehaviour
{
    public static DemoCore Instance;

    public ItemDatabase Database;

    public static UserData User;

    public static UnityEvent OnUserDataChanged = new UnityEvent();

    void Awake()
    {
        Instance = this;
        Initialize();
    }

    void Start()
    {
        OnUserDataChanged?.AddListener(() => SaveState());
    }

    void SaveState()
    {
        PlayerPrefs.SetInt("Level", User.Level);
        PlayerPrefs.SetInt("Coins", User.Coins);

        foreach (Item item in User.ItemsOwned)
        {
            PlayerPrefs.SetString("Owned_" + item.name, item.name);
        }

        foreach (SkinSlotData skin in User.Avatar.SkinsSlots)
        {
            if(skin.Item == null) continue;

            PlayerPrefs.SetString("Avatar_" + skin.Key, skin.Item.name);
        }


        PlayerPrefs.Save();
    }
    
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        User = null;
        OnUserDataChanged.RemoveAllListeners();
        SceneManager.LoadScene(0);
    }

    void Initialize()
    {
        LoadExistingSave();
    }

    public void LoadExistingSave()
    {
        User = UserDataGetter.GetUserData();

        if (!PlayerPrefs.HasKey("Level"))
        {
            ResetUserData();
            return;
        }

        User.Level = PlayerPrefs.GetInt("Level", 1);
        User.Coins = PlayerPrefs.GetInt("Coins", 0);

        foreach (Category category in Database.Categories)
        {
            if (PlayerPrefs.HasKey("Avatar_" + category.CategoryName))
            {
                User.Avatar.SkinsSlots.Add(new SkinSlotData(category.CategoryName, Database.GetItem(PlayerPrefs.GetString("Avatar_" + category.CategoryName))));
            }

            foreach (Item item in category.Items)
            {
                if (PlayerPrefs.HasKey("Owned_" + item.name))
                {
                    User.ItemsOwned.Add(item);
                }
            }
        }


        OnUserDataChanged?.Invoke();
    }

    void ResetUserData()
    {
        User.Avatar.SkinsSlots.Add(new SkinSlotData("Eyes", Database.GetItem("Eyes 1")));
        User.Avatar.SkinsSlots.Add(new SkinSlotData("Mouths", Database.GetItem("Mouth 1")));
    }



}

