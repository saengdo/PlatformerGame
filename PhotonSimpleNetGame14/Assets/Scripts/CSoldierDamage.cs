using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 용병 데미지 처리
public class CSoldierDamage : Photon.MonoBehaviour {

    private CSoldierStat _stat;         // 용병 상태
    public ParticleSystem _bloodEffect; // 피격 이펙트 파티클
    private CSoldierAnimation _anim;    // 용병 애니메이션

    private void Awake()
    {
        _stat = GetComponent<CSoldierStat>();
        _anim = GetComponent<CSoldierAnimation>();
    }

    // 용병 충돌 이벤트 
    public void OnTriggerEnter(Collider collision)
    {
        if (_stat._state == CSoldierStat.STATE.DEATH) return;

        // 피격된 오브젝트가 총알이고 피격당한 클라이언트가 마스터 클라이언트면
        if(collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);

            if (PhotonNetwork.isMasterClient)
            {
                // 현재 피격당한 오브젝트와 같은 PhotonView를 가진 
                // 네트워크 상의 모든 게임 오브젝트에 TakeDamage RPC 메소드를 호출함
                photonView.RPC("TakeDamage", PhotonTargets.AllViaServer);
            }
        }
    }

    // 용병 피격 RPC 메소드
    [PunRPC]
    public void TakeDamage()
    {
        // 피격 이펙트를 재생함
        _bloodEffect.Play();
    }

}
