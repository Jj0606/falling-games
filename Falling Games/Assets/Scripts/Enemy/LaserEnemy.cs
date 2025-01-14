using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour
{
    private Rigidbody2D rgb;

    private float fireRate;
    private float canFire = 2f;

    public GameObject laserBeam;

    //follow player
    private GameObject player;

    [SerializeField] private float speed = 4f, stopDist = 5f;

    [SerializeField] private float rotateSpeed = 90f;

    private bool isFiring;



    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player");

        laserBeam.SetActive(false);

        isFiring = false;
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Rotate());

        //follows player
        if (Vector2.Distance(player.transform.position, transform.position) > stopDist)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        StartCoroutine(LaserEnemyFire());

        LaserActive();
    }

    private void FixedUpdate()
    {
        Rotation();
    }

    /*IEnumerator Rotate()
    {
        //rotation
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        float generatorTimer = Random.Range(0f, 3f);

        yield return new WaitForSeconds(generatorTimer);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rgb.rotation = angle;

    }*/

    void Rotation()
    {
        if(isFiring == false)
        {
            Vector3 direction = player.transform.position - transform.position;
            direction.Normalize();

            float randomAngleoffset = Random.Range(-45f, 45f);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f + randomAngleoffset;
            Quaternion desiredRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, rotateSpeed * Time.deltaTime);
        }
    }

    IEnumerator LaserEnemyFire()
    {
        if(Time.time > canFire)
        {
            laserBeam.SetActive(true);

            fireRate = Random.Range(2f, 4f);
            canFire = Time.time + fireRate;

            yield return new WaitForSeconds(0.5f);

            laserBeam.SetActive(false);
        }
    }

    void LaserActive()
    {
        if(laserBeam.activeSelf == false)
        {
            isFiring = false;
        }
        else
        {
            isFiring = true;
        }
    }
}
