namespace SimplePaintWPF.Models;

public class Circle : Shape
{
    public float Radius { get; set; }

    public Circle(float x, float y, float radius, bool isFill = false)
    {
        Position = new Point
        {
            X = x,
            Y = y
        };
        
        Radius = radius;
        IsFill = isFill;
    }
    
    public float Area() => (float) (3.14 * Radius * Radius);
}