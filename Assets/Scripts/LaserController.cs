using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject explosion;
    public string colorName;

    private Vector3 initPos;

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
                col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1000, 0));
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
                col.transform.Find("Wand").transform.Find("default").gameObject.GetComponent<Rigidbody>().velocity = new Vector3(5, 5, 0);
                col.transform.Find("Wand").transform.Find("default").gameObject.GetComponent<Rigidbody>().useGravity = true;
                GameObject wand = col.transform.Find("Wand").gameObject;
                Destroy(wand);
            }
        }

        Destroy(this.gameObject);
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
