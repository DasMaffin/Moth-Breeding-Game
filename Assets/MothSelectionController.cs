using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MothSelectionController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event System.Action OnInitSelectEvent;
    public static event System.Action OnSelectMothEvent;
    public static MothSelectionController currentController;

    private bool awaitingRaycast = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(awaitingRaycast)
            return;

        currentController = this;
        this.GetComponent<Image>().color = Color.grey;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(awaitingRaycast)
            return;

        OnInitSelectEvent?.Invoke();
        awaitingRaycast = true;
    }

    private void OnEnable()
    {
        OnInitSelectEvent += HandleSelection;
        OnSelectMothEvent += SelectMoth;
    }

    private void OnDisable()
    {
        OnInitSelectEvent -= HandleSelection;
        OnSelectMothEvent -= SelectMoth;
    }

    private void HandleSelection()
    {
        if(this == MothSelectionController.currentController)
        {
            this.GetComponent<Image>().color = Color.yellow;

            return;
        }
        if(this.GetComponent<Image>().sprite != null) return;

        this.GetComponent<Image>().color = Color.black;
    }

    private void SelectMoth()
    {
        this.GetComponent<Image>().color = Color.white;
    }

    private void Update()
    {
        if(!awaitingRaycast) return;
        print(gameObject);
        if(Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            awaitingRaycast = false; // Reset the flag
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Moth")))
        {
            this.GetComponent<Image>().sprite = hit.collider.gameObject.GetComponent<MothController>().self.MothRepresentation;
            Debug.Log($"Hit object: {hit.collider.gameObject.name}");
        }
        else
        {
            Debug.Log("No moth detected below cursor!");
        }
        OnSelectMothEvent?.Invoke();
    }
}
