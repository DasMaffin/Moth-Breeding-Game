using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MothSelectionController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Tooltip("Index 1: First Moth for breeding selection\nIndex 2: Second moth for breeding selection")] public int index = 0;

    public static event System.Action OnMothSelectStarted;
    public static event System.Action OnMothSelected;
    public static MothSelectionController ActiveControllerForSelection;

    private bool awaitingRaycast = false;
    private Image thisImageComponent;

    private void Awake()
    {
        thisImageComponent = this.GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(awaitingRaycast)
            return;

        ActiveControllerForSelection = this;
        thisImageComponent.color = Color.grey;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(awaitingRaycast)
            return;

        OnMothSelectStarted?.Invoke();
        awaitingRaycast = true; // Set it here so in update it doesnt count as an instant press.
    }

    private void OnEnable()
    {
        OnMothSelectStarted += MothSelectStarted;
        OnMothSelected += MothSelected;
    }

    private void OnDisable()
    {
        OnMothSelectStarted -= MothSelectStarted;
        OnMothSelected -= MothSelected;
    }

    private void MothSelectStarted()
    {
        if(this == ActiveControllerForSelection)
        {
            thisImageComponent.color = Color.yellow;
            return;
        }
        if(thisImageComponent.sprite != null) return;

        thisImageComponent.color = Color.black;
    }

    private void MothSelected()
    {
        thisImageComponent.color = Color.white;
    }

    private void Update()
    {
        if(!awaitingRaycast) return;
        if(Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            awaitingRaycast = false; // Reset the flag
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Moth")))
        {
            Moth moth = hit.collider.gameObject.GetComponent<MothController>().self;
            thisImageComponent.sprite = moth.MothRepresentation;
            if(this.index == 1)
            {
                GameManager.Instance.selectedMoths.SetFirstMoth(moth);
            }
            else if(this.index == 2)
            {
                GameManager.Instance.selectedMoths.SetSecondMoth(moth);
            }
        }
        else
        {
            Debug.Log("No moth detected below cursor!");
        }
        OnMothSelected?.Invoke();
    }
}
