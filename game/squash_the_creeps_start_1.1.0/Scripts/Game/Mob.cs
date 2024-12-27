using Godot;

public partial class Mob : CharacterBody3D
{
   // Meter per second
   [Export]
   public int MinSpeed{get; set;} = 10;

   // Meter per second
   [Export]
   public int MaxSpeed{get; set;} = 18;

    public override void _PhysicsProcess(double delta)
    {
      MoveAndSlide();
    }
    public void Initialize(Vector3 start, Vector3 player){
      LookAtFromPosition(start,player,Vector3.Up);
      RotateY((float)GD.RandRange(-Mathf.Pi / 4.0, Mathf.Pi / 4.0));

      int randSpeed = GD.RandRange(MinSpeed,MaxSpeed);

      Velocity = Vector3.Forward * randSpeed;
      Velocity = Velocity.Rotated(Vector3.Up,Rotation.Y);

    }
    // We also specified this function name in PascalCase in the editor's connection window.
    private void OnVisibilityNotifierScreenExited()
    {
        QueueFree();
    }
}
