using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterDamage : CCharacterDamage {

    public Transform _damageEffectPos;                  // 타격 이펙트 효과 위치
    public GameObject _damageEffectPrefab;              // 타격 이펙트 프리팹

    public CDamageValueText _damageValueText;

    // 확장 피격 처리
    public override void Damage(float damage)
    {
        base.Damage(damage); // 부모의 데미지 처리 수행

        if(_damageValueText != null)
            _damageValueText.DamageValueShow((damage*100).ToString());

        CMonsterMovement mv = GetComponent<CMonsterMovement>();
        if (mv != null) mv.IdleTimeStop(0.3f);

        // 이펙트가 생성됩니다.
        // 피격 이펙트를 생성
        GameObject effect = Instantiate(_damageEffectPrefab,
            _damageEffectPos.position, Quaternion.identity);
        Destroy(effect, 0.25f);

    }
}
