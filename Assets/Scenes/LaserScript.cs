﻿using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

	LineRenderer line;


	// Use this for initialization
	void Start () {
		
		line = gameObject.GetComponent<LineRenderer> ();
		line.enabled = false;
		gameObject.GetComponent<Light> ().enabled = false;

		// thay cho Screen.lockCursor = true;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			StopCoroutine ("FireLaser");
			StartCoroutine ("FireLaser");
		}
	}

	IEnumerator FireLaser(){
		line.enabled = true;
		gameObject.GetComponent<Light> ().enabled = true;

		while (Input.GetButton ("Fire1")) {
			// scroll material line
			line.material.mainTextureOffset = new Vector2 (0, Time.time);

			Ray ray = new Ray (transform.position, transform.forward);
			RaycastHit hit;

			line.SetPosition (0, ray.origin);

			if (Physics.Raycast (ray, out hit)) {
				line.SetPosition (1, hit.point);
				if(hit.rigidbody){
					// do dai duong dan
					hit.rigidbody.AddForceAtPosition (transform.forward * 5, hit.point);
				}
			}
			else
				line.SetPosition (1, ray.GetPoint (100));

			yield return null;
		}

		line.enabled = false;
		gameObject.GetComponent<Light> ().enabled = false;
	}
}
