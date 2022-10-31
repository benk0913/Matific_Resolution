using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CategoryButtonUI : SelectableUI
{
    [SerializeField]
    TextMeshProUGUI TitleLabel;

    Action<CategoryButtonUI> OnCategorySelected;

    public Category CurrentCategory;

    public void SetInfo(Category category, Action<CategoryButtonUI> onSelect = null)
    {
        CurrentCategory = category;
        TitleLabel.text = CurrentCategory.CategoryName;

        OnCategorySelected = onSelect;
    }

    public void CategoryClick()
    {
        OnCategorySelected?.Invoke(this);
    }
    
}
