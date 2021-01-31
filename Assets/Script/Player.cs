using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;
    public Camera cam;
    public int points;
    public SaveData saveData;
    
    public Animator globalLightAnim;
    public Animator personLightAnim;
    public Animator alarmLight;
    private Animator camAnim;
    public GameObject camera;

    public bool death;
    public GameObject panelUI;
    

    void Start()
    {
        camAnim = camera.GetComponent<Animator>();
        Time.timeScale = 1;
        InvokeRepeating("AddSpeed",3f,3f);
    }
    
    void LateUpdate()
    {
        Debug.Log(points);
        transform.Translate(Vector3.right * speed);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            speed *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
        }

        if (collider.gameObject.tag == "Block")
        {
            cam.transform.DOMove(new Vector3(collider.gameObject.transform.position.x,0,-10), 0.2f);
        }

        if (collider.gameObject.tag == "light")
        {
            Destroy(collider.gameObject);
            globalLightAnim.SetTrigger("globalLightOff");
            personLightAnim.SetTrigger("playerLightOn");
            StartCoroutine(LightTime());
        }

        if (collider.gameObject.tag == "Alarm")
        {
            Destroy(collider.gameObject);
            globalLightAnim.SetTrigger("globalLightAlarmOff");
            alarmLight.SetTrigger("bigAlarmLightOn");
            camAnim.applyRootMotion = false;
            camAnim.SetBool("shake",true);
            StartCoroutine(AlarmTime());
        }

        if (collider.gameObject.tag == "180Boost")
        {
            Destroy(collider.gameObject);
            camAnim.SetTrigger("rotate");
            StartCoroutine(RotateTime());
        }
        
        else if (collider.gameObject.tag == "Lose")
        {
            Death();
        }
    }

    private void AddSpeed()
    {
        if (speed > 0)
            speed += 0.002f;
        else speed -= 0.002f;
    }

    private void Death()
    {
        death = true;
        panelUI.SetActive(true);
        //Time.timeScale = 0;
        saveData.currentPoints = points;
        if (saveData.currentPoints >= saveData.recordPoints)
            saveData.recordPoints = saveData.currentPoints;
        StartCoroutine(DeathTime());
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("DeathScene");
    }

    IEnumerator RotateTime()
    {
        yield return new WaitForSeconds(10f);
        camAnim.SetTrigger("backRotate");
        StartCoroutine(CameraNormalization());
    }

    IEnumerator CameraNormalization()
    {
        yield return new WaitForSeconds(1f);
        camAnim.SetTrigger("rotateOff");
        camAnim.applyRootMotion = true;
    }

    IEnumerator AlarmTime()
    {
        yield return new WaitForSeconds(10f);
        camAnim.SetBool("shake",false);
        camAnim.applyRootMotion = true;
        globalLightAnim.SetTrigger("globalLightAlarmOn");
        alarmLight.SetTrigger("bigAlarmLightOff");
    }

    IEnumerator LightTime()
    {
        yield return new WaitForSeconds(10f);
        globalLightAnim.SetTrigger("globalLightOn");
        personLightAnim.SetTrigger("playerLightOff");
    }
    
}
