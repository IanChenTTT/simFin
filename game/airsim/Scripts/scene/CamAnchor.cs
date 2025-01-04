using Godot;

public partial class CamAnchor : Node3D
{
   private Node3D _execute;

   [Export]
   public float FollowSpeed = 4.0f;
   public override void _Ready()
   {
      base._Ready();
      _execute = GetNode<Node3D>("../Execute");
      if(_execute == null)
         GD.PushWarning("Execute not found ACTION _Ready FILE CamAnChor");
   }
   public void LookupAndCacheForFutureAccess()
   {
      GD.Print(_execute.Name);
   }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        this.Position = this.Position.Lerp(_execute.Position,  (float)delta* FollowSpeed);
    }

}
