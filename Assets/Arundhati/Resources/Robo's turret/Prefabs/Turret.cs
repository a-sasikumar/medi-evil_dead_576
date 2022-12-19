using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private float shooting_delay; 
    private GameObject projectile_template;
    private Vector3 direction_from_turret_to_claire;
    private Vector3 shooting_direction;
    private Vector3 projectile_starting_pos;
    private float projectile_velocity;
    private bool claire_is_accessible;
    public float EPSILON = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        projectile_template = (GameObject)Resources.Load("Apple/Prefab/Apple", typeof(GameObject));  // projectile prefab
        if (projectile_template == null)
            Debug.LogError("Error: could not find the apple prefab in the project! Did you delete/move the prefab from your project?");
        shooting_delay = 0.5f;  
        projectile_velocity = 5.0f;
        direction_from_turret_to_claire = new Vector3(0.0f, 0.0f, 0.0f);
        projectile_starting_pos = new Vector3(0.0f, 0.0f, 0.0f);
        claire_is_accessible = false;
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject claire = GameObject.Find("Claire");
        if (claire == null)
            Debug.LogError("Error: could not find the game character 'Claire' in the scene. Did you delete the model Claire from your scene?");
        Vector3 claire_centroid = claire.GetComponent<CapsuleCollider>().bounds.center;
        Vector3 turret_centroid = GetComponent<Collider>().bounds.center;
        direction_from_turret_to_claire = claire_centroid - turret_centroid;
        direction_from_turret_to_claire.Normalize();

        RaycastHit hit;
        if (Physics.Raycast( turret_centroid, direction_from_turret_to_claire, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject == claire)
            {
                ////////////////////////////////////////////////
                // WRITE CODE HERE:
                // implement deflection shooting
                // shooting_direction = direction_from_turret_to_claire; // this is a very simple heuristic for shooting, replace it
                float delta_pos = 10.0f;
                Vector3 future_target_pos = claire_centroid;
                while(delta_pos > EPSILON) {
                    float dist = (float)Vector3.Distance(future_target_pos, turret_centroid);
                    float lookahead_time = dist / projectile_velocity;
                    Vector3 last_future_target_pos = future_target_pos;
                    Claire c = (Claire)claire.GetComponent<Claire>();
                    Vector3 target_velocity = c.movement_direction*c.velocity;
                    future_target_pos = claire_centroid + lookahead_time * target_velocity;
                    delta_pos = Vector3.Distance(future_target_pos, last_future_target_pos);
                }
                shooting_direction = future_target_pos - turret_centroid;
                shooting_direction.Normalize();
                ////////////////////////////////////////////////

                float angle_to_rotate_turret = Mathf.Rad2Deg * Mathf.Atan2(shooting_direction.x, shooting_direction.z);
                transform.eulerAngles = new Vector3(0.0f, angle_to_rotate_turret, 0.0f);
                Vector3 current_turret_direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * transform.eulerAngles.y), 1.1f, Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
                projectile_starting_pos = transform.position + 1.1f * current_turret_direction;  // estimated position of the turret's front of the cannon
                claire_is_accessible = true;
            }
            else
                claire_is_accessible = false;            
        }
    }

    private IEnumerator Spawn()
    {
        while (true)
        {            
            if (claire_is_accessible)
            {
                GameObject new_object = Instantiate(projectile_template, projectile_starting_pos, Quaternion.identity);
                new_object.GetComponent<Apple>().direction = shooting_direction;
                new_object.GetComponent<Apple>().velocity = projectile_velocity;
                new_object.GetComponent<Apple>().birth_time = Time.time;
                new_object.GetComponent<Apple>().birth_turret = transform.gameObject;
            }
            yield return new WaitForSeconds(shooting_delay); // next shot will be shot after this delay
        }
    }
}
