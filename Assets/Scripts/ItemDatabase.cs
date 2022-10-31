using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/ItemDatabase", order = 2)]
[Serializable]
public class ItemDatabase : ScriptableObject
{
    public List<Category> Categories = new List<Category>();
    public Item GetItem(string itemName)
    {
        foreach(Category category in Categories)
        {
            Item item = category.Items.Find(X=>X.name == itemName);

            if(item == null) continue;

            return item;
        }

        return null;
    }
}

[System.Serializable]
public class Category
{
    public string CategoryName;

    public List<Item> Items = new List<Item>();

    
    
}