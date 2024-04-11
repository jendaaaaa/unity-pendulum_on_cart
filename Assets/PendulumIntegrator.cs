using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PendulumIntegrator : Integrator
{
    double g_earth = 9.81;
    public double b = 1;
    public double m = 1;
    public double f = 1;
    public double gForce = 1;
    double g;

    public double [] x;
    public void setParams(double b, double m, double f, double gForce){
        this.m = m;
        this.b = b;
        this.f = f;
        this.gForce = gForce;
    }
    public void setIC(double x0, double v0, double u0){
        x = new double[3];
        Init(3);
        x[0] = x0;
        x[1] = v0;
        x[2] = u0;
    }
    public override void RatesOfChange(double[] x, double[] xdot, double t, double u)
    {
        g = (g_earth * gForce);
        xdot[0] = x[1];
        xdot[1] = -(b / m) * x[1] - (g * f / m)*System.Math.Sign(x[1]) + (1 / m) * u;
    }
}
