using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//вспомогательный скрипт для SkillCoolDown, для отключения анимации и доп.опций
public class AnimationControll : MonoBehaviour {

    public enum animList
    {
        Idle,
        Move,
        Fall,
        Death,
        AfterGame
    }
        	
	private Animator animator;
	private float Timer;

	void Start () {
		animator = GetComponent<Animator> ();

        StartAnim(animList.Idle);
	}

    public void EndAnim (animList anim) 
    {
        int rand = Random.Range(0,2);
        switch(anim)
        {
            case animList.Idle:  
                animator.SetFloat("Idle Blend Tree", -1);
                break;
            case animList.Move:
                animator.SetBool("Move", false);
                break;
            case animList.Fall:
                animator.SetBool("Fall", false);
                break;
        }
	}
        
    public void StartAnim(animList anim)
    {
        int rand = Random.Range(0,2);
        switch(anim)
        {
            case animList.Idle:  
                animator.SetFloat("Idle", rand);
                break;
            case animList.Move:
                animator.SetBool("Move", true);
                break;
            case animList.Fall:
                animator.SetBool("Fall", true);
                break;
            case animList.Death:
                animator.SetBool("Dead", true);
                break;
            case animList.AfterGame:
                animator.SetBool("AfterGame", true);
                break;
        }
    }
}
