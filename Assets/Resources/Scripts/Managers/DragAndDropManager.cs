using UnityEngine;

public class DragAndDropManager : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public RectTransform uiRepresentation; // 2D UI representation of the object
    public RectTransform targetUI; // The UI element to drop on

    private bool isDragging = false;
    private GameObject selectedObject;

    void Update()
    {
        // Check for mouse down on a 3D object
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.layer == 6)
            {
                selectedObject = hit.collider.gameObject;
                StartDragging();
            }
        }

        // Update the 2D representation's position
        if(isDragging)
        {
            uiRepresentation.position = Input.mousePosition;

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
        uiRepresentation.gameObject.SetActive(true); // Show the 2D representation
    }

    private void StopDragging()
    {
        isDragging = false;
        uiRepresentation.gameObject.SetActive(false); // Hide the 2D representation

        // Check if released over the target UI
        if(RectTransformUtility.RectangleContainsScreenPoint(targetUI, new Vector2(Input.mousePosition.x, Input.mousePosition.y)))
        {
            OnDroppedOnTargetUI();
        }

        selectedObject = null;
    }

    private void OnDroppedOnTargetUI()
    {
        MothSelectionManager.Instance.SetMothSelection(selectedObject.GetComponentInParent<MothController>());
    }
}
