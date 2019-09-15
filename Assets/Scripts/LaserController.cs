using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
	private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
		initPos = transform.position;
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
