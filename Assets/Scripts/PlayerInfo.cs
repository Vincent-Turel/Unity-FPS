using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerInfo : MonoBehaviour
{

    public int health=100;
    public ProgressBar progressBar;
    public static bool playerDead = false;
    public static Vector3 position;
    public static int score = 0;

    public GameObject damageUI;
    private RawImage image;

    private void Start()
    {
        progressBar.BarValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if (health <= 0) 
        {
            this.GetComponent<PlayerMotion>().enabled = false;
            playerDead = true;
        }
        else
        {
            playerDead = false;
        }
    }

    public void takeDamage(int damage)
    {
        if (!playerDead)
        {
            health -= damage;
            progressBar.BarValue = health;
            StartCoroutine(TakeDamageUI());
        }
    }

    public static void addscore()
    {
        score++;
    }

    IEnumerator TakeDamageUI()
    {
        yield return new WaitForSeconds(.23f);
        damageUI.SetActive(true);
        image = damageUI.GetComponent<RawImage>();
        Color fixedColor = image.color;
        fixedColor.a = 1;
        image.color = fixedColor;
        image.CrossFadeAlpha(0f, 0f, true);
        image.CrossFadeAlpha(.5f, .1f, false);
        yield return new WaitForSeconds(1f);
        image.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
        damageUI.SetActive(false);
    }
}