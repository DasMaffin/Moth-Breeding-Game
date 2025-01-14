using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FlowerEntryController : MonoBehaviour, IPointerClickHandler
{
    private KeyValuePair<Flower, int> flower;
    public KeyValuePair<Flower, int> Flower
    {
        get => flower;
        set
        {
            FlowerImage.sprite = value.Key.FlowerRepresentation;
            FlowerImage.preserveAspect = true;
            FlowerName.text = value.Key.FriendlyName;
            OwnedAmount.text = value.Value.ToString("0");
            flower = value;
        }
    }
    public Image FlowerImage;
    public TextMeshProUGUI FlowerName;
    public TextMeshProUGUI OwnedAmount;

    public void OnPointerClick(PointerEventData eventData)
    {
        SelectFlowerManager.Instance.Flower = Flower.Key;
        HUDManager.Instance.ToggleFlowerSelectionHud();
        GameManager.Instance.selectedFlower = Flower.Key;
    }
}
