using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Transform spwanPoint;

    public bool spawn;

    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float walking_velocity;
    //public Text text;
    public float velocity;
    public int num_lives;
    public bool has_won;
    public int maxHealth = 5;
    public int currentHealth;

    public HealthBar healthbar;
    //private GameObject endzone;
    private float Xorigin;
    private float Zorigin;
    private float Height;

    // Use this for initialization
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(1f, 0.0f, 1f);
        walking_velocity = 1.5f; 
        velocity = 0.0f;
        num_lives = 5;
        has_won = false;
        spawn = false;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        //endzone = GameObject.FindWithTag("Finish");
        //Xorigin = transform.position.x;
        //Zorigin = transform.position.z;
        //Height = transform.position.y;
        //// Asking Height
        //if (Height < 0)
        //{
        //    Height += 3;
        //}
        character_controller.transform.position = spwanPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        //text.text = "Lives left: " + num_lives;

        // Initial State: Idle

        // dummy method to check damage and health bar
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(1);
        }

        animation_controller.SetInteger("State", 0);

        // State: Walk
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animation_controller.SetInteger("State", 1);
            velocity += 0.5f;

            if (velocity > walking_velocity)
                velocity = walking_velocity;
        }

        // State: Walk Backwards
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animation_controller.SetInteger("State", 1);
            velocity -= 0.1f;

            if (velocity > walking_velocity)
                velocity = walking_velocity;
        }

        // State: Attack
        else if (Input.GetKey(KeyCode.A))
        {
            animation_controller.SetInteger("State", 2);
            velocity += 0.7f;

            //if (velocity > walking_velocity * 2)
            //    velocity = walking_velocity * 2;
        }

        // State: Roll
        else if (Input.GetKey(KeyCode.S))
        {
            animation_controller.SetInteger("State", 3);
        }

        // State: Block
        else if (Input.GetKey(KeyCode.B))
        {
            animation_controller.SetInteger("State", 4);
            velocity = 0;
        }

        // State: Damage (enemy hit)
        else if (Input.GetKey(KeyCode.Space))
        {
            animation_controller.SetInteger("State", 5);
            velocity += 0.1f;
        }
        // Default State: Idle
        else
        {
            animation_controller.SetInteger("State", 0);
            velocity = 0;
        }


        // Rotation and orientation
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(new Vector3(0.0f, -0.5f, 0.0f));
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(new Vector3(0.0f, 0.5f, 0.0f));

        // you will use the movement direction and velocity in Turret.cs for deflection shooting 
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        // if jump: no rotation ie., xdirection = zdirection

        //transform.position = new Vector3(transform.position.x + speed * xdirection, 0.0f, transform.position.z + speed * zdirection * Time.deltaTime);

        // character controller's move function is useful to prevent the character passing through the terrain
        // (changing transform's position does not make these checks)

        //if (transform.position.y < 0)
        //{
        //    transform.position = new Vector3(xdirection, Height, zdirection);
        //}

        if (character_controller.transform.position.y > 0.0f) // if the character starts "climbing" the terrain, drop him down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }
        else
        {
            character_controller.Move(movement_direction * velocity * Time.deltaTime);
        }

        if (num_lives < 1)
        {
            //text.text = "YOU DIED!";
            animation_controller.SetTrigger("isDead");
            velocity = 0;
            StartCoroutine(Lost());
        }

        //if (endzone.GetComponent<EndZone>().has_won)
        //{
        //    has_won = true;
        //    text.text = "YOU WON!";
        //    animation_controller.SetInteger("state", 0);
        //    velocity = 0;
        //    StartCoroutine(Won());
        //}
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    IEnumerator Lost()
    {
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("PlayAgain");
    }

    IEnumerator Won()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("YouWon");
    }
}
