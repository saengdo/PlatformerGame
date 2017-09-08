﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterState : MonoBehaviour {

    public LayerMask _targetMask;       // 충돌 레이어
    public int _hp;                   // 체력 상태
    public bool _isDie = false;         // 사망 여부
    public bool _isRightDir = false;    // 시선

    // 상태 열거형 타입을 선언함
    public enum State { Idle, Move, Attack, Damage, Die };

    // 상태 열거형 타입 변수 선언
    public State _state;

    private CDestroyer _destroyer;

    private void Awake()
    {
        _destroyer = GetComponent<CDestroyer>();
    }

    // 캐릭터 상태 프로퍼티
    public State state
    {
        get { return this._state; }
        set { this._state = value; }
    }

    public float HpDown(float damage)
    {
        _hp -= (int)damage;

        if (_hp <= 0)
        {
            _destroyer.Destroy();
        }

        return _hp;
    }
}
