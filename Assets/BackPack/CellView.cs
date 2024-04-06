using System;
using Gun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellView : MonoBehaviour, IPointerClickHandler
{
    public Action<CellView> OnSelect;
    public GunView GunView;
    public Image CellImage;
    public Text Count;
    public bool IsUsed;
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSelect?.Invoke(this);
    }
}
