﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour {

    #region UnityCompliant Singleton
    public static SceneLoading Instance
    {
        get;
        private set;
    }

    public virtual void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    #endregion

    [SerializeField]
    Image progressBarFull;

    [SerializeField]
    Text progressText;

    [SerializeField]
    Image blackScreen;

    [SerializeField]
    CanvasGroup progressGroup;

    [SerializeField]
    GameObject LoadingScreenGroup;

    public Image getBlackScreen()
    {
        return blackScreen;
    }

    public void loadingScreenGroup(bool active)
    {
        LoadingScreenGroup.SetActive(active);
    }

    public void loadScene(string sceneToLoad)
    {
        loadingScreenGroup(true);
        progressGroup.alpha = 1;
        StartCoroutine(waitForSceneToLoad(sceneToLoad));
    }

    private IEnumerator waitForSceneToLoad(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        int progress;
        while (!asyncLoad.isDone)
        {
            progressBarFull.fillAmount = asyncLoad.progress;
            progress = (int)(asyncLoad.progress * 100);
            progressText.text = progress.ToString() + "%";
            yield return null;
        }
        progressGroup.alpha = 0;
        Time.timeScale = 1;
        loadingScreenGroup(false);
    }
}
