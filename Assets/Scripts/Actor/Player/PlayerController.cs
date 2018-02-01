using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class PlayerController : MonoBehaviour {

		[SerializeField]
		private Player m_player;

		[SerializeField]
		private InputCommand m_leftKey;

		[SerializeField]
		private InputCommand m_rightKey;

		[SerializeField]
		private InputCommand m_upKey;

		[SerializeField]
		private InputCommand m_downKey;

		[SerializeField]
		private InputCommand m_spaceKey;

		// Use this for initialization
		void Start() {
			m_rightKey.AddCallBack(new CommandAction(InputTrigger.LongPress,OnMoveButtonDown,(int)ActorBase.Direction.RIGHT));
			m_rightKey.AddCallBack(new CommandAction(InputTrigger.Release , OnMoveButtonUp , (int)ActorBase.Direction.RIGHT));
			m_leftKey.AddCallBack(new CommandAction(InputTrigger.LongPress , OnMoveButtonDown , (int)ActorBase.Direction.LEFT));
			m_leftKey.AddCallBack(new CommandAction(InputTrigger.Release , OnMoveButtonUp , (int)ActorBase.Direction.LEFT));
		}

		// Update is called once per frame
		void Update() {

		}

		private void OnMoveButtonDown(object arg_direction) {

			int direction = (int)arg_direction;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Run(Player.Direction.LEFT); break;
				case (int)Player.Direction.RIGHT: m_player.Run(Player.Direction.RIGHT); break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません"); break;
			}
		}

		private void OnMoveButtonUp(object arg_direction) {
			int direction = (int)arg_direction;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Stop(Player.Direction.LEFT); break;
				case (int)Player.Direction.RIGHT: m_player.Stop(Player.Direction.RIGHT); break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません"); break;
			}
		}

	}
}