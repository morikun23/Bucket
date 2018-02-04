using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class PlayerController : MonoBehaviour {

		[SerializeField]
		private Player m_player;

		[SerializeField]
		private ItemHolder m_itemHolder;
		
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
			m_downKey.AddCallBack(new CommandAction(InputTrigger.Press , OnHideButtonDown));
			m_downKey.AddCallBack(new CommandAction(InputTrigger.Release , OnHideButtonUp));

			m_spaceKey.AddCallBack(new CommandAction(InputTrigger.Press , OnActionButtonDown));
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

		private void OnHideButtonDown() {
			m_player.Hide();
		}

		private void OnHideButtonUp() {
			m_player.Show();
		}

		private void OnActionButtonDown() {

			if(m_player.GetCurrentState() == typeof(Player.HideState)) {
				return;
			}

			if(m_itemHolder.IsHolding()){
				m_itemHolder.Throw();
				return;
			}
			else{
				if (m_player.IsGrounded()) {
					m_itemHolder.Grasp();
					if (m_itemHolder.IsHolding()) {
						return;
					}
				}
			}
			m_player.Jump();
		}
	}
}