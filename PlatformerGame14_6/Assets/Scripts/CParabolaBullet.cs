using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParabolaBullet : CBullet {
    public float _damageRange;      // 공격 범위
    public float _shotForce;        // 발포 힘

    public LayerMask _targetMask;   // 공격 타겟 충돌 레이어
  
    // 포탄 이동을 처리함
    public override void Move()
    {
        // 지정한 방향과 속도로 총알이 이동함
        _rigidbody2d.AddForce(new Vector2(1f * _dirValue, 1f) * _shotForce);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        // 스플래시 데미지를 처리함
        Collider2D collider = Physics2D.OverlapCircle(transform.position, _damageRange, _targetMask);

        if(collider != null)
        {
            collider.SendMessage("Damage", 1f, SendMessageOptions.DontRequireReceiver);
        }
    }

}
