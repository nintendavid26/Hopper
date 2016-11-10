using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Parabola
{
    ///<summary>
    ///a(x-h)^2+k=y
    ///</summary>
    public float a, h, k;
    public float vx, vy, px, py; 
    public Parabola(Vector3 point, Vector3 vertex)
    {
        px = point.x;
        py = point.y;
        vx= h = vertex.x;//v=(-2,-2) p=(-1,1)
        vy= k = vertex.y;//a(x+2)^2-2=y
        a = (point.y - k) / ((point.x - h) * (point.x - h));//a=(y-k)/(x-h)^2
    }
    public void Change(Vector3 point, Vector3 vertex)
    {
        px = point.x;
        py = point.y;
        vx = h = vertex.x;//v=(-2,-2) p=(-1,1)
        vy = k = vertex.y;//a(x+2)^2-2=y
        a = (point.y - k) / ((point.x - h) * (point.x - h));//a=(y-k)/(x-h)^2
    }
    public Parabola(float H, float A, float K)
    {
        h = H;
        a = A;
        k = K;
    }

    public Parabola()
    {
    }

    public void Update(Vector3 point, Vector3 vertex)
    {
        px = point.x;
        py = point.y;
        vx = h = vertex.x;//v=(-2,-2) p=(-1,1)
        vy = k = vertex.y;//a(x+2)^2-2=y
        a = (point.y - k) / ((point.x - h) * (point.x - h));//a=(y-k)/(x-h)^2
    }

    public float XtoY(float x)
    {
        
        return a * (x - h) * (x - h) + k; //cant use ^ with floats
    }
    public float Length() //This isn't great because it uses straight lines, but I don't feel like using calculus
    {
        return 2*Mathf.Sqrt((vx - px) * (vx - px) + (vy - py) * (vy - py));
    }
    public float Height()
    {
        return vy - py;
    }
    public float Width()
    {
        return Mathf.Abs(vx - px);
    }
    public List<Vector3> GeneratePoints(float startx, float endx, float delta = 0.1f)
    {
        float currx = startx;
        List<Vector3> points = new List<Vector3>();
        bool collision = false;
        if (startx < endx)
        {
            endx += 10;
            while (currx < endx || collision)
            {
                Vector3 temp = new Vector3(currx, XtoY(currx), 0);
                points.Add(temp);
               // if(Physics2D.OverlapCircle(points[points.Count - 1], 0.1f) != null)  {collision = true; }
                currx += delta;
                Vector3 temp2 = Camera.main.WorldToViewportPoint(temp);
                if (temp2.y < 0||temp2.x>1||temp2.x<0) {
                    return points;
                }
            }
        }
        else
        {
            endx -= 10;
            while (currx > endx || collision)
            {
                Vector3 temp = new Vector3(currx, XtoY(currx), 0);
                points.Add(temp);
                // if(Physics2D.OverlapCircle(points[points.Count - 1], 0.1f) != null)  {collision = true; }
                currx -= delta;
                Vector3 temp2 = Camera.main.WorldToViewportPoint(temp);
                if (temp2.y < 0 || temp2.x > 1 || temp2.x < 0)
                {
                    return points;
                }
            }
        }
        return points;
    }
    public string toString()
    {
        return "y=" + a + "(x-" + h + ")^2+" + k;
    }
    public string toString(float x)
    {
        return XtoY(x) + "=" + a + "(" + x + "-" + h + ")^2+" + k;
    }
    ///<summary>
    ///2a(x-h)=Slope because of derivatives
    ///</summary>
    public float Slope(float x)
    {
        return 2 * a * (x - h);
    }

}
