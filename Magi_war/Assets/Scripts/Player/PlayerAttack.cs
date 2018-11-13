using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public Animator animator;
	public WeaponHelper wh;

	[SerializeField]private float timer;


	void FixedUpdate () {
        if (animator)
        {
            if (animator.GetBool("Attack") == true)
            {
                StartCoroutine(EndAnim(0.5f));
            }
        }
	}

	public void commonAttack () {
		if (timer == 0f) {
			animator.SetBool ("Attack", true);
            GameHelper.Instance.Audio.PlayAudio("Kick", false);
			wh.isAttack = true;

		}
	}

	IEnumerator EndAnim (float time) {
		timer += Time.deltaTime;
		if (timer >= time) {
			timer = 0f;
			animator.SetBool ("Attack", false);
			wh.isAttack = false;
			yield return null;
		}
	}
}
