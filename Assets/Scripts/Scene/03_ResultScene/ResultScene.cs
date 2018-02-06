using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToyBox;
using UnityEngine.UI;

namespace Bucket {
	public class ResultScene : Scene {

		[SerializeField]
		private Text m_scoreText;
		[SerializeField]
		private Text m_timeText;
		[SerializeField]
		private Text m_nextSceneText;

		[SerializeField]
		private AudioSource bgm_;

		public override IEnumerator OnEnter() {
			AppManager.Instance.fade.StartFade(new FadeIn() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);

			m_scoreText.text = GameScore.TotalScore.ToString();
		}

		public override IEnumerator OnUpdate() {
			yield return new WaitUntil (() => Input.anyKeyDown);
			m_nextSceneText.GetComponent<Animator>().Play("Flash");

			for(int i = 1;i <= 60;i ++){
				bgm_.volume = 1f - ((float)i/60f);
				yield return null;
			}
			yield break;
		}

		public override IEnumerator OnExit() {
			AppManager.Instance.fade.StartFade(new FadeOut() , Color.black , 0.5f);
			yield return new WaitWhile(AppManager.Instance.fade.IsFading);
			UnityEngine.SceneManagement.SceneManager.LoadScene("SceneTitle");
		}
	}
}