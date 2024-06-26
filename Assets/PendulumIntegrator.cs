using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PendulumIntegrator : Integrator
{
    double g_earth = 9.81;
    double g;
    public double gForce = 1;
    public double f = 0.0005;
    public double b_b = 0.005;
    public double b_c = 0.005;
    public double m_b = 10;
    public double m_c = 10;
    public double L = 1;


    public double [] x;
    public void setParams(double m_b, double m_c, double b_b, double b_c, double L,  double f, double gForce){
        this.m_b = m_b;
        this.m_c = m_c;
        this.b_b = b_b;
        this.b_c = b_c;
        this.L = L;
        this.f = f;
        this.gForce = gForce;
    }
    public void setIC(double x0, double v0, double phi0, double omega0){
        x = new double[4];
        Init(4);
        x[0] = x0;
        x[1] = v0;
        x[2] = phi0;
        x[3] = omega0;
    }
    public override void RatesOfChange(double[] x, double[] xdot, double t, double u)
    {
        // pre calculate
        g = (g_earth * gForce);
        double sin_x2 = System.Math.Sin(x[2]);
        double cos_x2 = System.Math.Cos(x[2]);
        double sqr_x3 = System.Math.Pow(x[3], 2);
        // calculate derivation
        xdot[0] = x[1];
        xdot[1] = (m_b * L * sqr_x3 * sin_x2
                - m_b * g *  sin_x2 * cos_x2
                - b_c * x[1]
                + u) / (m_c + m_b * System.Math.Pow(sin_x2, 2));
        xdot[2] = x[3];
        xdot[3] = ( - m_b * L * sqr_x3 * sin_x2 * cos_x2
                + (m_b + m_c) * g * sin_x2
                - b_c * x[1] * cos_x2
                + u * cos_x2) / (L * (m_c + m_b * System.Math.Pow(sin_x2, 2)));
    }
}
