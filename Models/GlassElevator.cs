using Models.Elevator;

public class GlassElevator : Elevator
{
    public GlassElevator()
    {
        this.Type = ElevatorType.Glass;
        this.Description = "A clear (not bullet proof) elevator!";
        this.WeightLimit = 5;
    }    
}