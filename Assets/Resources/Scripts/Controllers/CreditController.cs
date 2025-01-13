using UnityEngine;
using UnityEngine.EventSystems;

public class CreditController : MonoBehaviour, IPointerClickHandler
{
    public string webURI;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(!string.IsNullOrEmpty(webURI))
        {
            Application.OpenURL(webURI);
        }
        else
        {
            Debug.LogWarning("webURI is not set or is empty.");
        }
    }
}
