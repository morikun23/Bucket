using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour {

	[SerializeField]
	float scrollSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Horizontal")){
			transform.Translate (new Vector3(Input.GetAxis("Horizontal") * scrollSpeed,0,0));
		}
	}
}
