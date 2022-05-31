namespace SimplePaintWPF.Models;

public class Rectangle : Shape
{
    public float Width { get; set; }
    public float Height { get; set; }
    
    public Rectangle(float x, float y, float width, float height, bool isFill = false)
    {
        Position = new Point
        {
            X = x,
            Y = y
        };
        
        Width = width;
        Height = height;
        IsFill = isFill;
    }
}