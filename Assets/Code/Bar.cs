using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(this.transform.position.y <= -6){
            Destroy(this.gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    
}
