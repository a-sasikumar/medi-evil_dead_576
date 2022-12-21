using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float walking_velocity;
    // public Text text;    
    public float velocity;
    public int num_lives;
    public bool has_won;
    
    public bool is_dead;
    public GameObject death_text_object;
    public GameObject restart_button;
    public GameObject success_text;
    public bool has_lost;
    

    // Use this for initialization
    void Start ()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        velocity = 0.0f;
        num_lives = 5;
        has_won = false;

        is_dead = false;
        has_lost = false;
        // death_text_object = GameObject.Find("GameOver");
        // restart_button = GameObject.Find("Restart");
        // success_text = GameObject.Find("Victory");
        // death_text_object.SetActive(false);
        // restart_button.SetActive(false);
        // success_text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // text.text = "Lives left: " + num_lives;

        ////////////////////////////////////////////////
        // WRITE CODE HERE:
        // (a) control the animation controller (animator) based on the keyboard input. Adjust also its velocity and moving direction. 
        // (b) orient (i.e., rotate) your character with left/right arrow [do not change the character's orientation while jumping]
        // (c) check if the character is out of lives, call the "death" state, let the animation play, and restart the game
        // (d) check if the character reached the target (display the message "you won", freeze the character (idle state), provide an option to restart the game
        // feel free to add more fields in the class        
        ////////////////////////////////////////////////
        bool isRolling = animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Roll");
            if(!isRolling) {
            if(Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))){
                animation_controller.SetBool("isIdle", false);   
                animation_controller.SetBool("isWalking", false);
                animation_controller.SetBool("isBlocking", false);  
                animation_controller.SetBool("isAttacking", false);  
                animation_controller.SetBool("isRunning", true);     
                animation_controller.SetBool("isRolling", false);
                velocity += 0.3f;
                velocity = Mathf.Min(velocity, walking_velocity*2);
            } else if (Input.GetKey(KeyCode.UpArrow)) {
                animation_controller.SetBool("isIdle", false);   
                animation_controller.SetBool("isWalking", true);
                animation_controller.SetBool("isBlocking", false);  
                animation_controller.SetBool("isAttacking", false);  
                animation_controller.SetBool("isRunning", false);   
                velocity += 0.15f;
                velocity = Mathf.Min(velocity, walking_velocity);
            } else {
                animation_controller.SetBool("isIdle", true);   
                animation_controller.SetBool("isWalking", false);
                animation_controller.SetBool("isBlocking", false);  
                animation_controller.SetBool("isAttacking", false);  
                animation_controller.SetBool("isRunning", false);     
                velocity = 0.0f;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && !isRolling) {
                transform.Rotate(new Vector3(0.0f,-0.2f,0.0f));
            } else if (Input.GetKey(KeyCode.RightArrow) && !isRolling) {
                transform.Rotate(new Vector3(0.0f,0.2f,0.0f)); 
            }
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && !isRolling) {
                animation_controller.SetBool("isAttacking", true);
            }
            if ((Input.GetKey(KeyCode.Space)) && !isRolling) {
                animation_controller.SetBool("isRolling", true);
            }
        }
        if (isRolling) {
            animation_controller.SetBool("isRolling", false);
            velocity += 0.45f;
            velocity = Mathf.Min(velocity, 3*walking_velocity);
        }

        if (num_lives <=0 && !is_dead && !has_won) {
            is_dead = true;
            animation_controller.SetTrigger("dead");
            velocity = 0.0f;
        } 

        if (has_won) {
            animation_controller.SetBool("isIdle", true);               
            animation_controller.SetBool("isWalking", false);
            animation_controller.SetBool("isBackWalking", false);      
            animation_controller.SetBool("isCrouchForward", false);  
            animation_controller.SetBool("isCrouchBackward", false);   
            animation_controller.SetBool("isRunning", false);                                                                                                                             
            velocity = 0.0f;
            restart_button.SetActive(true);
            success_text.SetActive(true);
        } 


        // Show menu after animation has run completely
        if (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
            animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9 && !has_lost) {
            has_lost = true;
            restart_button.SetActive(true);
            death_text_object.SetActive(true);
        }

        // you don't need to change the code below (yet, it's better if you understand it). Name your FSM states according to the names below (or change both).
        // do not delete this. It's useful to shift the capsule (used for collision detection) downwards. 
        // The capsule is also used from turrets to observe, aim and shoot (see Turret.cs)
        // If the character is crouching, then she evades detection. 
        bool is_crouching = false;
        if ( (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchForward"))
         ||  (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchBackward")) )
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

        // you will use the movement direction and velocity in Turret.cs for deflection shooting 
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

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
    }   

    public void Restart()
    {
        death_text_object.SetActive(false);
        restart_button.SetActive(false);
        has_lost = false;
        is_dead = false;
        num_lives = 5;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }                 
}
