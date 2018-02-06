using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bucket {
	[RequireComponent(typeof(Rigidbody2D))]
	public class Item : MonoBehaviour {

		/// <summary>Rigidbody</summary>
		[SerializeField]
		private Rigidbody2D m_rigidbody;

		/// <summary>自身より下段においてあるアイテム</summary>
		[SerializeField]
		private Item m_itemParent;

		/// <summary>
		/// 自身より上段にあるアイテムたち
		/// ※自身が最下段のときにのみ使用される
		/// </summary>
		private readonly Stack<Item> m_itemChildren = new Stack<Item>();

		/// <summary>自身の衝突用コライダー</summary>
		[SerializeField]
		private BoxCollider2D m_body;

		/// <summary>接地判定用コライダー</summary>
		[SerializeField]
		private BoxCollider2D m_foot;

		private void Start() {
			if(m_itemParent != null) {
				GetRoot().RegisterChild(this);
			}
		}

		/// <summary>
		/// 自身が掴まれたときに実行される
		/// </summary>
		private void OnGrasped() {

			m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

			m_itemParent = null;
			m_rigidbody.isKinematic = true;
			m_rigidbody.velocity = Vector2.zero;
			m_body.enabled = false;
		}

		/// <summary>
		/// 自身が離されたときに実行される
		/// </summary>
		private void OnRelease() {
			m_rigidbody.isKinematic = false;
			m_body.enabled = true;
		}

		/// <summary>
		/// 射出を開始する
		/// </summary>
		/// <param name="arg_velocity">加速度</param>
		public void Throw(Vector2 arg_velocity) {

			//射出する
			m_rigidbody.AddForce(arg_velocity * m_rigidbody.mass, ForceMode2D.Impulse);
			this.OnRelease();
		}
		
		/// <summary>
		/// アイテムを取得する
		/// 自身の頭上にアイテムが置いてある場合はそちらを渡す
		/// </summary>
		/// <returns></returns>
		public Item GetGraspableItem() {
			Item item = null;
			Item root = GetRoot();
			
			if (root.m_itemChildren.Count > 0) { item = root.m_itemChildren.Pop(); }
			else { item = this; }
			item.OnGrasped();
			return item;
		}
		
		/// <summary>
		/// 最下段にあるアイテムを取得する
		/// </summary>
		/// <returns></returns>
		private Item GetRoot() {
			if(m_itemParent != null) {
				return m_itemParent.GetRoot();
			}
			return this;
		}

		/// <summary>
		/// アイテムが重なったときに
		/// 親となるアイテムに自身を登録する
		/// </summary>
		/// <param name="arg_child"></param>
		public void RegisterChild(Item arg_child) {
			//不整合防止のため、すでに登録されている場合は中断する
			if (m_itemChildren.Contains(arg_child)) return;
			m_itemChildren.Push(arg_child);
		}

		//----------------------------------------------
		//	接地判定時のコールバック
		//----------------------------------------------

		/// <summary>
		/// 何かに設置したときに実行される
		/// </summary>
		protected virtual void OnGroundEnter() {

			m_rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;

			//滑り止め
			m_rigidbody.velocity = new Vector2(0,m_rigidbody.velocity.y);

			//接地した情報からItemを抽出する
			RaycastHit2D hitInfo = Physics2D.BoxCast(
				m_foot.transform.position , m_foot.bounds.size ,
				0 , Vector2.zero , 0 , 1 << LayerMask.NameToLayer("Item"));

			if (hitInfo) {
				Item item = hitInfo.transform.GetComponent<Item>();
				if (item) {
					this.m_itemParent = item;
					GetRoot().RegisterChild(this);
				}
			}
		}

		/// <summary>
		/// 設置状態が解除された時に実行される
		/// </summary>
		protected virtual void OnGroundExit() {

		}

		
	}
}