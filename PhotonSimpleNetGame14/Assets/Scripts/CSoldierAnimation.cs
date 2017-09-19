using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSoldierAnimation : MonoBehaviour {

    private CSoldierStat _stat; // 보병 상태

    private Animator _animator; // 애니메이터

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _stat = GetComponent<CSoldierStat>();

    }

    public bool IsPlayAnimation(string animName, int layer = 0)
    {
        if (_animator.GetCurrentAnimatorStateInfo(layer).IsName(animName))
            return true;

        return false;
    }

    public void PlayAnimation(CSoldierStat.STATE state)
    {
        _stat._state = state;

        switch (state)
        {
            case CSoldierStat.STATE.IDLE:
                _animator.SetBool("Move", false);
                break;
            case CSoldierStat.STATE.MOVE:
                _animator.SetBool("Move", true);
                break;
            case CSoldierStat.STATE.DEATH:
                if (IsPlayAnimation("Death", 0)) break;
                _animator.SetTrigger("Death");
                break;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
