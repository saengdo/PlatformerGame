using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInputMovement : CMovement {

    public bool _isJump = false; // 1단 점프 유무
    public bool _isDoubleJump = false; // 2단 점프 유무

    // 점프 크기(힘)
    public float _jumpPower;

    // 지면 위 여부
    public bool _isGround = false;

    private void Update()
    {
        InputMove(); // 키입력을 통한 이동 처리
        InputJump(); // 키입력을 통한 점프 처리     
         
    }

    private void InputMove()
    {
        float h = Input.GetAxis("Horizontal");

        if ((_characterState._isRightDir && h < 0) ||
            (!_characterState._isRightDir && h > 0))
        {
            Flip(); // 반전하라
        }

        // 캐릭터에 속도를 부여함
        _rigidbody2d.velocity = new Vector2(
            h * _moveSpeed, _rigidbody2d.velocity.y);

        // 수평 이동 방향에 대한 값을 애니메이터에게 넘겨줌
        _animator.SetFloat("Horizontal", Mathf.Abs(h));

        // 상승 및 하강의 속도를 애니메이터에게 넘겨줌
        _animator.SetFloat("Vertical", _rigidbody2d.velocity.y);
    }

    private void Jump()
    {
        // 점프 애니메이션 발동
        _animator.SetTrigger("Jump");

        // 점프 초기화
        _rigidbody2d.velocity = new Vector2(_rigidbody2d.velocity.x, 0f);

        // 점프
        _rigidbody2d.AddForce(Vector2.up * _jumpPower);
    }

    private void InputJump()
    {
        // 왼쪽 컨트롤키를 누르면
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 점프를 안한상태면
            if (!_isJump) // _isJump == false
            {
                Jump(); // 점프 수행
                _isJump = true; // 점프를 한상태로 변경
            }
            // 이미 점프 한 상태에서 2단 점프를 하지 않은 상태면
            else if (_isJump && !_isDoubleJump)
            {
                Jump(); // 점프 수행
                _isDoubleJump = true; // 이단 점프를 한 상태로 변경
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // 캐릭터가 지면에 충돌 했다면
        if (col.gameObject.tag == "Ground")
        {
            GroundSetting(true);
            _isJump = _isDoubleJump = false;
        }
    }

    // 지면 여부 설정
    private void GroundSetting(bool isGround)
    {
        _isGround = isGround;

        // 지면에 닿거나 떨어졌을때 IsGround값을 애니메이터에게 넘겨줌
        _animator.SetBool("IsGround", _isGround);
    }

    // OnCollisionStay2D() : 충돌 중임을 알려주는 이벤트 메소드
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GroundSetting(true);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        // 캐릭터가 지면에서 떨어졌다면
        if (col.gameObject.tag == "Ground")
        {
            GroundSetting(false);
        }
    }
}
