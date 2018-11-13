using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMechanics : MonoBehaviour
{

    [SerializeField]private float speedMove;
    public float currentSpeedMove;

	[HideInInspector]
    public Joystick joystick;

    private float gravityForce;
    private Vector3 moveVector;
    private Transform ch_Cam;
    private Vector3 ch_CamForward;

    private CharacterController ch_controller;
    private AnimationControll ch_animator;
    private AudioManager _audio;

    private void Start()
    {
        if (Camera.main != null)
        {
            ch_Cam = Camera.main.transform;
        }

        currentSpeedMove = speedMove;

        _audio = GetComponent<AudioManager>();
        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<AnimationControll>();

        //ch_animator.SetFloat("Idle", Random.Range(0,2));
    }

    private void FixedUpdate()
    {
        CharacterMove();
        CharacterGravity();
    }
		
    private void CharacterMove()
    {
        if (ch_controller.isGrounded)
        {
            moveVector = Vector3.zero;

            float h = joystick.Horizontal * currentSpeedMove;
            float v = joystick.Vertical * currentSpeedMove;

            ch_CamForward = Vector3.Scale(ch_Cam.forward, new Vector3(1, 0, 1)).normalized;
            moveVector = v * ch_CamForward + h * ch_Cam.right;

            if (moveVector.x != 0 || moveVector.z != 0)
            {
                ch_animator.StartAnim(AnimationControll.animList.Move);
                int rand = Random.Range(1,5);
                _audio.PlayAudio(_audio._audioClips[rand].name, false);
            }
            else
            {
                ch_animator.EndAnim(AnimationControll.animList.Move);
            }


            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }

        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.fixedDeltaTime);
    }

    private void CharacterGravity()
    {
        if (!ch_controller.isGrounded)
        {
            ch_animator.StartAnim(AnimationControll.animList.Fall);
            gravityForce -= 30f * Time.fixedDeltaTime;
        }
        else
        {
            gravityForce = -1f;
            ch_animator.EndAnim(AnimationControll.animList.Fall);
        }

    }

    public void Jump(float force)
    {
        if (ch_controller.isGrounded)
        {
            gravityForce = force;
        }
    }
}
