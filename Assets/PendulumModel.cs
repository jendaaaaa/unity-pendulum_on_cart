using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumModel : TimestepModel
{
    public GameObject cart;
    public GameObject beam;
    PendulumIntegrator integrator;
    double t = 0.0;
    double u = 0.0;
    // parameters to set
    public double inputGain = 0.01;         // gain of the input from keyboard
    public double gForce = 1;               // gain of g on earth
    public double f = 0.0005;               // coulomb friction coeficient
    public double b_b = 0.005;              // viscous friction of beam
    public double b_c = 0.005;              // viscous friction of cart
    public double mass_b = 1;               // mass of ball
    public double mass_c = 1;               // mass of cart
    public double length = 0.01;            // length of beam
    public double rotation = 0;
    public override void TakeStep(float dt)
    {
        t = integrator.RK4Step(integrator.x, t, dt, u);
    }
    void Start()
    {
        integrator = new PendulumIntegrator();
        integrator.setIC(0, 0, 0, 0);
        ModelStart();
    }

    void Update()
    {
        integrator.setParams(mass_b, mass_c, b_b, b_c, length, f, gForce);
        u = Input.GetAxisRaw("Horizontal") * inputGain;
        Vector3 pos = cart.transform.position;
        Vector3 rot = cart.transform.localEulerAngles;
        pos.x = (float)integrator.x[0];
        rot.z = (float)integrator.x[1]*100;
        cart.transform.position = pos;
        beam.transform.position = pos;
        beam.transform.localEulerAngles = rot;
    }
}