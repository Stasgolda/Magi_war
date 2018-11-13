using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace ProceduralToolkit.Examples
{
    public class MainMenuNavigation : UIBehaviour, IDragHandler
    {
        // переменные для вращения персонажа в главном меню
        [Header("Переменные для вращения персонажа в главном меню")]
        public Transform playerPlatformTransform;
        private float lookAngle;
        private Quaternion rotation;
        public float rotationSpeed = 20;
        //

        // переменные скилл билдера
        [Header("Переменные скилл билдера")]
		public Button[] abilityButtons; // кнопки выбранных скиллов
        public Sprite[] skillSprites; // спрайты скиллов
        public Skill[] abilities;		 // список всех умений
		public GameObject[] selectedSkillsImages; // изображения выбранных скиллов
        public Transform content; // кнопки выбора скиллов в скилл билдере

		public List<int> selectedSkills; // выбранные пользователем скиллы
        private SkillBuilder sBuilder;
        //
        [Header("Переменные кастомизации персонажа")]
        int selectedCharacterId = 0;                    // ID выбранного перса
        public GameObject characterSettings;            // гуишка с выбором и настройкой перса
        public List<GameObject> charactersPrefabs;      // префабы превьюшек персов
        public GameObject characterName;

        [Header("Остальные переменные")]
        public GameObject errorWindow;                  // окно ошибки
        public GameObject gamemodeSelector;             // окно выбора игрового режима
        public GameObject findGamePanel;                // окошко поиска игры
        private bool isSelectorActive;                  // проверяем, нажата ли кнопка Play
        public GameObject PlayButton;                   // кнопка Play в меню
		private int currentLevel;						// уровень нашего игрока
		private int maxAmountSkill;                     // максимальное кол-во скиллов на данный момент
        public GameObject mainMenuBackground;           // задний фон главного меню

        protected override void Awake()
        {
            base.Awake();        
            sBuilder = FindObjectOfType<SkillBuilder>();

            InitializeButtons();
            RefreshPlayerModel();
            checkSkillsOnStart();
            SetMaxSkillAmount();
           
            if (playerPlatformTransform == null)
                return;
            playerPlatformTransform.rotation = rotation = Quaternion.Euler(0f, 0f, 0f);
            isSelectorActive = false;    
        }

        public void OnDrag(PointerEventData eventData) //реагируем на свайпы
        {
            if (!playerPlatformTransform) return;
            if (!isSelectorActive)
            {
                lookAngle -= eventData.delta.x * 0.5f;
                rotation = Quaternion.Euler(0f, lookAngle, 0f);
            }
        }

        private void LateUpdate()
        {
            if (!playerPlatformTransform) return;

            if (playerPlatformTransform.rotation != rotation) //поворачиваем модельку
            {
                playerPlatformTransform.rotation = Quaternion.Lerp(playerPlatformTransform.rotation, rotation,
                    Time.deltaTime * rotationSpeed);
            }
        }

        //МЕТОДЫ СКИЛЛБИЛДЕРА

		private void SetMaxSkillAmount () { // высчитываем сколько скиллов можем взять
			currentLevel = PlayerPrefs.GetInt ("Level");
			for (int i = 1; i <= currentLevel; i++) {
				if (i % 3 == 0) {
					maxAmountSkill++;
				}
			}
			maxAmountSkill += 3;

			for (int i = 0; i < maxAmountSkill; i++) { 
				abilityButtons [i].interactable = true;
			}

            sBuilder.maxAmountSkill = maxAmountSkill;
		}

        private void InitializeButtons()
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                content.GetChild(i).GetChild(0).GetComponent<Image>().sprite = abilities[i].sSprite;
                content.GetChild(i).GetChild(1).GetComponent<Text>().text = abilities[i].sName;
                content.GetChild(i).GetComponent<SkillHolder>().item = abilities[i];
				skillSprites[i] = abilities[i].sSprite;
            }
        }

        public void selectSkillInList(int skillID)
        {

            if (selectedSkills.Count < maxAmountSkill)
            {
                if (!selectedSkills.Contains(skillID))
                {
                    selectedSkills.Add(skillID);

                    content.GetChild(skillID).transform.GetChild(2).gameObject.SetActive(true);
                }
            }
            else
            {
                showError(0);
            }
            UpdateSelectedSkills();
        }

        public void removeSkillFromSelected(int slotID)
        {
            if (selectedSkills.Count > slotID)
            {
                selectedSkills.RemoveAt(slotID);
            }
            UpdateSelectedSkills();
        }

        public void UpdateSelectedSkills()
        {
            for (int b = 0; b < content.childCount; b++)
            {
                content.GetChild(b).transform.GetChild(2).gameObject.SetActive(selectedSkills.Contains(b));
            }
            for (int i = 0; i < selectedSkillsImages.Length; i++)
            {
                selectedSkillsImages[i].GetComponent<Image>().enabled = false;
            }
            for (int s = 0; s < selectedSkills.Count; s++)
            {
                selectedSkillsImages[s].GetComponent<Image>().enabled = true;
                selectedSkillsImages[s].GetComponent<Image>().sprite = skillSprites[selectedSkills[s]];
            }
        }
        void checkSkillsOnStart()
        {
            for (int i = 0; i < selectedSkillsImages.Length; i++)
            {
                if (PlayerPrefs.HasKey("skill_slot" + i))
                {
                    selectedSkills.Add(PlayerPrefs.GetInt("skill_slot" + i));
                }
            }
            UpdateSelectedSkills();
        }

        //

        //РАЗДЕЛ КАСТОМИЗАЦИИ ПЕРСОНАЖА
        public void customizeCharacter()
        {
            if (!Camera.main.GetComponent<Animation>().isPlaying)
            {
                if (!characterSettings.activeSelf)
                {
                    Camera.main.GetComponent<Animation>().Play("camera_selectChar");
                    mainMenuBackground.SetActive(false);
                    characterSettings.SetActive(true);
                }
                else
                {
                    Camera.main.GetComponent<Animation>().Play("camera_mainMenuFromCharSelect");
                    characterSettings.SetActive(false);
                    mainMenuBackground.SetActive(true);
                }
            }
        }
        public void selectPreviousCharacter()
        {
            selectedCharacterId--;
            if (selectedCharacterId < 0)
            {
                selectedCharacterId = charactersPrefabs.Count - 1;
            }
            RefreshPlayerModel();
        }
        public void selectNextCharacter()
        {
            selectedCharacterId++;
            if (selectedCharacterId >= charactersPrefabs.Count)
            {
                selectedCharacterId = 0;
            }
            RefreshPlayerModel();
        }
        public void RefreshPlayerModel()
        {
            if (playerPlatformTransform.transform.GetChild(0).gameObject) {
                Destroy(playerPlatformTransform.transform.GetChild(0).gameObject);
            }
            GameObject obj = Instantiate(charactersPrefabs[selectedCharacterId], playerPlatformTransform);
            obj.transform.localScale = new Vector3(1, 15, 1);
            foreach (MonoBehaviour c in obj.GetComponents<MonoBehaviour>()) 
            {
                c.enabled = false;
            }
            characterName.GetComponent<Text>().text = charactersPrefabs[selectedCharacterId].name;
            if(characterName.GetComponent<Animation>().isPlaying)
            {
                characterName.GetComponent<Animation>().Stop();
            }
            characterName.GetComponent<Animation>().Play("showingCharacterName");
        }
        //

        public void show_hide_gamemodeSelector() //когда нажимаем кнопку play либо прячем либо показываем меню выбора режима игры
        {
            if (selectedSkills.Count == 3)
            {
                if (!isSelectorActive)
                {
                    StartCoroutine(gamemodeSelectorAnimation(0));
                }
                else
                {
                    StartCoroutine(gamemodeSelectorAnimation(1));
                }
            }
            else
            {
                showError(1);
            }
        }

        IEnumerator gamemodeSelectorAnimation(int animID) //воспроизводим анимацию панели выбора режима
        {
            string[] ClipNames = {"show_gamemodesMenu", "hide_gamemodesMenu"}; //список имеющихся анимаций
            if (!gamemodeSelector.GetComponent<Animation>().isPlaying)
            {
                isSelectorActive = (animID == 0);
                if (isSelectorActive)
                {
                    PlayButton.transform.GetChild(0).GetComponent<Text>().text = "CLOSE";
                }
                else
                {
                    PlayButton.transform.GetChild(0).GetComponent<Text>().text = "PLAY";
                }
                gamemodeSelector.GetComponent<Animation>().Play(ClipNames[animID]);
            }
            yield return null;
        }

        public void showError(int errorID)
        {
            string errorText;
            switch (errorID)
            {
                case 0:
                    errorText = "Максимум может быть выбрано " + maxAmountSkill + " скилла!";
                    break;
                case 1:
                    errorText = "Для начала игры должно быть выбрано 3 скилла персонажа!";
                    break;
                default:
                    errorText = "unknown error!";
                    break;
            }
            errorWindow.SetActive(true);
            errorWindow.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = errorText;
        }

        public void closeErrorWindow()
        {
            errorWindow.SetActive(false);
        }

        public void exitApp()
        {
            Application.Quit();
        }

        public void findGame(GameMode gameMode)                                   //нажимаем кнопку найти игру
        {
            findGamePanel.SetActive(true);
            GameObject timer = findGamePanel.transform.GetChild(0).gameObject;
            GameObject gamemodeText = findGamePanel.transform.GetChild(1).gameObject;
            string[] gamemodesList = {"DEATHMATCH","TIME MATCH"};

            timer.GetComponent<Text>().text = "0:00";
            gamemodeText.GetComponent<Text>().text = gameMode.gmName;
            StartCoroutine(findingGame(timer, gameMode, 0));
            show_hide_gamemodeSelector();
        }

        IEnumerator findingGame(GameObject timer, GameMode gamemode, int sec)     //поиск игры
        {
            int minutes = sec / 60;
            int seconds = sec % 60;
            timer.GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);
            int gameFinded = Random.Range(0, 1);
            Debug.Log(gameFinded);
            yield return new WaitForSeconds(1f);
            if (gameFinded >= 1)
            {
                StartCoroutine(findingGame(timer, gamemode, seconds+1));
            } else
            {
                findGamePanel.transform.GetChild(2).gameObject.SetActive(true);
                yield return new WaitForSeconds(1f);
                findGamePanel.transform.GetChild(2).gameObject.SetActive(false);
                startGame(gamemode);
            }
        }

        public void cancelSearch ()                                           //отмена поиска
        {
            GameObject timer = findGamePanel.transform.GetChild(0).gameObject;

            StopAllCoroutines();
            findGamePanel.SetActive(false);
        }

        void startGame(GameMode gmMode)                                          //игра найдена, стартуем
        {
            sBuilder.gameMode = gmMode;
			SaveChoose ();
            findGamePanel.SetActive(false);  
			mainMenuBackground.SetActive (false);

            GameHelper.Instance.LoadScene(1, true);
        }

		private void SaveChoose () { 
			for (int s = 0; s < selectedSkills.Count; s++)
			{
				PlayerPrefs.SetInt("skill_slot" + s, selectedSkills[s]);
			}

			foreach (int id in selectedSkills)
			{
                sBuilder.AddSkill(content.GetChild(id).GetComponent<SkillHolder>().item);
			}

			sBuilder.characterId = selectedCharacterId;
		}

    }
}
