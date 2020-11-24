using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowball : MonoBehaviour
{
    public GameObject Snowball;
	public Transform hold_pos;
	public float LaunchForce;

	Vector2 Direction;
	public float force;
	public GameObject pointPrefab;
	public GameObject[] points;
	public int points_number;
	//bool ReadyToShoot = false;
	//Vector2 ShootPos;

    // Start is called before the first frame update
    void Start()
    {
	        points = new GameObject[points_number];

		    for(int i = 0; i < points_number; i++)
		         {
		         	points[i] = Instantiate(pointPrefab, hold_pos.position, Quaternion.identity);
		         } 	
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
        //  	if (!ReadyToShoot)
        //  	{
      //   		ShootPos = hold_pos.position;

	         	// points = new GameObject[points_number];

		         // for(int i = 0; i < points_number; i++)
		         // {
		         // 	points[i] = Instantiate(pointPrefab, hold_pos.position, Quaternion.identity);
		         // }

		     // 	ReadyToShoot = true;
		     // } 
		     // else 
		     // {
		    	Shoot();
		    // 	ReadyToShoot = false;
		    // 	Debug.Log(ReadyToShoot);
		    // }

        }

        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 holdpos = hold_pos.position;

        // if (Input.GetKeyDown(KeyCode.A) && ReadyToShoot)
        // {
        // 	ShootPos = new Vector2(ShootPos.x- 0.5f, ShootPos.y);
        // }
        // if (Input.GetKeyDown(KeyCode.D) && ReadyToShoot)
        // {
        // 	ShootPos = new Vector2(ShootPos.x+ 0.5f, ShootPos.y);
        // }

		Direction = MousePos - holdpos;
		//Direction = ShootPos - holdpos;

		// if (!ReadyToShoot)
  //       {
	         faceMouse();

	         for (int i = 0; i < points.Length; i++)
	         {
	        	points[i].transform.position = PointPosition(i * 0.1f);
	         }
	 //    }

	 //    Debug.Log(ReadyToShoot);
    }

    void Shoot()
    {
    	GameObject SnowballIns = Instantiate(Snowball, hold_pos.position, hold_pos.rotation);
    	SnowballIns.GetComponent<Rigidbody2D>().velocity = hold_pos.right * LaunchForce;
    }

    void faceMouse()
    {
    	hold_pos.right = Direction;
    }
    
    Vector2 PointPosition(float t)
    {
    	Vector2 current_pointsPos = (Vector2)hold_pos.position + (Direction.normalized * force * t) + 0.5f * Physics2D.gravity * (t*t);
    	return current_pointsPos;
    }
	
}
