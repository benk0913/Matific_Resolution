using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarViewUI : MonoBehaviour
{
    [SerializeField]
    List<ItemDisplayEntity> SkinSlots = new List<ItemDisplayEntity>();

    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        DemoCore.OnUserDataChanged?.AddListener(() => Refresh());
         Refresh();
    }

    void Refresh()
    {
        foreach (SkinSlotData skinSlot in DemoCore.User.Avatar.SkinsSlots)
        {
            UpdateVisualEntity(skinSlot);
        }
    }

    void UpdateVisualEntity(SkinSlotData data)
    {
        ItemDisplayEntity entity = SkinSlots.Find(x => x.Type == data.Key);

        if (entity == null)
        {
            Debug.LogError("No supporting skin slot for type: " + data.Key);
            return;
        }

        entity.SetInfo(data.Item);
    }

    public void SetSkin(string type, Item item)
    {
        DemoCore.User.Avatar.SetSkin(type, item);
    }
}

