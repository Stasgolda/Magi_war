using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")]
    

    Vector2 joystickPosition = Vector2.zero;
    private Camera cam = new Camera();
	private Image img;
	private Image handleImg;

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(cam, background.position);
		img = GetComponent<Image> ();
		handleImg = transform.GetChild (0).GetComponent<Image> ();
		img.color = new Color(1f, 1f, 1f, 0.1f);
		handleImg.color = new Color (1f, 1f, 1f, 0.1f);
    }

    public override void OnDrag(PointerEventData eventData)
    {
		Vector2 direction = eventData.position - joystickPosition;
		if (direction.magnitude > background.sizeDelta.x / 6f) {
			inputVector = direction.normalized;
		}
		//inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : 0;//direction / (background.sizeDelta.x / 2f);
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
		img.color = new Color(1f, 1f, 1f, 1f);
		handleImg.color = new Color (1f, 1f, 1f, 1f);
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
		img.color = new Color(1f, 1f, 1f, 0.1f);
		handleImg.color = new Color (1f, 1f, 1f, 0.1f);
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}