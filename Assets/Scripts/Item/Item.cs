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

		/// <summary>自身より上段にあるアイテム</summary>
		[SerializeField]
		private Item m_itemChild;

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

			if (m_itemParent != null) {
				m_itemParent.UnRegisterChild(this);
			}
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
			Item item = GetRoot();
			
			item.OnGrasped();
			return item;
		}
		
		/// <summary>
		/// 最上段にあるアイテムを取得する
		/// </summary>
		/// <returns></returns>
		private Item GetRoot() {
			if(m_itemChild != null) {
				return m_itemChild.GetRoot();
			}
			return this;
		}

		/// <summary>
		/// アイテムが重なったときに
		/// 親となるアイテムに自身を登録する
		/// </summary>
		/// <param name="arg_child"></param>
		public void RegisterChild(Item arg_child) {
			m_itemChild = arg_child;
			m_itemChild.m_itemParent = this;
		}

		/// <summary>
		/// 親子関係を解除する
		/// </summary>
		/// <param name="arg_child"></param>
		public void UnRegisterChild(Item arg_child) {
			m_itemChild.m_itemParent = null;
			m_itemChild = null;
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
					item.RegisterChild(this);
				}
			}
		}

		/// <summary>
		/// 設置状態が解除された時に実行される
		/// </summary>
		protected virtual void OnGroundExit() {
			if(m_itemParent != null) {
				m_itemParent.UnRegisterChild(this);
			}
		}

		
	}
}