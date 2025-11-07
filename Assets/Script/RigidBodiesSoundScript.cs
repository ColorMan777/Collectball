using UnityEngine;

public class RigidBodiesSoundScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
      // Play a sound based on size of impact.
      if (collision.relativeVelocity.magnitude > 0.5f){
        //Debug.Log(collision.relativeVelocity.magnitude);
        gameObject.GetComponent<AudioSource>().volume = mapValue(collision.relativeVelocity.magnitude, 0.5f, 3f, 0f, 1f) * 5f;
        gameObject.GetComponent<AudioSource>().pitch = Random.Range(-1f, 1f);
        gameObject.GetComponent<AudioSource>().Play();
      }
        
   }
   
   float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }
}
