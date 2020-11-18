using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSphere : MonoBehaviour
{
    public float friction;
    public float mass;
    public float radius;

    [NonSerialized]
    public Vector3 currentV;

    //存储墙的位置
    private float[] x_walls;
    private float[] z_walls;
    // Start is called before the first frame update
    void Start()
    {
        //为墙的位置赋值
        x_walls = new float[] { -19.5f, 19.5f };
        z_walls = new float[] { -9.5f, 9.5f };
    }

    // Update is called once per frame
    void Update()
    {
        //摩擦力
        Vector3 frictionDeltaV = -Time.deltaTime * friction * currentV.normalized;
        //防止摩擦力反向运动
        Vector3 finalV = currentV + frictionDeltaV;
        if (finalV.x * currentV.x <= 0)
            frictionDeltaV.x = -currentV.x;
        if (finalV.y * currentV.y <= 0)
            frictionDeltaV.y = -currentV.y;
        if (finalV.z * currentV.z <= 0)
            frictionDeltaV.z = -currentV.z;

        Vector3 prePos = transform.position;

        //应用加速度
        Vector3 curV = currentV + frictionDeltaV;
        transform.Translate((curV + currentV) * Time.deltaTime / 2);
        currentV = curV;

        //检测是否与墙相撞
        Vector3 pos = transform.position;
        if (((pos.x - x_walls[0]) < 1) || ((x_walls[1] - pos.x) < 1))
        {
            Debug.Log("大球碰墙发生!");
            currentV.x = -currentV.x;
            transform.position = prePos;
        }
        if (((pos.z - z_walls[0]) < 1) || ((z_walls[1] - pos.z) < 1))
        {
            Debug.Log("大球碰墙发生!");
            currentV.z = -currentV.z;
            transform.position = prePos;
        }
    }
}
