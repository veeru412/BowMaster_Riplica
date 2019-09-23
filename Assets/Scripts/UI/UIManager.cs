using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image enemyAvatar;
    public Sprite[] avatarIcons;
    public Animation selectionAnim;
    public GameObject[] enemyObjs;
    public GameObject gameOverMenu;
    public static bool gameOver;
    IEnumerator Start()
    {
        gameOver = false;
        enemyAvatar.sprite = avatarIcons[0];
        int randLoop = Random.Range(10, 20);
        int indx = 0;
        int cLoop = 0;
        while(cLoop < randLoop)
        {
            cLoop++;
            indx++;
            if (indx >= avatarIcons.Length)
                indx = 0;
            yield return new WaitForSeconds(0.1f);
            float progress = 0.0f;
            while (progress < 1)
            {
                progress += Time.deltaTime * 10;
                float _scale = Mathf.Lerp(1.0f, 0.0f, progress);
                enemyAvatar.transform.localScale = Vector3.one * _scale;
                yield return null;
            }
            progress = 0.0f;
            enemyAvatar.sprite = avatarIcons[indx];
            if (indx == 1)
                enemyAvatar.transform.rotation = Quaternion.identity;
            else
                enemyAvatar.transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
            while (progress < 1)
            {
                progress += Time.deltaTime * 10;
                float _scale = Mathf.Lerp(0.0f, 1.0f, progress);
                enemyAvatar.transform.localScale = Vector3.one * _scale;
                yield return null;
            }        
        }
        Instantiate(enemyObjs[indx], new Vector3(Random.Range(40.0f, 50.0f),-1.0f,0.0f), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        selectionAnim.Play();
        Camera.main.GetComponent<BowMaster.Core.CameraFollow>().PlayIntro();
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }

    public void BtnGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
