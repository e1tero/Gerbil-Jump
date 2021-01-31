using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHelper : MonoBehaviour
{
    public Player player;
    public Text points;
    public Text currentPoints;
    public Text recordPoints;
    public SaveData saveData;
    public GameObject panelUI;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "DeathScene")
            StartCoroutine(OffPanelUI());
    }
    
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
            points.text = player.points.ToString();
        else if (SceneManager.GetActiveScene().name == "DeathScene")
        {
            currentPoints.text = saveData.currentPoints.ToString();
            recordPoints.text = saveData.recordPoints.ToString();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator OffPanelUI()
    {
        yield return new WaitForSeconds(1f);
        panelUI.SetActive(false);
    }
    
}
