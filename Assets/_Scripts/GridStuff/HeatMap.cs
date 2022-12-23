using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;
using System.Drawing;


public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    public Point(int x, int y)
    {
      //  X = x;
       // Y = y;
    }

}
public class HeatMap
{
    private int width;
    private int height;
    private int[][] HM;

    public bool cast(int x, int y, int endx, int endy)
    {


        return true;
    }
    public bool checkXY(int x, int y)
    {
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return false;
        }
        return true;
    }
    public bool setValue(int x, int y, int val)
    {
        if (x > width || x < 0 || y > height || height < 0)
        {
            return false;
        }
        HM[x][y] = val;
        return true;

    }

    public bool addValue(int x, int y, int val)
    {
        if (x > width || x < 0 || y > height || height < 0)
        {
            return false;
        }
        HM[x][y] += val;
        return true;

    }
    public HeatMap(int[][] hm, int width, int height)
    {
        this.width = width;
        this.height = height;
        this.HM = hm;
    }








    public int getValue(int x, int y)
    {
        if (x < 0 || x > width || y < 0 || y > height)
        {
            return 0;
        }
        return HM[x][y];

    }

    public void radiate(Tile node, int distance)
    {
        int x = node.x;
        int y = node.y;
        int nx = (node.x - distance);
        int ny = (node.y - distance);
        int py = (node.y + distance);
        int px = (node.x + distance);
        if (nx < 0) { nx = 0; }
        if (ny < 0) { ny = 0; }
        if (px > width) { px = width; }
        if (py > height) { py = height; }

        int rays = 100;
        int obs1 = 0;
        int obs2 = 0;
        HM[x][y] = 0;
        for (int k = 0; k < rays; k++)
        {
            int j1 = y;
            int j2 = y;
            obs1 = 0;
            obs2 = 0;
            for (int i = x; i < px; i++)
            {
                double m = i;
                if ((k / m) % 1 == 0)
                {
                    j1++;
                    j2--;
                }
                if (j1 > height - 1) j1 = height - 1;
                if (j2 < 0) j2 = 0;
                m = 1;

                if (HM[i][j1] < 0)
                {
                    obs1 = HM[i][j1];

                }
                else
                {
                    HM[i][j1] = obs1;
                }
                if (HM[i][j2] < 0)
                {
                    obs2 = HM[i][j2];
                }
                else
                {
                    HM[i][j2] = obs2;
                }


            }
            j1 = y;
            j2 = y;
            obs1 = 0;
            obs2 = 0;
            for (int i = x; i > nx; i--)
            {
                double m = i;
                if ((k / m) % 1 == 0)
                {
                    j1++;
                    j2--;
                }
                if (j1 > height - 1) j1 = height - 1;
                if (j2 < 0) j2 = 0;
                m = 1;
                if (HM[i][j1] < 0)
                {
                    obs1 = HM[i][j1];
                }
                else
                {
                    HM[i][j1] = obs1;
                }
                if (HM[i][j2] < 0)
                {
                    obs2 = HM[i][j2];
                }
                else
                {
                    HM[i][j2] = obs2;
                }



            }



            j1 = x;
            j2 = x;
            obs1 = 0;
            obs2 = 0;
            for (int i = y; i < py; i++)
            {
                double m = i;
                if ((k / m) % 1 == 0)
                {
                    j1++;
                    j2--;
                }
                m = 1;
                if (j1 > width - 1) j1 = width - 1;
                if (j2 < 0) j2 = 0;
                if (HM[j1][i] < 0)
                {
                    obs1 = HM[j1][i];
                }
                else
                {
                    HM[j1][i] = obs1;
                }
                if (HM[j2][i] < 0)
                {
                    obs2 = HM[j2][i];
                }
                else
                {
                    HM[j2][i] = obs2;
                }
            }
            j1 = x;
            j2 = x;
            obs1 = 0;
            obs2 = 0;
            for (int i = y; i > ny; i--)
            {
                double m = i;
                if ((k / m) % 1 == 0)
                {
                    j1++;
                    j2--;
                }
                m = 1;
                if (j1 > width - 1) j1 = width - 1;
                if (j2 < 0) j2 = 0;
                if (HM[j1][i] < 0)
                {
                    obs1 = HM[j1][i];
                }
                else
                {
                    HM[j1][i] = obs1;
                }
                if (HM[j2][i] < 0)
                {
                    obs2 = HM[j2][i];
                }
                else
                {
                    HM[j2][i] = obs2;
                }

            }

        }
    }

    public bool drawLine(int x, int y, int x2, int y2)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Math.Abs(w);
        int shortest = Math.Abs(h);
        if (!(longest > shortest))
        {
            longest = Math.Abs(h);
            shortest = Math.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            //putpixel(x,y,color) ;
            if (HM[x][y] < 0)
            {
                return false;
            }
            numerator += shortest;
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
        return true;
    }

    public void drawclose(int x, int y, int x2, int y2, out int z, out int t)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
        int longest = Math.Abs(w);
        int shortest = Math.Abs(h);
        int z1 = x;
        int t1 = y;
        z = x;
        t = y;
        if (!(longest > shortest))
        {
            longest = Math.Abs(h);
            shortest = Math.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }
        int numerator = longest >> 1;
        for (int i = 0; i <= longest; i++)
        {
            //putpixel(x,y,color) ;
            if (HM[x][y] < 0)
            {
                if (z1 == z && t1 == t)
                {
                    if (HM[x + 1][y] == 0)
                    {
                        z1 = x + 1;
                        t1 = y;
                    }
                    else if (HM[x - 1][y] == 0)
                    {
                        z1 = x - 1;
                        t1 = y;
                    }
                    else if (HM[x + 1][y - 1] == 0)
                    {
                        z1 = x + 1;
                        t1 = y - 1;
                    }
                    else if (HM[x - 1][y + 1] == 0)
                    {
                        z1 = x - 1;
                        t1 = y + 1;
                    }
                    else if (HM[x + 1][y + 1] == 0)
                    {
                        z1 = x + 1;
                        t1 = y + 1;
                    }
                    else if (HM[x - 1][y - 1] == 0)
                    {
                        z1 = x - 1;
                        t1 = y - 1;
                    }
                    else if (HM[x][y - 1] == 0)
                    {
                        z1 = x;
                        t1 = y - 1;
                    }
                    else if (HM[x][y + 1] == 0)
                    {
                        z1 = x;
                        t1 = y + 1;
                    }
                    else
                    {

                       // int j = 0;
                    }
                }
                z = z1;
                t = t1;

                return;
            }
            // int numerator2 = numerator;
            numerator += shortest;
            if (HM[x][y] == 0)
            {
                //                z = x;
                //                t = y;
                z1 = x;
                t1 = y;
            }
            if (!(numerator < longest))
            {
                numerator -= longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }

        }

        return;
    }

    public struct vert
    {
        public float x;
        public float y;
    }

}