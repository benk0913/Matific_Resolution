using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ItemStorePanelUI : MonoBehaviour
{
    #region Essentials

    public Transform CategoriesContainer;
    public Transform ItemsContainer;

    [SerializeField]
    GameObject CategoryPrefab;
    [SerializeField]
    GameObject ItemPrefab;

    CategoryButtonUI CurrentSelectedCategory;
    ItemSlotUI CurrentSelectedItem;

    [SerializeField]
    AvatarViewUI DummyAvatar;

    [SerializeField]
    GameObject BuyButton;

    

    void Start()
    {
        RefreshCategories();
        SelectCategory();
        
    }


    #endregion

    #region Public Interfacing
    public void RefreshCategories()
    {
        while (CategoriesContainer.childCount > 0)
        {
            GameObject categoryToClear = CategoriesContainer.GetChild(0).gameObject;
            categoryToClear.transform.SetParent(null);
            Destroy(categoryToClear);
        }

        foreach (Category category in DemoCore.Instance.Database.Categories)
        {
            CategoryButtonUI categoryButton = Instantiate(CategoryPrefab).GetComponent<CategoryButtonUI>();

            categoryButton.transform.SetParent(CategoriesContainer);
            categoryButton.transform.localScale = Vector3.one;
            categoryButton.transform.position = Vector3.zero;
            categoryButton.SetInfo(category, (CategoryButtonUI selected) => SelectCategory(selected));
        }

    }

    public void RefreshItems()
    {
        if (CurrentSelectedCategory == null) return;

        while (ItemsContainer.childCount > 0)
        {
            GameObject itemToClear = ItemsContainer.GetChild(0).gameObject;
            itemToClear.transform.SetParent(null);
            Destroy(itemToClear);
        }

        foreach (Item item in CurrentSelectedCategory.CurrentCategory.Items)
        {
            ItemSlotUI itemSlot = Instantiate(ItemPrefab).GetComponent<ItemSlotUI>();

            itemSlot.transform.SetParent(ItemsContainer);
            itemSlot.transform.localScale = Vector3.one;
            itemSlot.transform.position = Vector3.zero;
            itemSlot.SetInfo(item, (ItemSlotUI selected) => SelectItem(selected));
        }
    }

    public void SelectCategory(CategoryButtonUI selectedCategory = null)
    {
        if (selectedCategory == null)
        {
            selectedCategory = CategoriesContainer.GetChild(0).GetComponent<CategoryButtonUI>();
        }

        if (CurrentSelectedCategory != null)
        {
            CurrentSelectedCategory.SetDeselected();
        }

        CurrentSelectedCategory = selectedCategory;
        CurrentSelectedCategory.SetSelected();

        RefreshItems();
    }

    public void SelectItem(ItemSlotUI selectedItem)
    {
        if (CurrentSelectedItem != null)
        {
            CurrentSelectedItem.SetDeselected();
        }

        CurrentSelectedItem = selectedItem;
        CurrentSelectedItem.SetSelected();

        DummyAvatar.SetSkin(CurrentSelectedCategory.CurrentCategory.CategoryName, CurrentSelectedItem.CurrentItem);

        BuyButton.SetActive(CurrentSelectedItem.CurrentItem.State == Item.ItemState.PendingPurchase);

        DemoCore.OnUserDataChanged?.Invoke();
    }

    public void PurchaseSelectedItem()
    {
        Item item = CurrentSelectedItem.CurrentItem;

        if (item.State != Item.ItemState.PendingPurchase) return;

        if (DemoCore.User.Coins < item.Cost) return;

        DemoCore.User.Coins -= item.Cost;
        DemoCore.User.ItemsOwned.Add(item);


        CurrentSelectedItem.SetInfo(item, (ItemSlotUI selected) => SelectItem(selected));
        SelectItem(CurrentSelectedItem);
    }

    #endregion

}
