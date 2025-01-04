using System;
using System.Diagnostics;
using Godot;

public partial class Execute : StaticBody3D
{
   public override void _Input(InputEvent @event)
   {
      if (@event is InputEventKey keyEvent && keyEvent.Pressed)
      {
         switch(keyEvent.Keycode){
            case Key.W:
               GD.Print("W was pressed");
            break;
            default:
               break;
         }
      }
   }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
    }


}
