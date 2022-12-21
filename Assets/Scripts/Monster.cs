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
     public int maxHealth;
     public int currentHealth;
     private bool monster_attacked_recently;
     internal float timestamp_attack_landed_on_monster = float.MaxValue;
     private bool player_attacked_recently;
     internal float timestamp_attack_landed_on_player = float.MaxValue;  

     public HealthBar healthbar;

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
         healthbar.SetMaxHealth(maxHealth);
         monster_attacked_recently = false;
         player_attacked_recently = false;
     }


     void Update()
     {
         if (fps_player_obj.currentHealth < 0.001f) 
             return;
        
         float time_since_moster_attacked = Time.time - timestamp_attack_landed_on_monster;
         if (time_since_moster_attacked > 1.0f) {
             monster_attacked_recently = false;
         }

         float time_since_player_attacked = Time.time - timestamp_attack_landed_on_player;
         if (time_since_player_attacked > 1.0f) {
             player_attacked_recently = false;
         }

         Vector3 dir_and_dist_from_player = (fps_player_obj.transform.position - transform.position);
         float dist_from_player = dir_and_dist_from_player.magnitude;
        //  Debug.Log(dist_from_player);
         if (!is_dead) {
            if (dist_from_player <= attack_dist) {
                monster_animation_controller.SetBool("walk", false); 
                monster_animation_controller.SetBool("attack", true);

                fps_player_obj.hasPlayed = false;
                if (!fps_player_obj.source.isPlaying && fps_player_obj.hasPlayed == false)
                {
                    fps_player_obj.source.PlayOneShot(fps_player_obj.swordSwoosh);
                    fps_player_obj.hasPlayed = true;
                }
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

     }

     private void OnCollisionEnter(Collision collision)
     {
         collisionStay = true;
         this.collision = collision;
     }

     private void OnCollisionExit(Collision collision)
     {
         collisionStay = false;
         this.collision = collision;
     }

     private void Attack()
     {
         if (!is_dead && !monster_attacked_recently && !player_attacked_recently) {
             if (collision.gameObject.name == "Player")
             {
                //  Debug.Log("collision stay");
                //  Debug.Log((fps_player_obj.transform.position - transform.position).magnitude);
                 // if player attacked monster
                 if(player_animation_controller.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
                     Debug.Log("player attacked");
                     monster_attacked_recently = true;
                     timestamp_attack_landed_on_monster = Time.time;
                     // monster loses health
                     currentHealth -= 1;
                     Debug.Log(currentHealth);
                     healthbar.SetHealth(currentHealth);

                    fps_player_obj.hasPlayed = false;
                    if (!fps_player_obj.source.isPlaying && fps_player_obj.hasPlayed == false)
                    {
                        fps_player_obj.source.PlayOneShot(fps_player_obj.enemyHurt);
                        fps_player_obj.hasPlayed = true;
                    }
                    //  StartCoroutine(ExampleCoroutine());
                    // monster dies
                    if (currentHealth <= 0) {
                        is_dead = true;
                        monster_animation_controller.SetBool("walk", false); 
                        monster_animation_controller.SetBool("attack", false); 
                        monster_animation_controller.SetTrigger("dead");
                        fps_player_obj.hasPlayed = false;
                        if (!fps_player_obj.source.isPlaying && fps_player_obj.hasPlayed == false)
                        {
                            fps_player_obj.source.PlayOneShot(fps_player_obj.enemyHurt);
                            fps_player_obj.hasPlayed = true;
                        }
                        Destroy(gameObject, 3);
                     }
                 } 
                 // if monster attacks
                 else {
                     // player health decreases
                     player_attacked_recently = true;
                     timestamp_attack_landed_on_player = Time.time;
                     fps_player_obj.currentHealth -= 1;
                     fps_player_obj.healthbar.SetHealth(fps_player_obj.currentHealth);
                     fps_player_obj.hasPlayed = false;
                    if (!fps_player_obj.source.isPlaying && fps_player_obj.hasPlayed == false)
                    {
                        fps_player_obj.source.PlayOneShot(fps_player_obj.playerHurt);
                        fps_player_obj.hasPlayed = true;
                    }
                }
             }
         }
     }
    // IEnumerator ExampleCoroutine()
    // {
    //     //Print the time of when the function is first called.
    //     Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //     //yield on a new YieldInstruction that waits for 5 seconds.
    //     yield return new WaitForSecondsRealtime(5);

    //     //After we have waited 5 seconds print the time again.
    //     Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    // }

 }