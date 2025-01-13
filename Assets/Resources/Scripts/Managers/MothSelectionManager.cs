using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MothSelectionManager : MonoBehaviour
{
    #region Singleton

    private static MothSelectionManager _instance;

    public static MothSelectionManager Instance
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

    public Image FirstImageComponent;
    public TextMeshProUGUI FirstNameComponent;
    public TextMeshProUGUI FirstTimerComponent;

    public Image SecondImageComponent;
    public TextMeshProUGUI SecondNameComponent;
    public TextMeshProUGUI SecondTimerComponent;

    private MothController FirstMC;
    private MothController SecondMC;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMothSelection(MothController mc)
    {
        if(mc.Gender == Gender.Male)
        {
            GameManager.Instance.selectedMoths.SetFirstMoth(mc);
            MothSelectionManager.Instance.FirstImageComponent.sprite = mc.self.MothRepresentation;
            MothSelectionManager.Instance.FirstNameComponent.text = mc.self.FriendlyName;
            MothSelectionManager.Instance.FirstMC = mc;
        }
        else if(mc.Gender == Gender.Female)
        {
            GameManager.Instance.selectedMoths.SetSecondMoth(mc);
            MothSelectionManager.Instance.SecondImageComponent.sprite = mc.self.MothRepresentation;
            MothSelectionManager.Instance.SecondNameComponent.text = mc.self.FriendlyName;
            MothSelectionManager.Instance.SecondMC = mc;
        }
    }

    private void Update()
    {
        if(MothSelectionManager.Instance.FirstMC)
        {
            if(!MothSelectionManager.Instance.FirstMC.BreedingCooldownActive)
            {
                MothSelectionManager.Instance.FirstTimerComponent.text = "Moth can breed!";
            }
            else
            {
                MothSelectionManager.Instance.FirstTimerComponent.text = MothSelectionManager.Instance.FirstMC.BreedingCooldownLeft.ToString("0");
            }
        }
        if(MothSelectionManager.Instance.SecondMC)
        {
            if(!MothSelectionManager.Instance.SecondMC.BreedingCooldownActive)
            {
                MothSelectionManager.Instance.SecondTimerComponent.text = "Moth can breed!";
            }
            else
            {
                MothSelectionManager.Instance.SecondTimerComponent.text = MothSelectionManager.Instance.SecondMC.BreedingCooldownLeft.ToString("0");
            }
        }
    }
}
