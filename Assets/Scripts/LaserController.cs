using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject explosion;
    public string colorName;

    private Vector3 initPos;
    private GameObject wand;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
    }

    void OnTriggerEnter(Collider col)
    {


        if (col.gameObject.tag == "Enemy")
        {
            if (colorName == "Red")
            {
                col.gameObject.GetComponent<Rigidbody>().AddForce(this.gameObject.GetComponent<Rigidbody>().velocity / 2);
            }
            else if (colorName == "Green")
            {
                GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
                exp.GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
            }
            else if (colorName == "Orange")
            {
                GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
                exp.GetComponent<ParticleSystem>().Play();
                Destroy(col.gameObject);
            }
            else if (colorName == "Blue")
            {
                col.transform.Find("Wand").GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
                wand = col.transform.Find("Wand").gameObject;
                Invoke("DestroyWand", 1.0f);
            }
        }

        Destroy(this.gameObject);
    }

    void DestroyWand()
    {
        wand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - initPos).magnitude > 30)
		{
            Destroy(this.gameObject);
        }
    }
}
