using System;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region Singleton

    private static HUDManager _instance;

    public static HUDManager Instance
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

    [SerializeField] private GameObject FlowerSelectionHUD;
    [SerializeField] private GameObject FlowerSelectionScrollContentRoot;

    [SerializeField] private GameObject FlowerSelectionScrollContentEntryPrefab;

    public void ToggleFlowerSelectionHud()
    {
        FlowerSelectionHUD.SetActive(!FlowerSelectionHUD.activeSelf);
    }
    public void ToggleFlowerSelectionHud(bool SetState)
    {
        FlowerSelectionHUD.SetActive(SetState);
    }

    public void AddFlowerToSelection(KeyValuePair<Flower, int> v)
    {
        FlowerEntryController fec = Instantiate(FlowerSelectionScrollContentEntryPrefab, FlowerSelectionScrollContentRoot.transform).GetComponent<FlowerEntryController>();
        fec.Flower = v;
    }

    private void Awake()
    {
        HUDManager.Instance = this;
    }
}
