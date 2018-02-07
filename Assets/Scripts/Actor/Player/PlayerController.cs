using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	public class PlayerController : MonoBehaviour {

		
		//-----------------------------------------
		//	デフォルト設定
		[Header("Default Settings")]
		//-----------------------------------------

		/// <summary>制御する対象</summary>
		private Player m_player;

		/// <summary>アイテムを制御するためのクラス</summary>
		private ItemHolder m_itemHolder;

		//-----------------------------------------
		//	ボタン設定
			[Header("Buttons")]
		//-----------------------------------------

		[SerializeField]
		private InputCommand m_leftKey;

		[SerializeField]
		private InputCommand m_rightKey;

		[SerializeField]
		private InputCommand m_upKey;

		[SerializeField]
		private InputCommand m_downKey;

		[SerializeField]
		private InputCommand m_actionKey;

		public void Initialize(Player arg_player) {

			m_player = arg_player;
			m_itemHolder = m_player.GetComponentInChildren<ItemHolder>();

			m_rightKey.AddCallBack(new CommandAction(InputTrigger.LongPress,OnMoveButtonDown,(int)ActorBase.Direction.RIGHT));
			m_rightKey.AddCallBack(new CommandAction(InputTrigger.Release , OnMoveButtonUp , (int)ActorBase.Direction.RIGHT));
			m_leftKey.AddCallBack(new CommandAction(InputTrigger.LongPress , OnMoveButtonDown , (int)ActorBase.Direction.LEFT));
			m_leftKey.AddCallBack(new CommandAction(InputTrigger.Release , OnMoveButtonUp , (int)ActorBase.Direction.LEFT));
			m_downKey.AddCallBack(new CommandAction(InputTrigger.Press , OnHideButtonDown));
			m_downKey.AddCallBack(new CommandAction(InputTrigger.Release , OnHideButtonUp));

			m_actionKey.AddCallBack(new CommandAction(InputTrigger.Press , OnActionButtonDown));
		}

		/// <summary>
		/// 移動ボタンが押されたとき
		/// </summary>
		/// <param name="arg_direction"></param>
		private void OnMoveButtonDown(object arg_direction) {

			int direction = (int)arg_direction;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Run(Player.Direction.LEFT); break;
				case (int)Player.Direction.RIGHT: m_player.Run(Player.Direction.RIGHT); break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません"); break;
			}
		}

		/// <summary>
		/// 移動ボタンが離されたとき
		/// </summary>
		/// <param name="arg_direction"></param>
		private void OnMoveButtonUp(object arg_direction) {
			int direction = (int)arg_direction;

			switch (direction) {
				case (int)Player.Direction.LEFT: m_player.Stop(Player.Direction.LEFT); break;
				case (int)Player.Direction.RIGHT: m_player.Stop(Player.Direction.RIGHT); break;
				default: Debug.LogError("ボタンからのコールバックが正しくありません"); break;
			}
		}

		/// <summary>
		/// 隠れるボタンが押されたとき
		/// </summary>
		private void OnHideButtonDown() {
			if (m_player.IsGrounded() && !m_itemHolder.IsHolding()) {
				m_player.Hide();
			}
		}

		/// <summary>
		/// 隠れるボタンが離された時
		/// </summary>
		private void OnHideButtonUp() {
			m_player.Show();
		}

		/// <summary>
		/// アクションボタンが押されたとき
		/// </summary>
		private void OnActionButtonDown() {

			if(m_player.GetCurrentState() == typeof(Player.HideState)) {
				return;
			}

			if(m_itemHolder.IsHolding()){
				m_itemHolder.Throw();
				return;
			}
			else if (m_player.IsGrounded()) {
				m_itemHolder.Grasp();

				//掴むアイテムが無ければジャンプを行う
				if (m_itemHolder.IsHolding()) {
					return;
				}
				m_player.Jump();
			}
		
			
		}
	}
}