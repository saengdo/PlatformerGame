using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierShot : Photon.MonoBehaviour {

    private CSoldierStat _stat; // 보병 상태

    // 발포 지연 관련 시간
    public float _shootDelayTime;
    private float _timer;

    public GameObject _bulletPrefab; //총알 프리팹
    public Transform _shotPos; //발포 위치
    public float _shotPower; // 발포 힘

    private void Awake()
    {
        _stat = GetComponent<CSoldierStat>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // 보병이 사망한 상태라면 발포를 할 수 없음
        if (_stat._state == CSoldierStat.STATE.DEATH) return;

        if (photonView.isMine)
        {
            _timer -= Time.deltaTime;

            if (Input.GetButtonDown("Fire1") && _timer < 0)
            {
                // 현재 오브젝트랑 같은 PhotonView 컴포넌트를 가진
                // 오브젝트의 CSoldierShot 컴포넌트의 Shot 메소드를 실행 시킴

                // [실행 범위]
                // PhotonTargets.All : 모두에게 (직접)
                // PhotonTargets.MasterClient : 방장에게만
                // PhotonTargets.Others : 나를 제외한
                // PhotonTargets.AllViaServer : 중계 서버를 통해 동등한 조건으로 모두에게

                photonView.RPC("Shot", PhotonTargets.AllViaServer, _shotPos.position, _shotPos.forward, transform.rotation, photonView.viewID);

                // 발포
                //Shot(_shotPos.position, _shotPos.forward, transform.rotation, 0);
            }
        } 

        
    }
     
    
    // 보병 총알 발포
    [PunRPC]    // Photon Unity Network의 RPC 메소드임을 지정함
    public void Shot(Vector3 pos, Vector3 forawrd, Quaternion qt, int viewId)

    {

        _timer = _shootDelayTime;   // 지연 시간 초기화

        // 총알 생성
        GameObject bullet = Instantiate(_bulletPrefab, pos, qt);
        // 총알 발사
        bullet.GetComponent<Rigidbody>().velocity = forawrd * _shotPower;

        Destroy(bullet, 0.5f);
    }



}
