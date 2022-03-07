using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public FPSController player;
    public bool GuardCanShoot;


    public GameObject[] PotentialTargets;
 
   public Material Green;
   public Material Red;
 
   protected float rotation = 120;
 
   protected void Update()
   {
     rotation += 36.0f * Time.deltaTime;
 
     transform.rotation = Quaternion.Euler(0, rotation, 0);
 
     foreach(GameObject target in PotentialTargets)
     {      
       if (CanSee(target) == true)
         ChangeMaterial(target, Red);
       else
         ChangeMaterial(target, Green);
     }

     
   }

   void FixedUpdate()
   {
       //rb.AddRelativeForce( Vector3.up * (rb.mass * Mathf.Abs(Physics.gravity.y) ) );
   }
 
   protected void ChangeMaterial(GameObject target, Material material)
   {
     Renderer renderer = target.GetComponent<Renderer>();
     renderer.material = material;
   }
 
   protected bool CanSee(GameObject target)
   {
     float distance = 100.0f; // how far they can see the target
     float arc = 45.0f; // their field of view
 
     if (Vector3.Distance(transform.position, target.transform.position) < distance)
     {
       // enemy is within distance
 
       if (Vector3.Dot(transform.forward, target.transform.position) > 0 && Vector3.Angle(transform.forward, target.transform.position) < arc)
       {
         // enemy is ahead of me and in my field of view
         RaycastHit hitInfo;
 
         // Vector3(0, 0.5f, 0) is the head offset for my capsules, you'll need to adjust accordingly
         if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), (target.transform.position + new Vector3(0, 0.5f, 0)) - transform.position, out hitInfo) == true)
         {
           // we hit SOMETHING, not necessarily a player
           if (hitInfo.collider.name == "Player")
             return true;
         }
       }
     }
 
     return false;
   }

   public void SafetyOff()
   {
       if (!player.isInStart)
       {
           GuardCanShoot = true;
       }
       else
       {
           GuardCanShoot = false;
       }
   }
}
