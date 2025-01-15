using System;
using System.Collections.Generic;
using System.Linq;
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

    private List<GameObject> PopUpHistory = new List<GameObject>();

    [SerializeField] private GameObject FlowerSelectionHUD;
    [SerializeField] private GameObject FlowerSelectionScrollContentRoot;

    [SerializeField] private GameObject FlowerSelectionScrollContentEntryPrefab;

    public void ToggleFlowerSelectionHud()
    {
        ToggleFlowerSelectionHud(!Instance.FlowerSelectionHUD.activeSelf);
    }

    public void ToggleFlowerSelectionHud(bool SetState)
    {
        Instance.FlowerSelectionHUD.SetActive(SetState);
        AddRemovePopUpHistory(Instance.FlowerSelectionHUD, SetState);
    }

    public void CloseLastPopUp()
    {
        if(Instance.PopUpHistory.Count == 0) return;
        GameObject lastPopUp = Instance.PopUpHistory.LastOrDefault();
        lastPopUp.SetActive(false);
        AddRemovePopUpHistory(lastPopUp, false);
    }

    public void AddFlowerToSelection(KeyValuePair<Flower, int> v)
    {
        FlowerEntryController fec = Instantiate(Instance.FlowerSelectionScrollContentEntryPrefab, Instance.FlowerSelectionScrollContentRoot.transform).GetComponent<FlowerEntryController>();
        fec.Flower = v;
    }

    private void AddRemovePopUpHistory(GameObject popUp, bool Add)
    {
        if(Add)
        {
            Instance.PopUpHistory.Add(popUp);
        }
        else
        {
            Instance.PopUpHistory.Remove(popUp);
        }
    }

    private void Awake()
    {
        HUDManager.Instance = this;
    }
}
