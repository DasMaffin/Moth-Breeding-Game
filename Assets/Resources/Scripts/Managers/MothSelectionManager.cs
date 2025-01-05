using System.Net.Sockets;
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
    public Image SecondImageComponent;

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
        }
        else if(mc.Gender == Gender.Female)
        {
            GameManager.Instance.selectedMoths.SetSecondMoth(mc);
            MothSelectionManager.Instance.SecondImageComponent.sprite = mc.self.MothRepresentation;
        }
    }
}
