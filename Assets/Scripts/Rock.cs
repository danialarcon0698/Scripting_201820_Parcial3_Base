using UnityEngine;

public class Rock : MonoBehaviour {
    public void ThrowRock(Vector3 direction) {
        GetComponent<Rigidbody>().AddForce(direction * 40, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ActorController actor = collision.gameObject.GetComponent<ActorController>();
        if (collision.gameObject.GetComponent<ActorController>() != null && collision.gameObject.GetComponent<ActorController>().IsTagged) {
            actor.StartCoroutine(actor.Stunned());
        }
    }
}
