using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotGenerator : MonoBehaviour {

    public CarrotController carrot;
    public int countOfCarrots;
    public static List<CarrotController> carrots;
    float timeToCarrot;
    // Use this for initialization
    void Start()
    {
        carrots = new List<CarrotController>();
        for (int i = 0; i < countOfCarrots; i++)
        {
            carrots.Add(Instantiate(carrot, new Vector3(-2000, -2000), new Quaternion(), transform));
            carrots[i].Set();
        }
    }
	


	// Update is called once per frame
	void Update () {
        if (GameController.isPlay)
        {
            if (timeToCarrot > 0)
                timeToCarrot -= Time.deltaTime;
            else
            {
                SendCarrot();
                timeToCarrot = 1.5f;
            }

        }
        else
            timeToCarrot = 0;
	}


    void SendCarrot()
    {
        CarrotController c;
        if (carrots.Count <= 0)
        {
            c = Instantiate(carrot, new Vector3(-2000, -2000), new Quaternion(), transform);
            c.Set();
        }
        else
        {
            c = carrots[0];
            carrots.RemoveAt(0);
        }
        c.transform.position = new Vector3(Random.Range(GameController.minXPoz,GameController.maxXPoz),transform.position.y);
        int i = Random.Range(0, PlayerController.colors.Length );
        c.active = true;
        c.color = i;
        c.carrotRenderer.color = PlayerController.colors[i];
        c.gameObject.SetActive(true);
        
    }
}
