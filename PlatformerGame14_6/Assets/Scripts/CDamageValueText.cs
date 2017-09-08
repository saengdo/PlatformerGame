using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CDamageValueText : MonoBehaviour {

    Animator _animator;
    Text _damageValueText;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _damageValueText = GetComponentInChildren<Text>();
    }

    public void DamageValueShow(string DamageValue)
    {
        // 데미지 글자 변경
        _damageValueText.text = DamageValue;

        // 데미지 표시 애니메이션 재생
        _animator.Play("DamageValueShow");
    }
}
