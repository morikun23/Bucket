using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTest_Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			transform.position += new Vector3(3 * Time.deltaTime,0,0);
		}
	}
}
