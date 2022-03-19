using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawn_controller : MonoBehaviour
{
    [SerializeField] private GameObject SpawnPoint;
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private TMP_InputField speed_text;//скорость куба
    [SerializeField] private TMP_InputField time_text;//задержка спавна
    [SerializeField] private TMP_InputField distance_text;//дистанция исчезновения

    private float speed=1;
    private float time=1;
    private float distance=30;
    private List<GameObject> cubes = new List<GameObject>();
    private bool checktime=false;
    private IEnumerator Timer(float timeInSec)
    {
        yield return new WaitForSeconds(timeInSec);
        SpawnCube();
        checktime = false;
    }

    public void SpawnCube()
    {
        GameObject newcube = Instantiate(CubePrefab);
        int randpos = Random.Range(-2, 2);
        newcube.transform.position =new Vector3(randpos, 0.3f,0);
        cubes.Add(newcube);
    }
    public void FixedUpdate()
    {
        if (checktime == false)
        {
            checktime = true;
            StartCoroutine(Timer(time));
        }
        for(int i = 0; i < cubes.Count; i++)
        {
            if(cubes[i].transform.position.z >= distance)
            {
                Destroy(cubes[i]);
                cubes.RemoveAt(i);
            }
        }
        for( int i = 0; i < cubes.Count; i++)
        {
            cubes[i].transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
    public void changeSpeed()
    {
        float tmp = speed;
        speed_text.Select();
        if (float.TryParse(speed_text.text, out speed))
        {
            if (speed <= 0)
            {
                speed = tmp;
                speed_text.text = tmp.ToString();
            }
        }
        else
        {
            speed = tmp;
            speed_text.text = speed.ToString();
        }
    }
    public void changeTime()
    {
        float tmp = time;
        time_text.Select();
        if (float.TryParse(time_text.text, out time))
        {
            if (time <= 0)
            {
                time = tmp;
                time_text.text = tmp.ToString();
            }
        }
        else
        {
            time = tmp;
            time_text.text = time.ToString();
        }
    }
    public void changeDistance()
    {
        float tmp = distance;
        distance_text.Select();
        if (float.TryParse(distance_text.text, out distance))
        {
            if (distance <= 1)
            {
                distance = tmp;
                distance_text.text = tmp.ToString();
            }
        }
        else
        {
            distance = tmp;
            distance_text.text = distance.ToString();
        }
    }
}
