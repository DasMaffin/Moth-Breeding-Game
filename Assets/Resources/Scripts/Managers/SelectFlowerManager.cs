using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectFlowerManager : MonoBehaviour, IPointerClickHandler
{
    #region Singleton

    private static SelectFlowerManager _instance;

    public static SelectFlowerManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            if(_instance != null)
            {
                Destroy(value.gameObject);
                return;
            }
            _instance = value;
        }
    }

    #endregion

    private Flower flower;
    public Flower Flower
    {
        get => flower;
        set
        {
            FlowerImage.sprite = value.FlowerRepresentation;
            FlowerImage.preserveAspect = true;
            FlowerName.text = value.FriendlyName;
            flower = value;
        }
    }

    [SerializeField] private Image FlowerImage;
    [SerializeField] private TextMeshProUGUI FlowerName;

    private void Awake()
    {
        Instance = this;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HUDManager.Instance.ToggleFlowerSelectionHud();
    }
}
