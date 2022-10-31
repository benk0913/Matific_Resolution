using System.Collections.Generic;

public class UserData
{
    public int Level;
    public int Coins;
    public List<Item> ItemsOwned;

    public AvatarData Avatar;

}

public class AvatarData
{
    public List<SkinSlotData> SkinsSlots = new List<SkinSlotData>();

    public AvatarData()
    {
        SkinsSlots.Add(new SkinSlotData());
    }

    public SkinSlotData SetSkin(string type, Item item)
    {
        SkinSlotData dataSkin = SkinsSlots.Find(x => x.Key == type);

        if (dataSkin == null)
        {
            dataSkin = new SkinSlotData();
            dataSkin.Key = type;
            SkinsSlots.Add(dataSkin);
        }

        dataSkin.Item = item;

        return dataSkin;

    }
}

public class SkinSlotData
{
    public string Key;
    public Item Item;

    public SkinSlotData(string key = default, Item item = null)
    {
        this.Key = key;
        this.Item = item;
    }
}
