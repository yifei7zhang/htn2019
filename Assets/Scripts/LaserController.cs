using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public GameObject explosion;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
		initPos = transform.position;
    }

    void OnTriggerEnter()
    {
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.GetComponent<ParticleSystem>().Play();
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
