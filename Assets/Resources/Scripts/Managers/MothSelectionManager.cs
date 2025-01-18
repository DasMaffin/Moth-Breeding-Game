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
        if(mc.Moth.Gender == Gender.Male)
        {
            GameManager.Instance.selectedMoths.SetFirstMoth(mc);
            MothSelectionManager.Instance.FirstImageComponent.sprite = mc.Moth.species.MothRepresentation;
            MothSelectionManager.Instance.FirstNameComponent.text = mc.Moth.species.FriendlyName;
            MothSelectionManager.Instance.FirstMC = mc;
        }
        else if(mc.Moth.Gender == Gender.Female)
        {
            GameManager.Instance.selectedMoths.SetSecondMoth(mc);
            MothSelectionManager.Instance.SecondImageComponent.sprite = mc.Moth.species.MothRepresentation;
            MothSelectionManager.Instance.SecondNameComponent.text = mc.Moth.species.FriendlyName;
            MothSelectionManager.Instance.SecondMC = mc;
        }
    }

    private void Update()
    {
        if(MothSelectionManager.Instance.FirstMC)
        {
            if(!MothSelectionManager.Instance.FirstMC.Moth.BreedingCooldownActive)
            {
                MothSelectionManager.Instance.FirstTimerComponent.text = "Moth can breed!";
            }
            else
            {
                MothSelectionManager.Instance.FirstTimerComponent.text = MothSelectionManager.Instance.FirstMC.Moth.BreedingCooldownLeft.ToString("0") + "s";
            }
        }
        if(MothSelectionManager.Instance.SecondMC)
        {
            if(!MothSelectionManager.Instance.SecondMC.Moth.BreedingCooldownActive)
            {
                MothSelectionManager.Instance.SecondTimerComponent.text = "Moth can breed!";
            }
            else
            {
                MothSelectionManager.Instance.SecondTimerComponent.text = MothSelectionManager.Instance.SecondMC.Moth.BreedingCooldownLeft.ToString("0") + "s";
            }
        }
    }
}
