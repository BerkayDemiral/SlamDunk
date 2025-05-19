using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("LEVEL OBJECT")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Hoop;
    [SerializeField] private GameObject GrowHoopObject;
    [SerializeField] private GameObject[] FeatureLocations;
    [SerializeField] private AudioSource[] Sounds;
    [SerializeField] private ParticleSystem[] Effects;


    [Header("UI OBJECT")]
    [SerializeField] private Image[] ImageMissions;
    [SerializeField] private Sprite ImageTickMissions;
    [SerializeField] private int TargetBallCount;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI LevelName;

    int BasketCount;
    float FingerPosX;

    void Start()
    {
        LevelName.text= "LEVEL: " + SceneManager.GetActiveScene().name;

        for (int i = 0; i < TargetBallCount; i++)
        {
            ImageMissions[i].gameObject.SetActive(true);
        }

        Invoke("SpawnFeature", 3f);
    }

    void SpawnFeature()
    {
        int RandomCount = Random.Range(0, FeatureLocations.Length-1);

        GrowHoopObject.transform.position = FeatureLocations[RandomCount].transform.position;
        GrowHoopObject.SetActive(true);
    }


    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        FingerPosX = TouchPosition.x - Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if (TouchPosition.x - FingerPosX > -1 && TouchPosition.x - FingerPosX < 1)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x - FingerPosX, Platform.transform.position.y, Platform.transform.position.z), 5f);
                        }
                        break;
                }
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Platform.transform.position.x > -1)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x - .3f, Platform.transform.position.y, Platform.transform.position.z), .050f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Platform.transform.position.x < 1)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .3f, Platform.transform.position.y, Platform.transform.position.z), .050f);
            }
        }
    }

    public void Basket(Vector3 Pos)
    {
        BasketCount++;
        ImageMissions[BasketCount - 1].sprite = ImageTickMissions;
        Effects[0].transform.position = Pos;
        Effects[0].gameObject.SetActive(true);
        Sounds[1].Play();

        if (BasketCount == TargetBallCount)
        {
            Win();
        }
    }

    public void Win()
    {
        Sounds[2].Play();
        Panels[1].SetActive(true);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        Time.timeScale = 0;
    }
    public void Lose()
    {
        Sounds[3].Play();
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }

    public void GrowHoop(Vector3 Pos)
    {
        Effects[1].transform.position = Pos;
        Effects[1].gameObject.SetActive(true);
        Sounds[0].Play();
        Hoop.transform.localScale = new Vector3(55f, 55f, 55f);
    }

    public void PanelButtonAction(string ButtonAction)
    {
        switch (ButtonAction)
        {
            case "Pause":
                Time.timeScale = 0;
                Panels[0].SetActive(true);
                break;

            case "Resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;

            case "Retry":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            
            case "Settings":
                //Im just coding the game ^_^
                break;
            
            case "Next":
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;

            case "Quit":
                Application.Quit();
                break;
        }
    }

}
