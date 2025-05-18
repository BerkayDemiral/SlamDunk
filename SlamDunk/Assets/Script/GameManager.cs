using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Platform;

    [SerializeField] private Image[] ImageMissions;
    [SerializeField] private Sprite ImageTickMissions;
    [SerializeField] private int TargetBallCount;
    int BasketCount;

    void Start()
    {
        for (int i = 0; i < TargetBallCount; i++)
        {
            ImageMissions[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
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

    public void Basket()
    {
        BasketCount++;
        ImageMissions[BasketCount - 1].sprite = ImageTickMissions;

        if (BasketCount == TargetBallCount)
        {
            //Panel Will Appear
            //Level Number Will Be Updated
            //Points Will Be Awarded

            Debug.Log("Win");
        }
    }

    public void Lose()
    {
        Debug.Log("Lose");
    }

}
