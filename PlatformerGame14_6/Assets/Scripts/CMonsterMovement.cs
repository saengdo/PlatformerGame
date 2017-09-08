using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterMovement : CMovement {

    protected float _prevMoveSpeed;         // 이전 몬스터 속도
    protected GameObject _player;           // 플레이어

    protected override void Awake() {
        base.Awake();
        _prevMoveSpeed = _moveSpeed;
    }

    protected virtual void Start()
    {
        // 플레이어를 참조함
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void Move()
    {
        // 이동 상태를 변경함
        _characterState.state = CCharacterState.State.Move;
        if (_animator)
        {
            _animator.SetBool("Move", true);
        }
    }

    protected virtual void Update()
    {
        // 이동
        Move();
    }

    // 공격을 위해 정지함 (속도0)
    public virtual void AttackStop()
    {
        // 정지 속도 설정
        _moveSpeed = 0;
    }

    // 대기 정지
    public virtual void IdleStop()
    {
        // 일시적인 대기 상태 전환
        _characterState.state = CCharacterState.State.Idle;

       // Debug.Break();

        // 정지 속도 설정
        _moveSpeed = 0;
    }

    // 이제 재시작
    public virtual void MoveResume()
    {
        // 속도 재 설정
        _moveSpeed = _prevMoveSpeed;
        // 이동 상태로 다시 변경
        _characterState.state = CCharacterState.State.Move;
    }

    // 지정한 시간을 멈추는 처리
    public virtual void IdleTimeStop(float time)
    {
        // 이미 멈춰 있으면 패쓰
        if (_characterState.state == CCharacterState.State.Idle) return;

        // 멈춰 있지 않으면 지정된 시간만큼 멈출 것
        StartCoroutine("StopIdleDelayCoroutine", time);
    }

    // 이동 정지 지연 코루틴
    public IEnumerator StopIdleDelayCoroutine(float time)
    {
        IdleStop(); // 정지
        // 지연
        yield return new WaitForSeconds(time);
        MoveResume(); // 이동 다시 시작
    }
}
