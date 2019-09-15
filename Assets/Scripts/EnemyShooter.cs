using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public Transform wand;
    public GameObject greenLaser;
    public Transform playerHead;

    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        InvokeRepeating("Shoot", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void Shoot()
    {
        if (wand.gameObject.activeSelf)
        {
            GameObject laser = (GameObject)Instantiate(greenLaser, wand.position + (new Vector3(0, 0, 0.5f)), wand.rotation);
            laser.transform.LookAt(playerHead);
            laser.transform.Rotate(new Vector3(90, 0, 0));
            laser.GetComponent<Rigidbody>().velocity = laser.transform.up * 5f;
        }
    }
}
