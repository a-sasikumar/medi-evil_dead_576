using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Claire : MonoBehaviour {

    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float walking_velocity;
    public Text text;    
    public float velocity;
    public int num_lives;
    public bool has_won;
    public bool is_dead;
    public GameObject button;

	// Use this for initialization
	void Start ()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        velocity = 0.0f;
        num_lives = 50000;
        has_won = false;
        is_dead = false;
        button = GameObject.Find("Button");
        button.SetActive(false);
    }

    void LoadOnClick(){
        Debug.Log("in loads()");
        animation_controller.SetTrigger("Exit");
        Debug.Log(SceneManager.GetActiveScene().name);
        num_lives=5;
    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Lives left: " + num_lives;

        ////////////////////////////////////////////////
        // WRITE CODE HERE:
        // (a) control the animation controller (animator) based on the keyboard input. Adjust also its velocity and moving direction. 
        // (b) orient (i.e., rotate) your character with left/right arrow [do not change the character's orientation while jumping]
        // (c) check if the character is out of lives, call the "death" state, let the animation play, and restart the game
        // (d) check if the character reached the target (display the message "you won", freeze the character (idle state), provide an option to restart the game
        // feel free to add more fields in the class        
        ////////////////////////////////////////////////
        animation_controller.SetBool("isIdle",true);
        bool is_jumping = animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Jump");
        is_dead = animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Death");
        // Debug.Log(is_dead);

        // if character is dead, restart game
        if (is_dead) {
            velocity=0.0f;
            animation_controller.ResetTrigger("death");
            // Debug.Log(animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime);
            if(animation_controller.GetCurrentAnimatorStateInfo(0).normalizedTime >=0.65f){
                animation_controller.SetTrigger("Exit");
                button.SetActive(true);
                button.GetComponent<Button>().interactable = true;
            }
        }
        // if character is out of lives, enter death state
        if (num_lives <= 0 & !has_won) {
            animation_controller.SetTrigger("death");
            text.text = "You are Dead.";
            animation_controller.SetBool("isWalking", false);
            animation_controller.SetBool("isWalkingBack", false);
            animation_controller.SetBool("isFwdCrouching", false);
            animation_controller.SetBool("isBwdCrouching", false);
            animation_controller.SetBool("isRunning", false);
            animation_controller.SetBool("isIdle", false);
        }
        
        // if game is won, show option to restart
        if (has_won) {
            velocity=0.0f;
            text.text = "Congratulations, you won!";
            animation_controller.SetBool("isWalking", false);
            animation_controller.SetBool("isWalkingBack", false);
            animation_controller.SetBool("isFwdCrouching", false);
            animation_controller.SetBool("isBwdCrouching", false);
            animation_controller.SetBool("isRunning", false);
            animation_controller.SetBool("isIdle", false);

            animation_controller.SetTrigger("Exit");
            button.SetActive(true);
            button.GetComponent<Button>().interactable = true;
            num_lives=5;
               
        }
              
        // jump
        if(is_jumping){
            velocity+=0.2f;
            velocity=Mathf.Min(velocity,3.0f*walking_velocity);
            animation_controller.ResetTrigger("Jump");
        }

        // walk/run/crouch forwards
        else if(Input.GetKey(KeyCode.UpArrow)) {
            if(Input.GetKey(KeyCode.LeftShift)){   

                if(Input.GetKey(KeyCode.Space)){
                    animation_controller.SetTrigger("Jump");
                }
                    animation_controller.SetBool("isWalking", false);
                    animation_controller.SetBool("isWalkingBack", false);
                    animation_controller.SetBool("isFwdCrouching", false);
                    animation_controller.SetBool("isBwdCrouching", false);
                    animation_controller.SetBool("isRunning", true);
                    animation_controller.SetBool("isIdle", false);
                    
                    if(velocity<0.0f){
                        velocity=0.0f;
                    }
                    velocity+=0.2f;
                    velocity=Mathf.Min(velocity,walking_velocity*2.0f);
                
            }
            else if(Input.GetKey(KeyCode.LeftControl)){                
                animation_controller.SetBool("isWalking", false);
                animation_controller.SetBool("isWalkingBack", false);
                animation_controller.SetBool("isFwdCrouching", true);
                animation_controller.SetBool("isBwdCrouching", false);
                animation_controller.SetBool("isRunning", false);
                animation_controller.SetBool("isIdle", false);
                
                if(velocity<0.0f){
                    velocity=0.0f;
                }
                velocity+=0.2f;
                velocity=Mathf.Min(velocity,walking_velocity/2.0f);
            }
             else {
                animation_controller.SetBool("isWalking", true);
                animation_controller.SetBool("isWalkingBack", false);
                animation_controller.SetBool("isFwdCrouching", false);
                animation_controller.SetBool("isBwdCrouching", false);
                animation_controller.SetBool("isRunning", false);
                animation_controller.SetBool("isIdle", false);

                if(velocity<0.0f){
                    velocity=0.0f;
                }
                velocity+=0.2f;
                velocity=Mathf.Min(velocity,walking_velocity);
            }
        }

        // walking/crouch backwards
        else if (Input.GetKey(KeyCode.DownArrow)) {
            if(Input.GetKey(KeyCode.LeftControl)){                
                animation_controller.SetBool("isWalking", false);
                animation_controller.SetBool("isWalkingBack", false);
                animation_controller.SetBool("isFwdCrouching", false);
                animation_controller.SetBool("isBwdCrouching", true);
                animation_controller.SetBool("isRunning", false);
                animation_controller.SetBool("isIdle", false);
                
                if(velocity<0.0f){
                    velocity=0.0f;
                }
                velocity-=0.2f;
                velocity=Mathf.Min(velocity,-1.0f*walking_velocity/2.0f);
            }
            else {
                animation_controller.SetBool("isWalking", false);
                animation_controller.SetBool("isWalkingBack", true);
                animation_controller.SetBool("isFwdCrouching", false);
                animation_controller.SetBool("isBwdCrouching", false);
                animation_controller.SetBool("isRunning", false);
                animation_controller.SetBool("isIdle", false);

                if(velocity<0.0f){
                    velocity=0.0f;
                }
                velocity-=0.2f;
                velocity=Mathf.Min(velocity,-1.0f*walking_velocity/1.5f);
            }
        }
    
        else {
            animation_controller.SetBool("isWalking", false);
            animation_controller.SetBool("isWalkingBack", false);
            animation_controller.SetBool("isFwdCrouching", false);
            animation_controller.SetBool("isBwdCrouching", false);
            animation_controller.SetBool("isRunning", false);
            animation_controller.SetBool("isIdle", true);
            velocity = 0;
        }

        // turn the character left and right
        if(Input.GetKey(KeyCode.LeftArrow) && !is_jumping &&!has_won &&!is_dead){
            transform.Rotate(new Vector3(0.0f,-0.6f,0.0f));
        }
        if(Input.GetKey(KeyCode.RightArrow) && !is_jumping &&!has_won &&!is_dead){
            transform.Rotate(new Vector3(0.0f,0.6f,0.0f));
        }


        // you don't need to change the code below (yet, it's better if you understand it). Name your FSM states according to the names below (or change both).
        // do not delete this. It's useful to shift the capsule (used for collision detection) downwards. 
        // The capsule is also used from turrets to observe, aim and shoot (see Turret.cs)
        // If the character is crouching, then she evades detection. 
        bool is_crouching = false;
        if ( (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchForwards"))
         ||  (animation_controller.GetCurrentAnimatorStateInfo(0).IsName("CrouchBackwards")) )
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
}
