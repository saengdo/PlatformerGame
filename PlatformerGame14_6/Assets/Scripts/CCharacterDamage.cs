using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterDamage : MonoBehaviour {

    protected CCharacterState _characterState;            // 캐릭터 상태
    protected Animator _animator;                       // 애니메이터

    protected virtual void Awake()
    {
        _characterState = GetComponent<CCharacterState>();
        _animator = GetComponent<Animator>();
    }

    // 피격 처리
    public virtual void Damage(float damage)
    {
        // 몬스터의 체력을 감소함
        if (_characterState.HpDown(damage) <= 0)
        {
            return;
        }

        // int 레이어인덱스 = Animator.GetLayerIndex("레이어이름");
        int layerIndex = _animator.GetLayerIndex("Damage Layer");

        // 피격 애니메이션 재생
        _animator.Play("Damage", layerIndex);
    }
}
