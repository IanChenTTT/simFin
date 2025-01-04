using Godot;

public partial class Execute : CharacterBody3D
{
   [Export]
   public float max_speed = 50.0f; 
   [Export]
   public float acceleration = 50.0f; 

   [Export]
   public float pitch_speed = 1.5f; 

   [Export]
   public float roll_speed = 1.9f; 
   
   [Export]
   public float yawn_speed = 1.25f; 
   [Export]
   public float input_response = 8.0f;
   
   private float _forward_speed = 0;
   private float _pitch_input = 0;
   private float _roll_input = 0;
   private float _yaw_input = 0;
   
   private void Get_input(float delta)
   {
      // Accelerate forward and backward
      if (Input.IsActionPressed("throttle_up"))
         _forward_speed = Mathf.Lerp(_forward_speed, max_speed, acceleration * delta);
      if (Input.IsActionPressed("throttle_down"))
         _forward_speed = Mathf.Lerp(_forward_speed, 0, acceleration * delta);
      
      //Rotation //For key or joystick
      //_pitch_input = Mathf.Lerp(_pitch_input,Input.GetAxis("",""),delta);
      //_roll_input = Mathf.Lerp(_roll_input,Input.GetAxis("",""),delta);
      _yaw_input = _roll_input;
   }
   public override void _PhysicsProcess(double delta)
   {
      Get_input((float)delta);
      base._PhysicsProcess(delta);

      Velocity = Transform.Basis.Z * _forward_speed;

      // https://randomnerdtutorials.com/esp32-mpu-6050-accelerometer-gyroscope-arduino/ 
      // https://docs.godotengine.org/en/latest/tutorials/3d/using_transforms.html
      // mpu6050 roll pith yall cordinate different from godot

      // in godot z-> roll / mpu x-> roll
      // in godot x-> pitch / mpu y-> pitch
      // in godot y-> yawn / mpu z-> yaw

      // Rotate can use radiant!!

      Rotate(new Vector3(1, 0, 0), _pitch_input); // x axis
      Rotate(new Vector3(0, 1, 0), _roll_input); // y axis
      Rotate(new Vector3(0, 0, 1), _yaw_input);
      MoveAndCollide(Velocity * (float)delta);
   }


}
