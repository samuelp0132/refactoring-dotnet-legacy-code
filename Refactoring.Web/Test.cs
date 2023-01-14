using System;

public class Rectangle : IShape{
    public string BuildShape => "this is a rectangle";   
}

public class Triangle : IShape{
    public string BuildShape => "This is a Rectangle";   
}

public class Circle : IShape{
    public string BuildShape => "This is a Circle";   
}

public class ShapeFactory : IShapeFactory{
    
    public IShape forShape(string shapeName){
        switch(shapeName){
            case "Rectangle": return new Rectangle();
                break;
            case "Triangle": return new Triangle();
                break;
            case "Circle": return new Circle();
                break;
            default: return null;
                break; 
        }
    }
}

public interface IShape{
    string BuildShape { get; }
}

public interface IShapeFactory{
    IShape forShape(string shapeName);
}

public class ProgramProg{
    public void Run(){
        var factory = new ShapeFactory();
        var shape = factory.forShape("Rectangle");
        Console.WriteLine(shape.BuildShape);
    }
}