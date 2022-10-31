using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 2)]
[Serializable]
public class Item : ScriptableObject
{
    public ItemType Type;
    public Sprite Icon;
    public string Skin;

    [TextArea(3, 6)]
    public string Description;

    public int Cost;
    public int MinimumLevel;

    public ItemState State
    {
        get
        {
            if(DemoCore.User.Level < MinimumLevel)         return ItemState.LockedByLevel;

            if(DemoCore.User.ItemsOwned.Contains(this))    return ItemState.Available;
            
                                                                    return ItemState.PendingPurchase;
        }
    }

    public enum ItemState
    {
        Available, PendingPurchase, LockedByLevel
    }
}