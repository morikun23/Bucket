using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour {

    private enum DetectorType
    {
        ItemHit,
        ItemFound,
        PlayerSearching
    }

    [SerializeField]
    private DetectorType m_thisDetectorType = DetectorType.ItemHit;

    [SerializeField]
    private GameObject m_noticeTarget;

    [SerializeField]
    private LayerMask m_targetLayer;

    private BoxCollider2D m_collider;

    private void Awake()
    {
        m_collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        RaycastHit2D hitInfo = Physics2D.BoxCast(
                (Vector2)transform.position + m_collider.offset, m_collider.bounds.size,
                0, Vector2.zero, 0, m_targetLayer);


        if (hitInfo)
        {
            switch (m_thisDetectorType)
            {
                case DetectorType.ItemFound: m_noticeTarget.SendMessage("OnEncountItem",hitInfo.collider.gameObject); break;
                case DetectorType.ItemHit:

                    //ToDo:空中か、地上にあるかで気絶、向き反転を決定

                    m_noticeTarget.SendMessage("MoveDirectionChange");
                    m_noticeTarget.SendMessage("LookBack");
                    break;
                case DetectorType.PlayerSearching:
                    
                    m_noticeTarget.SendMessage("OnFoundTarget");

                    break;
            }
        }

    }
}
