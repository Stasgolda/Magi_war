using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDirection : MonoBehaviour {
	
	private NavMeshAgent agent;
	public MoveCamera cam;

	void Start () {
		
		agent = GetComponent<NavMeshAgent> ();
		agent.destination = new Vector3 (350f, 18f, 260f);
		agent.speed = 15f;
		GetComponent<Animator> ().SetBool ("Move", true);
	}

	void Update () {
		if (agent.destination.x - transform.position.x < 1 ) {
			agent.enabled = false;
			cam.character = null;
			GetComponent<Animator> ().SetBool ("Move", false);
		}
	}
}
