using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScript : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5.5f);
        SceneManager.LoadScene(2);
    }
}
