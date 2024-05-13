using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject leftRayController;
    [SerializeField] GameObject rightRayController;
    private bool carClear;
    private int score;
    private float timer;
    private bool isMenuOn = false;
    DataManager dataManager;
    void Start()
    {
        score = 0;
        dataManager = FindAnyObjectByType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
        {
            if (isMenuOn)
            {
                isMenuOn = false;
                menu.SetActive(false); leftRayController.SetActive(false); rightRayController.SetActive(false);
            }
            else
            {
                isMenuOn = true;
                menu.SetActive(true); leftRayController.SetActive(true); rightRayController.SetActive(true);
            }
        }
        if (score > 10)
        {
            dataManager.SetClear();
            //carClear = true;
        }
    }
    public void ScorePlus()
    {
        score++;
    }
    public int GetScore()
    {
        return score;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("CityDesign");
    }
}
