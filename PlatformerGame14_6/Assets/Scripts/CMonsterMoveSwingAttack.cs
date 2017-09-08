using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMonsterMoveSwingAttack : CMonsterMoveAttack {

    public override void Attack()
    {
        // 공격!!!
        Collider2D collider = Physics2D.OverlapCircle(_attackPoint.position,
            2f, _monsterState._targetMask);

        if (collider == null) return;

        collider.SendMessage("Damage", 1f,
                    SendMessageOptions.DontRequireReceiver);
    }
}
