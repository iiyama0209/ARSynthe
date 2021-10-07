using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCube : MonoBehaviour
{
    public GameObject[] cube;
    private int[] cmajor = { 0, 2, 4, 5, 7, 9, 11, 12 };
    private int[] cminor = { 0, 2, 3, 5, 7, 8, 10, 12 };
    private int[] all = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
    public SoundBall soundball;
    //public Dictionary<string,Vector3> cubesPos = new Dictionary<string,Vector3>();

    Vector3 cube_pos;
    Vector3 init_cube_pos;
    Vector3 cube_scale;

    // Start is called before the first frame update
    void Start()
    {
        //init_cube_pos = soundball.init_head;
        init_cube_pos = new Vector3(0,-1,0);
        init_cube_pos.x -= 0f;
        init_cube_pos.y += 1.0f;
        init_cube_pos.z += 0.6f;
        cube_pos = init_cube_pos;

        cube_scale.x = 0.2f;
        cube_scale.y = 0.07f;
        cube_scale.z = 0.5f;

        cube_arrangement(all);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha0))
        {
            cube_reset();
            cube_arrangement(all);
            Debug.Log("All");
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            cube_reset();
            cube_arrangement(cmajor);
            Debug.Log("CMAJOR");
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            cube_reset();
            cube_arrangement(cminor);
            Debug.Log("CMINOR");
        }
    }

    void Ege(GameObject cube,Vector3 cubeScale)
    {
        var lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        var positions = new Vector3[]
        {
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),

            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),

            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),

            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),
            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z-cubeScale.z/2),

            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),
            new Vector3(cube.transform.position.x-cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),
            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y-cubeScale.y/2,cube.transform.position.z+cubeScale.z/2),
            new Vector3(cube.transform.position.x+cubeScale.x/2,cube.transform.position.y+cubeScale.y/2,cube.transform.position.z+cubeScale.z/2)
        };

        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
    }

    private void cube_reset()
    {
        int i = 0;

        for (i = 0; i < 52; i++)
        {
            cube[i].transform.position = new Vector3(0,-2,0);
            cube[i].GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            cube[i].transform.localScale = new Vector3(1,1,1);
            cube[i].GetComponent<Renderer>().material.color = new Color(1,1,1);


        }
    }

    private void cube_arrangement(int[] array)
    {
        //init_cube_pos = soundball.init_head;
        init_cube_pos = new Vector3(0,-1,0);
        init_cube_pos.x -= 0f;
        init_cube_pos.y += 1.0f;
        init_cube_pos.z += 0.6f;
        cube_pos = init_cube_pos;

        int i = 0;
        int k = 0;

        float R, G, B;
        R = 1.0f;
        G = 0.0f;
        B = 0.0f;
        for (i = 0; i < array.Length*4; i++)
        {
            cube[array[i / 4] * 4 + i % 4].transform.position = cube_pos;
            cube[array[i / 4] * 4 + i % 4].GetComponent<Renderer>().material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
            cube[array[i / 4] * 4 + i % 4].transform.localScale = cube_scale;
            cube[array[i / 4] * 4 + i % 4].GetComponent<Renderer>().material.color = new Color(R, G, B, 0.4f);
            cube[array[i / 4] * 4 + i % 4].GetComponent<BoxCollider>().isTrigger = true;


            if ((i + 1) % 4 == 0)
            {
                R -= 0.08f;
                B += 0.08f;
                G = 0.0f;
            }
            G += 0.24f;

            cube_pos.x -= cube_scale.x;
            k++;
            if (k == 4)
            {
                k = 0;
                cube_pos.x = init_cube_pos.x;
                cube_pos.y += cube_scale.y;
            }
        }
    }
}
