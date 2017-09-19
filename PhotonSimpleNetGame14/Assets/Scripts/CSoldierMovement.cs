using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierMovement : Photon.MonoBehaviour {

    private CharacterController _cc;

    private Vector3 _moveDir = Vector3.zero;    // 이동 방향 및 크기

    public float _speed;    //속도
    public float _gravity;  // 중력

    private CSoldierStat _state; //보병 상태

    private CSoldierAnimation _anim; // 보병 애니메이션 제어

    private void Awake()
    {
        _state = GetComponent<CSoldierStat>();
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<CSoldierAnimation>();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        // 이동 방향 벡터
        _moveDir = new Vector3(h, 0f, v);

        // 이동 여부에 따른 보병 애니메이션을 수행함
        if(h != 0 || v != 0)
        {
            _anim.PlayAnimation(CSoldierStat.STATE.MOVE);
        }
        else
        {
            _anim.PlayAnimation(CSoldierStat.STATE.IDLE);
        }


        // 대각선 이동 시 비율 처리
        float speed = _speed;
        if(h != 0 && v != 0)
        {
            float degree = Mathf.Cos(45f * Mathf.Deg2Rad);
            speed = speed * degree;
        }

        // 중력 적용
        _moveDir *= speed;
        _moveDir.y -= _gravity;

        // 이동 수행 (캐릭터 컨트롤러를 이용)
        _cc.Move(_moveDir * Time.deltaTime);

    }

    private void Turn()
    {
        // 카메라에서 마우스 포인터를 향한 레이를 구함
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        // bool 결과 = Physics.Raycast(레이, 충돌 정보, 체크길이, 충돌레이어정보)
        if (Physics.Raycast(camRay, out floorHit, 100f, 1 << LayerMask.NameToLayer("Floor")))
        {
            // out, ref 
            // 데이터 셋을 만들어서 아웃해줘,  내가 만든 데이터 셋을 참조해서 만들어줘.

            // 캐릭터가 마우스 포인터가 가리키는 바닥의 위치를 향한 방향을 구함
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            // 방향을 향한 회전을 구함
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            // 캐릭터를 회전함
            transform.rotation = newRotation;
        
        }

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // PhotonView 컴포넌트의 Controlled Locally 속성이 참이라면
        // 현재 오브젝트가 내가 생성한 오브젝트라면
        if (photonView.isMine)
        {
            Move();
            Turn();
        }
	}
}
