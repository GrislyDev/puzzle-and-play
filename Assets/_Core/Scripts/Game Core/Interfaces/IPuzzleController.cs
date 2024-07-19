using UnityEngine.EventSystems;

public interface IPuzzleController
{
	public bool IsEnabled { get; set; }
	void OnPointerClick(PointerEventData eventData);
}