using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScriptExit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
