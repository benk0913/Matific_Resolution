using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableUI : MonoBehaviour
{
    [SerializeField]
    protected GameObject SelectedMarker;

    public virtual void SetSelected()
    {
        SelectedMarker?.SetActive(true);
    }

    public virtual void SetDeselected()
    {
        SelectedMarker?.SetActive(false);
    }
}
