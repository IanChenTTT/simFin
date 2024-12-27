using Godot;

public partial class Main : Node
{
   [Export]
   public PackedScene MobScene {get; set;}
   Node3D player;

   public override void _Ready()
   {
      base._Ready();
      player = GetNode<Node3D>("player");
      if(player == null) GD.PrintErr("ACTION not found player, LOCATE Main.cs _Ready");

      var mobTimer = GetNode<Timer>("MobTimer");
		mobTimer.Timeout += OnMobTimerTimeout;
   }
   private void OnMobTimerTimeout(){
      Mob mob = MobScene.Instantiate<Mob>();

      var mobLocation = GetNode<PathFollow3D>("SpawnPath/SpawnLoaction");
      if(mobLocation == null) GD.PrintErr("ACTION not found SpawnLoaction, LOCATE Main.cs OnMobTimerTimeout");
      mobLocation.ProgressRatio = GD.Randf();

      Vector3 pos = player.Position;
      mob.Initialize(mobLocation.Position, pos);
      AddChild(mob);

   }
}

