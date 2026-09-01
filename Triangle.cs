using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maxwell;

class Triangle
{
    // Triangle is drawn by clockwise order.
    public Vector3 Vertice0;
    public Vector3 Vertice1;
    public Vector3 Vertice2;
    
    public Triangle(Vector3 vertice0, Vector3 vertice1, Vector3 vertice2)
    {
        Vertice0 = vertice0;
        Vertice1 = vertice1;
        Vertice2 = vertice2;
    }
}