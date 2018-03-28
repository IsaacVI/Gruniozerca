using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotController : MonoBehaviour {

    float speed= 10;
    public SpriteRenderer carrotRenderer;

    public int color;

   public bool active;
    
    // Use this for initialization
   public	void Set () {
        carrotRenderer = GetComponent<SpriteRenderer>();
	}

  

    private void OnBecameInvisible()
    {
        if(active)
        PlayerController.GetDamage();
        Disable();
    }

    public void Disable()
    {
        active = false;
        transform.position = new Vector3(-1000, -1000, 0);
        gameObject.SetActive(false);
        CarrotGenerator.carrots.Add(this);
    }

    // Update is called once per frame
    void Update () {
        transform.Translate(new Vector3(0,-speed*Time.deltaTime,0));
      
	}
}
