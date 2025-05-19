using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrowHoop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Time;
    [SerializeField] private int StartTime;
    [SerializeField] private GameManager _GameManager;

    IEnumerator Start()
    {
        Time.text = StartTime.ToString();

        while (true)
        {
            yield return new WaitForSeconds(1f);
            StartTime--;
            Time.text = StartTime.ToString();

            if (StartTime == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        _GameManager.GrowHoop(transform.position);
    }
}
