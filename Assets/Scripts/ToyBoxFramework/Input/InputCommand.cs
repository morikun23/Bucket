using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {

	/// <summary>
	/// 入力読み取りタイミング
	/// </summary>
	public enum InputTrigger {
		LongPress,
		Press,
		Release
	}

	public class CommandAction {

		/// <summary>押されたとみなすタイミング</summary>
		public InputTrigger m_trigger { get; private set; }

		/// <summary>コールバック</summary>
		public System.Action m_action { get; private set; }

		/// <summary>引数ありコールバック</summary>
		public System.Action<object> m_objectAction { get; private set; }

		/// <summary>コールバック時の引数</summary>
		public object m_value;

		/// <summary>
		/// コンストラクタ
		/// コールバックを設定する
		/// </summary>
		/// <param name="arg_trigger">タイミング</param>
		/// <param name="arg_action">コールバック</param>
		public CommandAction(InputTrigger arg_trigger , System.Action arg_action) {
			m_trigger = arg_trigger;
			m_action = arg_action;
		}

		/// <summary>
		/// コンストラクタ
		/// 引数ありコールバックを設定する
		/// </summary>
		/// <param name="arg_trigger">タイミング</param>
		/// <param name="arg_objectAction">コールバック</param>
		/// <param name="arg_value">引数</param>
		public CommandAction(InputTrigger arg_trigger , System.Action<object> arg_objectAction , object arg_value) {
			m_trigger = arg_trigger;
			m_objectAction = arg_objectAction;
			m_value = arg_value;
		}

	}

	public class InputCommand : MonoBehaviour {

		/// <summary>使用するキー</summary>
		public KeyCode m_targetKey;

		private readonly List<CommandAction> m_actions = new List<CommandAction>();

		public void AddCallBack(CommandAction arg_action) {
			m_actions.Add(arg_action);
		}

		// Update is called once per frame
		void Update() {
			if (Input.GetKeyDown(m_targetKey)) {
				foreach(CommandAction action in m_actions.FindAll(_ => _.m_trigger == InputTrigger.Press)){
					ExecCallBack(action);
				}
			}

			if (Input.GetKey(m_targetKey)) {
				foreach (CommandAction action in m_actions.FindAll(_ => _.m_trigger == InputTrigger.LongPress)){
					ExecCallBack(action);
				}
			}

			if (Input.GetKeyUp(m_targetKey)) {
				foreach (CommandAction action in m_actions.FindAll(_ => _.m_trigger == InputTrigger.Release)){
					ExecCallBack(action);
				}
			}
		}

		/// <summary>
		/// ボタンが押されたときの処理
		/// コールバックを実行する
		/// </summary>
		protected void ExecCallBack(CommandAction arg_action) {

			if (arg_action == null) return;

			System.Action action = arg_action.m_action;

			if (action != null) {
				action();
				return;
			}

			System.Action<object> objectAction = arg_action.m_objectAction;
			object value = arg_action.m_value;

			if (arg_action.m_objectAction != null) {
				arg_action.m_objectAction(arg_action.m_value);
				return;
			}
		}
	}
}