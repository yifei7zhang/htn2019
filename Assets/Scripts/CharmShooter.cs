using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class CharmShooter : MonoBehaviour
{
	public GameObject laserPrefab;
	public Transform wand;
	public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
		{
			GameObject laser = (GameObject) Instantiate(laserPrefab, wand.position, wand.rotation);
			laser.transform.Rotate(new Vector3(0, 0, 90));
			laser.GetComponent<Rigidbody>().velocity = wand.right * speed;
		}
	}
}
