using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class _SampleEnemy : EnemyBehaviour {

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {
			transform.position += Vector3.right * 0.05f;
		}
	}
}