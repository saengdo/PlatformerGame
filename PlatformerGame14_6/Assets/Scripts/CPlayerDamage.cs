using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerDamage : CCharacterDamage {

    // 몬스터의 체력을 감소함
    public override void Damage(float damage)
    {
        if (_characterState.state == CCharacterState.State.Damage) return;

        base.Damage(damage);


        // 플레이어 캐릭터의 상태를 데미지 상태로 변경함
        _characterState.state = CCharacterState.State.Damage;

    }

    // 데미지 애니메이션이 끝났을때 호출되는 이벤트 메소드
    public void DamageAnimationEndEvent()
    {
        // 캐릭터의 상태를 대기 상태로 변경
        _characterState.state = CCharacterState.State.Idle;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            Damage(1f);
        }
    }

    // 충돌 오브젝트와 충돌중임을 알려주는 이벤트 메소드
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Damage(1f);
        }
    }
}
