using UnityEngine;
using UnityEngine.UI;

public class DragAndDropManager : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public RectTransform uiRepresentation; // 2D UI representation of the object
    public GameObject MaleIcon;
    public GameObject FemaleIcon;
    public Image MothRepresentation;
    public RectTransform targetUI; // The UI element to drop on

    private bool isDragging = false;
    private MothController selectedObject;
    private Vector2 initialMousePosition;
    private bool hasMovedMouse = false;

    void Update()
    {
        // Check for mouse down on a 3D object
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.layer == 6)
            {
                selectedObject = hit.collider.gameObject.GetComponentInParent<MothController>();
                StartDragging();
            }
        }

        // Update the 2D representation's position
        if(isDragging)
        {
            if(!hasMovedMouse && Vector2.Distance(initialMousePosition, Input.mousePosition) > 5f) // Adjust threshold as needed
            {
                hasMovedMouse = true;
                uiRepresentation.gameObject.SetActive(true); // Show the 2D representation
                MothRepresentation.sprite = selectedObject.Moth.species.MothRepresentation;
                if(selectedObject.Moth.Gender == Gender.Male)
                {
                    MaleIcon.SetActive(true);
                }
                else if(selectedObject.Moth.Gender == Gender.Female)
                {
                    FemaleIcon.SetActive(true);
                }
            }

            if(hasMovedMouse)
            {
                uiRepresentation.position = Input.mousePosition;
            }

            // Check for mouse up
            if(Input.GetMouseButtonUp(0))
            {
                StopDragging();
            }
        }
    }

    private void StartDragging()
    {
        isDragging = true;
        initialMousePosition = Input.mousePosition;
        hasMovedMouse = false; // Reset mouse movement flag
    }

    private void StopDragging()
    {
        isDragging = false;
        uiRepresentation.gameObject.SetActive(false); // Hide the 2D representation
        MaleIcon.SetActive(false);
        FemaleIcon.SetActive(false);

        // Check if released over the target UI
        if(RectTransformUtility.RectangleContainsScreenPoint(targetUI, new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
        {
            OnDroppedOnTargetUI();
        }

        selectedObject = null;
    }

    private void OnDroppedOnTargetUI()
    {
        MothSelectionManager.Instance.SetMothSelection(selectedObject);
    }
}
