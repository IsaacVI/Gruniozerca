using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour {

    public float speed = 10; //predkosc domyslna
    int color=0;
    public static Color32[] colors = new Color32[] { new Color32(226 , 135 , 43 , 255), new Color32(0, 0, 255, 255), new Color32(0, 255, 0, 255) };
    SpriteRenderer nose;
    public static int lives =3;
    
    public bool mobileInputTest;
    public AudioSource damage, earnCarrot;
    public static PlayerController singleton;
    // Use this for initialization
	void Start () {
        singleton = this;
        GameController.maxXPoz = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.9f, 0)).x;
        GameController.minXPoz = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 0.4f, 0)).x;
        transform.position = new Vector3((GameController.maxXPoz +GameController.minXPoz)/2+transform.position.y,transform.position.z);
        nose = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void UpdateColor()
    {
        nose.color = colors[color];
    }

    private void LateUpdate()
    {
        Vector3 poz = transform.position;
        poz.x = Mathf.Clamp(poz.x,GameController.minXPoz,GameController.maxXPoz);
        transform.position = poz;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Carrot")
        {
            CarrotController carrot = collision.gameObject.GetComponent<CarrotController>();
            if (carrot.color == color)
            {
                AddScore();
            }
            else
            {
                GetDamage();
            }
            carrot.Disable();
        }
    }

    void AddScore()
    {
        if (GameController.isPlay)
        {
            if(GameController.sound)
            earnCarrot.Play();
            GameController.score += 100;
            MenuController.UpdateScore();
        }
    }

    public static void GetDamage()
    {
        if(GameController.sound)
        singleton.damage.Play();
        lives--;
        MenuController.UpdateLives();
        if (lives <= 0)
            MenuController.EndGame();
    }

    // Update is called once per frame
    void Update () {
        if (GameController.isPlay)
        {
            if (!mobileInputTest)
            {
                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                {
                    if (Input.GetAxis("Horizontal") < 0)
                        transform.localScale = new Vector3(1, 1, 1);
                    else
                        transform.localScale = new Vector3(-1, 1, 1);

                    transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime * Input.GetAxis("Horizontal"));
                }
                if (Input.GetButtonDown("NextColor"))
                {
                    color++;
                    if (color >= colors.Length)
                        color = 0;
                    UpdateColor();
                }
                if (Input.GetButtonDown("PreviusColor"))
                {
                    color--;
                    if (color < 0)
                        color = colors.Length - 1;
                    UpdateColor();
                }
            }
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || mobileInputTest)
            {

                if (CrossPlatformInputManager.GetButton("Left"))
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime * -1);
                }
                if (CrossPlatformInputManager.GetButton("Right"))
                {
                    transform.localScale = new Vector3(-1, 1, 1);

                    transform.Translate(new Vector3(speed, 0, 0) * Time.deltaTime );
                }
                if (CrossPlatformInputManager.GetButtonDown("Paint"))
                {
                    color++;
                    if (color >= colors.Length)
                        color = 0;
                    UpdateColor();
                }

            }
        }
    }
}
