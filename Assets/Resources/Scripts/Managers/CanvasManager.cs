using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private bool activeState = true;
    private Animator[] _allAnimators;
    private Animator[] allAnimators
    {
        get
        {
            if(_allAnimators == null)
            {
                _allAnimators = GetComponentsInChildren<Animator>(); 
                foreach(Animator animator in allAnimators)
                {
                    animator.keepAnimatorStateOnDisable = true;
                }
            }
            return _allAnimators;
        }
    }

    public void ToggleHide()
    {
        _ = allAnimators;
        activeState = !activeState;

        gameObject.SetActive(activeState);
    }
}
