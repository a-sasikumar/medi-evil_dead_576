using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float walking_velocity;
    public Text text;
    public float velocity;
    public int num_lives;
    public bool has_won;
    //private GameObject endzone;

    // Use this for initialization
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        velocity = 0.0f;
        num_lives = 5;
        has_won = false;
        //endzone = GameObject.FindWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Lives left: " + num_lives;

        // Initial State: Idle
        animation_controller.SetInteger("state", 0);

        // State: Walk
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animation_controller.SetInteger("state", 1);
            velocity += 0.5f;

            if (velocity > walking_velocity)
                velocity = walking_velocity;
        }

        // State: Run
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animation_controller.SetInteger("state", 2);
            velocity -= 0.7f;

            if (velocity > walking_velocity * 2)
                velocity = walking_velocity * 2;
        }

        // State: Roll
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A))
        {
            animation_controller.SetInteger("state", 3);
            velocity += 0.1f;

            if (velocity > walking_velocity / 2)
                velocity = walking_velocity / 2;
        }

        // State: CrouchBackwards
        else if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.Z))
        {
            animation_controller.SetInteger("state", 4);
            velocity -= 0.1f;

            if (Mathf.Abs(velocity) > walking_velocity / 2)
                velocity = (-1) * walking_velocity / 2;
        }

        // State: RunForwards + Jump later (if 'Space' is pressed)
        else if (Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.R))
        {
            animation_controller.SetInteger("state", 5);
            velocity += 0.7f;

            if (velocity > walking_velocity * 2)
                velocity = walking_velocity * 2;

            if (Input.GetKey(KeyCode.Space) && character_controller.isGrounded)
            {
                animation_controller.SetInteger("state", 6);
                velocity += 0.7f;

                if (velocity > walking_velocity * 3)
                    velocity = walking_velocity * 3;
            }

        }
        // Default State: Idle
        else
        {
            animation_controller.SetInteger("state", 0);
            velocity = 0;
        }



        // you don't need to change the code below (yet, it's better if you understand it). Name your FSM states according to the names below (or change both).
        // do not delete this. It's useful to shift the capsule (used for collision detection) downwards. 
        // The capsule is also used from turrets to observe, aim and shoot (see Turret.cs)
        // If the character is crouching, then she evades detection. 
        bool is_crouching = false;
        if ((animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchForward"))
         || (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchBackward")))
        {
            is_crouching = true;
        }

        if (is_crouching)
        {
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.0f, GetComponent<CapsuleCollider>().center.z);
        }
        else
        {
            GetComponent<CapsuleCollider>().center = new Vector3(GetComponent<CapsuleCollider>().center.x, 0.9f, GetComponent<CapsuleCollider>().center.z);
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

        if (transform.position.y > 0.0f) // if the character starts "climbing" the terrain, drop her down
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
            text.text = "YOU DIED!";
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
