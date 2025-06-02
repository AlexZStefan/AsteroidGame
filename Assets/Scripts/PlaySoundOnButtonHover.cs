using UnityEngine;
using UnityEngine.EventSystems;

public class PlaySoundOnButtonHover : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("SFX_hover");
    }
}