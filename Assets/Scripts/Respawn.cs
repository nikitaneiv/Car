using UnityEngine;

public class Respawn : MonoBehaviour
{
    private readonly static Vector3 respawn = new Vector3(0, 0, 0);
    private readonly static Vector3 rotationRespawn = new Vector3(0, 90, 0);
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Car>())
        {
            other.gameObject.GetComponent<Rigidbody>().Sleep();
            Quaternion rotation = Quaternion.identity;
            rotation.eulerAngles = rotationRespawn;
            other.transform.rotation = rotation;
            other.transform.position = respawn;
        }
    }
}
