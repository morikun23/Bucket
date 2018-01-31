using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;

namespace Bucket {
	public class ResultScene : Scene {

		public override IEnumerator OnEnter() {
			AppManager.Instance.fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
		}

		public override IEnumerator OnUpdate() {
			yield break;
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTitle");
		}
	}
}