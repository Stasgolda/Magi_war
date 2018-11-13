using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillCoolDown : MonoBehaviour {

	public Transform ui;
	public SkillCoolDown[] buttons; //другие кнопки скиллов

	//переменные игрока
	public GameObject directionalArrow;
	public GameObject player;
	public AnimationControll animationControll;
	public Animator animator;
	public CharacterMechanics cm;
	public DrawRange lineRange;

	public List<Skill> ability;

	//переменные кнопки
	public Image darkMask;
	public Text coolDownTextDisplay;
	public GameObject weaponHolder;
	private Image myButtonImage;
	private AudioSource abilitySource;
	private float coolDownDuration;
	private float nextReadyTime;
	private float coolDownTimeLeft;

	//переменные для скилла
	[SerializeField]private bool isTargetSkillActive;


	public void SetPlayer () 
	{
		lineRange = player.transform.Find ("DrawRange").GetComponent<DrawRange> ();
		directionalArrow = player.transform.Find ("Arrow").gameObject;
		animationControll = player.GetComponent<AnimationControll> ();
		animator = player.GetComponent<Animator> ();
		cm = player.GetComponent<CharacterMechanics> ();

		ui = GameObject.Find ("GUICanvas").transform;
		buttons = FindObjectsOfType<SkillCoolDown> ();
		Initialize (ability, weaponHolder); 
	}

	public void Initialize(List<Skill> abilities, GameObject weaponHolder)
	{
		Skill ability = abilities [0];
        myButtonImage = GetComponent<Image> ();
        Debug.Log(myButtonImage);
        myButtonImage.sprite = ability.sSprite;
			
        foreach (Skill item in abilities)
        {
            abilitySource = GetComponent<AudioSource> ();
		    darkMask.sprite = ability.sSprite;
		    coolDownDuration = ability.coolDown;
			ability.Initialize (weaponHolder);
		}
		AbilityReady ();
	}

	void Update () 
	{
		bool coolDownComplete = (Time.time > nextReadyTime);
		if (coolDownComplete) 
		{
			AbilityReady ();
		} else 
		{
			CoolDown();
		}

		if (isTargetSkillActive) {
			GetPositionTap ();
		}

	}

	private void AbilityReady()
	{
		coolDownTextDisplay.enabled = false;
		darkMask.enabled = false;
	}

	private void CoolDown()
	{
		coolDownTimeLeft -= Time.deltaTime;
		float roundedCd = Mathf.Round (coolDownTimeLeft);
		coolDownTextDisplay.text = roundedCd.ToString ();
		darkMask.fillAmount = (coolDownTimeLeft / coolDownDuration);
	}

	private void ButtonTriggered()
	{
		nextReadyTime = coolDownDuration + Time.time;
		coolDownTimeLeft = coolDownDuration;
		darkMask.enabled = true;
		coolDownTextDisplay.enabled = true;

		//abilitySource.clip = ability.sSound;
		//abilitySource.Play ();
		animator.SetBool("Skill", true);

		for (int i = 0; i < buttons.Length; i++) {
			buttons [i].GetComponent<Button> ().interactable = true;
		}
	}

	private void GetPositionTap () {
		if ((Input.touchCount > 0)) {
			for (int i = 0; i < Input.touchCount; i++) {
				if (!EventSystem.current.IsPointerOverGameObject (i)) {
					if (Input.GetTouch (i).phase == TouchPhase.Began) {
						Ray raycast = Camera.main.ScreenPointToRay (Input.GetTouch (i).position);
						RaycastHit raycastHit;
						if (Physics.Raycast (raycast, out raycastHit)) {
							float distance = Vector3.Distance (raycastHit.point, player.transform.position);
							if (distance <= ability[0].range) {
								PlayerHP health = raycastHit.collider.GetComponent<PlayerHP> ();
								if (health && health.tag == "Enemy") {
									TargetSkill targetAbility = ability[0] as TargetSkill;
									if (targetAbility) {
                                        foreach (var skill in ability)
                                        {
                                            if (skill is TargetSkill)
                                            {
                                                TargetSkill targetSkill = skill as TargetSkill;
                                                targetSkill.UseAbility(raycastHit);
                                            }
                                            else
                                            {
                                                NonTargetSkill nonTargetSkill = skill as NonTargetSkill;
                                                nonTargetSkill.UseAbility();
                                            }
                                        }										
										player.transform.LookAt (raycastHit.point);
										UseTargetSkill ();
									}
								}
							}
						}				
					}
				}
			}
		}
	}

	private void UseTargetSkill () {
		Vector3 tmp = player.transform.eulerAngles;
		tmp.x = 0f;
		player.transform.eulerAngles = tmp;
		ButtonTriggered ();
		cm.currentSpeedMove = 0f;
		isTargetSkillActive = false;
		directionalArrow.SetActive (false);
		lineRange.enabled = false;
	}

	public void PrepareToSkill () {
		bool coolDownComplete = (Time.time > nextReadyTime);
		if (isTargetSkillActive) {
			directionalArrow.SetActive (false);
			lineRange.enabled = false;
			isTargetSkillActive = false;
			for (int i = 0; i < buttons.Length; i++) {
				buttons [i].GetComponent<Button> ().interactable = true;
			}
			return;
		}

		if(coolDownComplete){
			if (ability is NonTargetSkill) 
            {
                foreach (var skill in ability)
                {
                    if (skill is NonTargetSkill)
                    {
                        NonTargetSkill nonTargetSkill = skill as NonTargetSkill;
                        nonTargetSkill.UseAbility();
                    }
                }
				ButtonTriggered ();			 
			} 
            else 
            {
				lineRange.enabled = true;
				lineRange.CreatePoints (ability[0].range);
				directionalArrow.SetActive (true);
				directionalArrow.transform.localScale = new Vector3 (ability[0].range / 5, transform.localScale.y, transform.localScale.z); 
				isTargetSkillActive = true;
				for (int i = 0; i < buttons.Length; i++) {
					if (buttons [i].ability != ability) {
						buttons [i].GetComponent<Button> ().interactable = false;
					}
				}
			}
		}
	}
}
