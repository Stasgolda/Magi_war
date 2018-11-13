using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMechanics : MonoBehaviour
{

    public float speedMove;
    public float currentSpeedMove;

    public Joystick joystick;

    private float gravityForce;
    private Vector3 moveVector;
    private Transform ch_Cam;
    private Vector3 ch_CamForward;
    private CharacterController ch_controller;
    private Animator ch_animator;
    private SignalRIdentity _signalRIdentity;

    Vector3 _syncPosition;

    public Vector3 SyncPosition
    {
        get { return _syncPosition; }
        set { _syncPosition = value; }
    }

    Quaternion _syncRotation;

    public Quaternion SyncRotation
    {
        get { return _syncRotation; }
        set { _syncRotation = value; }
    }



    private void Start()
    {
        _signalRIdentity = GetComponent<SignalRIdentity>();

        if (Camera.main != null)
        {
            ch_Cam = Camera.main.transform;
        }

        currentSpeedMove = speedMove;

        ch_controller = GetComponent<CharacterController>();
        ch_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!_signalRIdentity.IsAuthority)
            CharacterMove();

        CharacterGravity();

        if (_signalRIdentity.IsAuthority)
            OtherPlayerMove();
    }

    private void OtherPlayerMove()
    {
        if (_syncPosition == Vector3.zero && _syncRotation == Quaternion.identity)
        {
            return;
        }
        if (ch_controller.isGrounded)
        {

            moveVector = Vector3.zero;

            moveVector = _syncPosition - transform.position;
            if (moveVector.magnitude > 1f)
            {
                moveVector = moveVector.normalized * currentSpeedMove;
            }

            if (moveVector.x != 0 || moveVector.z != 0)
            {
                ch_animator.SetBool("Move", true);
            }
            else
            {
                ch_animator.SetBool("Move", false);
            }

            transform.rotation = _syncRotation;
        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);
    }

    private void CharacterMove()
    {
        if (_syncPosition == Vector3.zero && _syncRotation == Quaternion.identity)
        {
            return;
        }
        if (ch_controller.isGrounded)
        {

            moveVector = Vector3.zero;

            float h = joystick.Horizontal * currentSpeedMove;
            float v = joystick.Vertical * currentSpeedMove;

            ch_CamForward = Vector3.Scale(ch_Cam.forward, new Vector3(1, 0, 1)).normalized;
            moveVector = v * ch_CamForward + h * ch_Cam.right;

            if (moveVector.x != 0 || moveVector.z != 0)
            {
                ch_animator.SetBool("Move", true);
            }
            else
            {
                ch_animator.SetBool("Move", false);
            }


            if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
                transform.rotation = Quaternion.LookRotation(direction);
            }

        }

        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);

        _syncPosition = transform.position;
        _syncRotation = transform.rotation;
    }

    private void CharacterGravity()
    {
        if (!ch_controller.isGrounded)
        {
            gravityForce -= 30f * Time.deltaTime;
        }
        else
            gravityForce = -1f;

    }

    public void Jump(float force)
    {
        if (ch_controller.isGrounded)
        {
            gravityForce = force;
        }
    }
}
