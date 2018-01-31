using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace Bucket {
	public class StartUpScene : Scene {

		public override IEnumerator OnEnter() {
			AppManager.Instance.fade.Fill(Color.black);
			yield break;
		}

		public override IEnumerator OnUpdate() {
			yield break;
		}

		public override IEnumerator OnExit() {
			UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTitle");
			yield return null;

		}
	}
}