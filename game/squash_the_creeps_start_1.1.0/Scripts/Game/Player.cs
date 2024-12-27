using Godot;

public partial class Player : CharacterBody3D
{
   [Export]
   public int Speed {get; set;} = 14;
   [Export]
   public int FallAcc{get; set;} = 75;

   private Vector3 TarVel = Vector3.Zero;

   private Vector3 dir = Vector3.Zero; 
   private Node3D pivot;
   public override void _Ready()
   {
      base._Ready();
      pivot = GetNode<Node3D>("pivot");
      if(pivot == null) GD.PrintErr("ACTION not found pivot, LOCATE Player.cs _Ready");
   }
   private void InputAction()
   {
      dir = Vector3.Zero;
      if(Input.IsActionPressed("move_left")){
         dir.X --;
      }
      if(Input.IsActionPressed("move_right")){
         dir.X ++;
      }
      if(Input.IsActionPressed("move_forward")){
         dir.Z--;
      }
      if(Input.IsActionPressed("move_back")){
         dir.Z++;
      }
      if(Input.IsActionPressed("jump")){

      }
   }
   
   public void Motion(float delta)
   {
      //Ground velocity
      TarVel.X = dir.X * Speed;
      TarVel.Z = dir.Z * Speed;
      
      //Vertical veloctiy
      if(!IsOnFloor()){
         TarVel.Y -= FallAcc * delta;
      }
   }
   public override void _PhysicsProcess(double delta){
      InputAction();
      if(dir != Vector3.Zero){
         dir = dir.Normalized();
         pivot.Basis = Basis.LookingAt(dir);
      }

      Motion((float)delta);
      Velocity = TarVel;
      MoveAndSlide();
   }
};
