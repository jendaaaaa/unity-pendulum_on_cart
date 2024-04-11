using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumModel : TimestepModel
{
    public GameObject cart;
    public GameObject ballOnBeam;
    PendulumIntegrator integrator;
    double t = 0.0;
    double u = 0.0;
    // parameters to set
    public double inputGain = 0.1;          // gain of the input from keyboard
    public double gForce = 1;               // gain of g on earth
    public double f = 0.005;                // coulomb friction coeficient
    public double b = 1;                    // viscous friction
    public double m = 100;                  // mass of cart
    public override void TakeStep(float dt)
    {
        t = integrator.RK4Step(integrator.x, t, dt, u);
    }
    void Start()
    {
        integrator = new PendulumIntegrator();
        integrator.setIC(0.5, 0, 0);
        ModelStart();
    }

    void Update()
    {
        integrator.setParams(b, m, f, gForce);
        u = Input.GetAxisRaw("Horizontal") * inputGain;
        Vector3 pos = cart.transform.position;
        pos.x = (float)integrator.x[0];
        cart.transform.position = pos;
    }
}