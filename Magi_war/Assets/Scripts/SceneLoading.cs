using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {

    public delegate void OnComplete();
    public OnComplete success;

	public int sceneId; 

	public Image loadImg;
	public Text loadTxt;

	void OnEnable () {
		StartCoroutine (AsyncLoad ());
	}
	
	IEnumerator AsyncLoad () {
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneId);

		while (!operation.isDone) {
			float progress = operation.progress / 0.9f;
			loadImg.fillAmount = progress;
			loadTxt.text = string.Format ("{0:0}%", progress * 100);
			yield return null;
		}
        if (success != null)
        {
            success();
        }
	}
}
