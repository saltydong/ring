using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRing : MonoBehaviour
{
    public ParticleSystem particleSystem; 	//粒子系统对象
    public int particleNumber = 5000;       //最大粒子数
    public float pingPong = 0.05f;
    public float size = 0.05f;             //大小
    public float maxRadius = 10f;          //旋转半径
    public float minRadius = 4.0f;
    public float speed = 0.05f;             //运动速度
    private float[] particleAngle;
    private float[] particleRadius;

    private float time = 0;
    private ParticleSystem.Particle[] particlesArray;

    private Color[] changeColor = { new Color(255, 255, 255), new Color(255, 0, 0), new Color(255, 255, 0), new Color(0, 255, 0), new Color(0, 0, 255) };
    private float colorTimeOut = 0;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        particlesArray = new ParticleSystem.Particle[particleNumber];
        particleSystem.maxParticles = particleNumber;
        particleAngle = new float[particleNumber];
        particleRadius = new float[particleNumber];

        particleSystem.Emit(particleNumber);
        particleSystem.GetParticles(particlesArray);

        init();

        particleSystem.SetParticles(particlesArray, particlesArray.Length);
    }
    void Update()
    {
        colorTimeOut += Time.deltaTime;
        for (int i = 0; i < particleNumber; i++)
        {
            time += Time.deltaTime;
            particlesArray[i].color = changeColor[(int)(colorTimeOut % 5)];
            particleRadius[i] += (Mathf.PingPong(time / minRadius / maxRadius, pingPong) - pingPong / 2.0f);
            if (i % 2 == 0)
            {
                particleAngle[i] += speed * (i % 10 + 1);
            }
            else
            {
                particleAngle[i] -= speed * (i % 10 + 1);
            }
            particleAngle[i] = (particleAngle[i] + 360) % 360;
            float rad = particleAngle[i] / 180 * Mathf.PI;

            particlesArray[i].position = new Vector3(particleRadius[i] * Mathf.Cos(rad), particleRadius[i] * Mathf.Sin(rad), 0f);
        }
        particleSystem.SetParticles(particlesArray, particleNumber);
    }

    void init()
    {
 
        for (int i = 0; i < particleNumber; i++)
        {
         
            float angle = Random.Range(0.0f, 360.0f);
        
            float rad = angle / 180 * Mathf.PI;
           
            float midRadius = (maxRadius + minRadius) / 2;
            float rate1 = Random.Range(1.0f, midRadius / minRadius);
            float rate2 = Random.Range(midRadius / maxRadius, 1.0f);
            float r = Random.Range(minRadius * rate1, maxRadius * rate2);
      
            particlesArray[i].size = size;

            particleAngle[i] = angle;
            particleRadius[i] = r;
      
            particlesArray[i].position = new Vector3(r * Mathf.Cos(rad), r * Mathf.Sin(rad), 0.0f);
        }
    }
}
