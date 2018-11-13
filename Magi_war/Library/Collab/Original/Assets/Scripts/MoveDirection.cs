using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDirection : MonoBehaviour {
	
	private NavMeshAgent agent;
	public MoveCamera cam;
	public GameObject gm;

	void Start () {		
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = gm.transform.position;
		agent.speed = 15f;
		GetComponent<Animator> ().SetBool ("Move", true);
	}
		
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Enemy") {
			agent.enabled = false;
			cam.character = null;
			GetComponent<Animator> ().SetBool ("Move", false);
		}
	}
}
