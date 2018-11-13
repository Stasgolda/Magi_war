using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//скрипт для определения зажата ли кнопка

public class HoldButton : EventTrigger {

	private float duration; // время нажатия на кнопку
	private bool isDown; // нажата ли кнопка

	public float Duration {
		get {
			return duration;
		}
	}

	void Update () {
		if (isDown) {
			duration += Time.deltaTime;
		}
	}
		
	public override void OnPointerDown (PointerEventData data) {
		duration = 0f;
		isDown = true;
	}

	public override void OnPointerUp(PointerEventData data) {
		print (duration);
		isDown = false;
	}
}
