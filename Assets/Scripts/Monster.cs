using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// functionality of virus
// animates color and size of the virus
// and attacks the player if the player is near the virus (check the code)
public class Monster : MonoBehaviour
{
    private Player fps_player_obj;
    private Animator player_animation_controller;
    private Animator monster_animation_controller; 
    private float radius_of_search_for_player;
    private float monster_speed;
    private float attack_dist;
    private bool collisionStay;
    private Collision collision = null;
    private bool is_dead;

	void Start ()
    {
        
        fps_player_obj = GameObject.Find("Player").GetComponent<Player>();
        player_animation_controller = fps_player_obj.GetComponent<Animator>();
        monster_animation_controller = GetComponent<Animator>();
        radius_of_search_for_player = 10.0f;
        monster_speed = 1.0f;
        attack_dist = 1.15f;
        collisionStay = false;
        is_dead = false;
    }

    // *** YOU NEED TO COMPLETE THIS PART OF THE FUNCTION TO ANIMATE THE VIRUS ***
    // so that it moves towards the player when the player is within radius_of_search_for_player
    // a simple strategy is to update the position of the virus
    // so that it moves towards the direction d=v/||v||, where v=(fps_player_obj.transform.position - transform.position)
    // with rate of change (virus_speed * Time.deltaTime)
    // make also sure that the virus y-coordinate position does not go above the wall height
    void Update()
    {
        if (fps_player_obj.currentHealth < 0.001f) // || level.player_entered_house)
            return;
        
        Vector3 dir_and_dist_from_player = (fps_player_obj.transform.position - transform.position);
        float dist_from_player = dir_and_dist_from_player.magnitude;
        if (dist_from_player <= attack_dist) {
            monster_animation_controller.SetBool("walk", false); 
            monster_animation_controller.SetBool("attack", true); 
        }
        else if (dist_from_player <= radius_of_search_for_player) { 
            Vector3 dir = dir_and_dist_from_player / dist_from_player;
            dir.y = 0;
            monster_animation_controller.SetBool("attack", false); 
            monster_animation_controller.SetBool("walk", true); 
            transform.position += dir * monster_speed * Time.deltaTime;
            transform.LookAt(fps_player_obj.transform.position);
        }
        else {
        }

        if (collisionStay) {
            Attack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        collisionStay = true;
        this.collision = collision;
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exit");
        collisionStay = false;
        this.collision = collision;
    }

    private void Attack()
    {
        if (!is_dead) {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("collision stay");
                Debug.Log((fps_player_obj.transform.position - transform.position).magnitude);
                // if player attacked monster
                if(player_animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                    Debug.Log("player attcked");
                    // monster dies
                    is_dead = true;
                    monster_animation_controller.SetBool("walk", false); 
                    monster_animation_controller.SetBool("attack", false); 
                    monster_animation_controller.SetTrigger("dead");
                    Destroy(gameObject, 4);
                } 
                // if monster attacks
                else {
                    // player health decreases
                    // fps_player_obj.currentHealth -= 1;
                    fps_player_obj.healthbar.SetHealth(fps_player_obj.currentHealth);
                }
            }
        }
    }

    // IEnumerator Monster_death() {
    //     yield return new WaitForSeconds(5); //this will wait 5 seconds 
        
    // }

}
