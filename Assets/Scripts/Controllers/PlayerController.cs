using UnityEngine;

public class PlayerController : ActorController
{
    [SerializeField]
    private LayerMask walkable;
    public Transform reference;

    protected override Vector3 GetTargetLocation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 1000);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkable))
        {
            return hit.point;
        }
        else
        {
            print("Couldn't find point");
            return transform.position;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveActor();
        }

        if (GetComponent<Rock>() != null) {
            if (Input.GetMouseButtonDown(1)) {
                GameObject rock = Instantiate(GameController.instance.rock,reference.position,Quaternion.identity);
                if (GameController.instance.TargetActor != gameObject) {
                    Vector3 direction = GameController.instance.TargetActor.transform.position - reference.position;
                    direction.Normalize();
                    rock.GetComponent<Rock>().ThrowRock(direction);
                    Destroy(GetComponent<Rock>());
                    print("HOli");
                }
            }
        }

        //print(string.Format("{0},{1},{2}", Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
    }
}