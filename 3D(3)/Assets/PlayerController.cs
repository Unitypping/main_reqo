using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 20f;
    public float torque = 1f;
    public float jumpForce = 500f;

    float vspeed = 2f;
    float hspeed = 2f;

    float current = 60f;
    float target = 60f;
    float velocity = 0;

    public GameObject bulletPrefab;

    bool canShoot = true;
    float coolTime;
    public float coolTimeMax = 5f;

    private RaycastHit hit;
    private Ray ray;

    public float getDistance = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolTime = coolTimeMax;
    }
    void ObjectHit()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Key")
            {
                if(Vector3.Distance(hit.collider.transform.position, transform.position) < getDistance)
                {
                    Debug.Log("°¨Áö");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GetItem(hit.collider.gameObject);
                    }
                }
            }
        }
    }
    void GetItem(GameObject item)
    {
        Destroy(item);
    }

    // Update is called once per frame
    void Update()
    {
        ObjectHit();

        Vector3 forward = transform.forward;
        forward.y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(forward * moveForce);
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(-forward * moveForce);
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(transform.right * moveForce);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(-transform.right * moveForce);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
        }

        float h = Input.GetAxis("Mouse X") * hspeed;
        float v = Input.GetAxis("Mouse Y") * vspeed;
        Camera.main.transform.Rotate(-v, 0, 0);
        transform.Rotate(Vector3.up, h, Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            if(canShoot)
            {
                GameObject cube = Instantiate(bulletPrefab);
                cube.transform.position = transform.position + transform.forward * 2 + transform.up;
                Rigidbody rig = cube.GetComponent<Rigidbody>();
                Vector3 f = Camera.main.transform.forward;
                rig.AddForce(f * 2000);
                canShoot = false;
            }
        }
        if (!canShoot)
        {
            coolTime -= Time.deltaTime;
            if (coolTime < 0)
            {
                coolTime = 10f;
                canShoot=true;
            }
        }
    }
}
