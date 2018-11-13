using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHelper : MonoBehaviour
{ 
    public static GameHelper Instance;

    public AudioManager Audio;

    [Header("Игровые объекты")]
    [SerializeField]private GameObject _platform;
    [SerializeField]private Image timeIndicator; //полоса времени
    [SerializeField]private GameObject resultPanel; //панель результатов
    [SerializeField]private Joystick joystick; //переменная джойстика
    [SerializeField]private GameObject UIObject;
    [SerializeField]private GameObject skillPlace; // место хранения скиллов
    [SerializeField]private SkillBuilder sBuilder;
    [SerializeField]private SceneLoading loading; //экран загрузки
    [SerializeField]private SkillCoolDown[] btnSkill; //кнопки скиллов
    [SerializeField]private cameraFollow cam; //камера

    [Header("Игровой режим")]
    private int amountKillToWin;
    private float gameTime;
    private float currentGameTime;
    private bool isGameStarted = false;

    [Header("Префабы игроков")]
    [SerializeField]private GameObject[] prefabs; //префабы
    [SerializeField]private GameObject CurrentPlayerGameObject;

    void Awake()
    {
        if (Instance == null) 
        {       
            Instance = this; 
        } 
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
   
        DontDestroyOnLoad(gameObject);

        Audio = GetComponent<AudioManager>();
        btnSkill = FindObjectsOfType<SkillCoolDown>();
        sBuilder = FindObjectOfType<SkillBuilder>();

        Audio.PlayAudio("Final", true);
    }

    void Update()
    {
        GameProcess();
    }
		
	private void InitializeGame(GameMode gameMode)
    {
        amountKillToWin = gameMode.amountKillToWin;
		gameTime = gameMode.gameTime;
		currentGameTime = gameTime;
        UIObject.SetActive(true);
		InitializePlayer ();
        Audio.PlayAudio("RoundBegin", false);
	}

    private void InitializePlayer()
    { //создаем нашего игрока
		CurrentPlayerGameObject = Instantiate (prefabs[sBuilder.characterId], transform.position, Quaternion.identity);
        Debug.Log(CurrentPlayerGameObject);
        PlayerAttack attack = GetComponentInChildren<PlayerAttack>();
		attack.animator = CurrentPlayerGameObject.GetComponent<Animator>();
		attack.wh = CurrentPlayerGameObject.GetComponentInChildren<WeaponHelper>();

		skillPlace.transform.parent = CurrentPlayerGameObject.transform;
        skillPlace.transform.localPosition = new Vector3(0, 1, 1);


		CurrentPlayerGameObject.GetComponent<CharacterMechanics> ().joystick = joystick;

        cam = GameObject.Find("Camera").GetComponent<cameraFollow>();
		cam.character = CurrentPlayerGameObject;
        cam.enabled = true;
        foreach (var skill in sBuilder.abilities)
        {
            Debug.Log(skill.sName);
        }


        int index = 0;
        int length = btnSkill.Length;
		for (int i = 0; i < length; ++i) {
            
            btnSkill [i].player = CurrentPlayerGameObject;
            btnSkill [i].weaponHolder = skillPlace;

            btnSkill [i].ability = sBuilder.abilities;  
                     
            btnSkill [i].SetPlayer ();
		}

        isGameStarted = true;
    }
		
    void GameProcess()
    {
        if (isGameStarted)
        {
            currentGameTime -= Time.deltaTime;
            timeIndicator.fillAmount -= 1.0f / gameTime * Time.deltaTime;
            if (currentGameTime <= 0f)
            {
                EndGame();
                isGameStarted = false;
            }
        }
    }

    void EndGame()
    {
        DisableComponent();
        GameObject gm = GameObject.Find("GUICanvas");
        foreach (Transform obj in gm.transform)
        {
            obj.gameObject.SetActive(false);
        }
        resultPanel.SetActive(true);
        _platform.gameObject.SetActive(true);
        _platform.GetComponentInChildren<AnimationControll>().StartAnim(AnimationControll.animList.AfterGame);
    }

    void DisableComponent()
    { //отключаем компоненты игрок
        CurrentPlayerGameObject.GetComponent<PlayerHP>().enabled = false;
        CurrentPlayerGameObject.GetComponent<CharacterMechanics>().enabled = false;
    }

    public void ButtonToMenu()
    {
        if (sBuilder != null)
        {
            sBuilder.Clear();
        }

        // Destroy prefab 
        Destroy(CurrentPlayerGameObject);

        GameObject gm = GameObject.Find("GUICanvas");
        foreach (Transform obj in gm.transform)
        {
            obj.gameObject.SetActive(false);
        }

        Audio.PlayAudio("Final", true);
        _platform.SetActive(false);
        LoadScene(0, false);
    }

    public void LoadScene(int id, bool isGame)
    {
        loading.success = () =>
        {
            if (isGame)
            {
                InitializeGame(sBuilder.gameMode);
            }  
            loading.gameObject.SetActive(false);
        };
        Audio.StopAudio();
        _platform.SetActive(false);
        loading.sceneId = id;
        loading.gameObject.SetActive(true);
    }
}


