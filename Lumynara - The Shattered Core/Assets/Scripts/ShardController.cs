using UnityEngine;

public class ShardController : MonoBehaviour
{

 void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
 
}