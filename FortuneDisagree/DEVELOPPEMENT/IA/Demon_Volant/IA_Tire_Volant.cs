using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Tire_Volant : MonoBehaviour
{
	private Transform cible;
	
	public float vitesse = 10.0f;
	public float augmentation_vitesse = 0.2f;	
	
    // Start is called before the first frame update
    void Start()
    {
        cible = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
		vitesse += augmentation_vitesse;
        transform.position = Vector2.MoveTowards(transform.position, cible.position, vitesse * Time.deltaTime);
    }
	
	void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(gameObject);
    }
}
