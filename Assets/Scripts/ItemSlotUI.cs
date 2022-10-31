using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : SelectableUI
{
    [SerializeField]
    Image IconImage;

    [SerializeField]
    TextMeshProUGUI PriceLabel; 
    [SerializeField]
    TextMeshProUGUI LevelLabel; 

    [SerializeField]
    GameObject Lock; 
       [SerializeField]
    GameObject OwnedMarker; 

    public Item CurrentItem;

    Action<ItemSlotUI> OnSelected;

    public void SetInfo(Item item, Action<ItemSlotUI> onSelected = null)
    {
        CurrentItem = item;

        IconImage.sprite = CurrentItem.Icon;
        PriceLabel.text =  CurrentItem.Cost.ToString("N0");
        LevelLabel.text =  CurrentItem.MinimumLevel.ToString("N0");
        
        Lock.SetActive(CurrentItem.State == Item.ItemState.LockedByLevel);
        OwnedMarker.SetActive(CurrentItem.State == Item.ItemState.Available);
    
        OnSelected = onSelected;
    }

    public void OnItemClick()
    {
        OnSelected?.Invoke(this);
    }
}
