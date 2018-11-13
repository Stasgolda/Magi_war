using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerHP : MonoBehaviour
{
    //хп бар персонажа
    public Slider HPSlider;

    private Transform HPBarPlace;
    private Slider MyHP;

    public int maxHP = 100;
    private int currentHP;

    private bool isStunned;
    private float durationStun;

    public bool isDead = false;

    private AnimationControll _animator;

    void Start()
    {
        _animator = GetComponent<AnimationControll>();
        HPBarPlace = transform.Find("HPBarPlace").transform;
        currentHP = maxHP;
    }

    public void takeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Death();
        }
        //_animator.StartAnim();
    }

    public void takeHealth(int health)
    {
        currentHP += health;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void Stun(float duration)
    {
        durationStun = duration;
        isStunned = true;
        Debug.Log("Stunned");
    }

    private void Death()
    {
        GameHelper.Instance.Audio.PlayAudio("Dead", false);
        isDead = true;
        DiactivateComponents();
        _animator.StartAnim(AnimationControll.animList.Death);
        /* Destroy(MyHP.gameObject);
        Destroy(gameObject);*/
    }

    private void FixedUpdate()
    {
        if (MyHP)
        {
            if (currentHP > 50f)
            {
                MyHP.transform.Find("Fill Area").GetComponentInChildren<Image>().color = new Color(0, 255, 12);
            }
            else if (currentHP < 50 && currentHP > 25)
            {
                MyHP.transform.Find("Fill Area").GetComponentInChildren<Image>().color = new Color(255, 165, 0);
            }
            else if (currentHP <= 25)
            {
                MyHP.transform.Find("Fill Area").GetComponentInChildren<Image>().color = new Color(255, 0, 0);
            }
        }
        if (isStunned)
        {
            durationStun -= Time.deltaTime;
            if (durationStun <= 0f)
            {
                print("Normal");
                isStunned = false;
            }
        }
        if (!MyHP)
        {
            MyHP = (Slider)Instantiate(HPSlider, GameObject.Find("GUICanvas").transform);
            MyHP.maxValue = maxHP;
            MyHP.value = currentHP;
            //hpBar.transform.localPosition = new Vector3(0f,1.5f,0f);
        }
        else
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(HPBarPlace.position);
            MyHP.transform.position = screenPos;
            MyHP.value = currentHP;
        }
    }

    void DiactivateComponents()
    {
        enabled = false;
        GetComponent<PlayerXP>().enabled = false;
        GetComponent<PlayerAttack>().enabled = false;
        GetComponent<CharacterMechanics>().enabled = false;
    }
}
