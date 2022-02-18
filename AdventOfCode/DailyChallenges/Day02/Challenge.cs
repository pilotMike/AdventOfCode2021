using AdventOfCode.Lib;

namespace AdventOfCode.DailyChallenges.Day02;

public class Challenge
{
    
    private static readonly StringExtensions.SpanFactoryFunc<Command> Parse = span =>
    {
        var i = span.IndexOf(' ');
        if (Direction.TryParse(span[..i], true, out Direction direction))
        {
            var distance = int.Parse(span[i..]);
            return new Command(direction, distance);
        }

        throw new Exception("could not parse direction");
    };
    
    readonly record struct Submarine(Location Location)
    {
        public Submarine Move(Command command)
        {
            Location newLocation = command.Direction switch
            {
                Direction.Forward => new(Location.Coordinate + (command.Distance, 0)),
                Direction.Down => new(Location.Coordinate + (0, -command.Distance)),
                Direction.Up => new(Location.Coordinate + (0, command.Distance))
            };

            return new Submarine(newLocation);
        }
    }
    
    public class Part1 : IChallenge
    {
        public string DefaultInput => Input.Part1;
        

        public ChallengeResult Execute(string? input = null)
        {
            if (input == null) input = DefaultInput;

            var finalLocation = input.SplitParse(Parse)
                .Aggregate(new Submarine(), (submarine, command) => submarine.Move(command))
                .Location;

            var result = finalLocation.Coordinate.X * finalLocation.Depth;

            return new ValueChallengeResult<int>(result);
        }
    
    }
    
    
    public class Part2 : IChallenge
    {
        public string DefaultInput => Input.Part1;

        readonly record struct Aim(int Value);

        readonly record struct SubmarineWithAim(Location Location, Aim Aim)
        {
            public SubmarineWithAim Move(Command command)
            {
                (Location location, Aim aim) = command.Direction switch
                {
                    Direction.Forward => (
                        Location + (command.Distance, 0) + new Depth(Aim.Value * command.Distance), 
                        Aim),
                    Direction.Down => (Location, new Aim(Aim.Value + command.Distance)),
                    Direction.Up => (Location, new Aim(Aim.Value - command.Distance))
                };

                return new SubmarineWithAim(location, aim);
            }
        }
            
        
        public ChallengeResult Execute(string? input = null)
        {
            input = input ?? DefaultInput;
        
            //In addition to horizontal position and depth, you'll also need to track a third value, aim,
            // which also starts at 0. The commands also mean something entirely different than you first thought:
            // down X increases your aim by X units.
            //     up X decreases your aim by X units.
            //     forward X does two things:
            // It increases your horizontal position by X units.
            //     It increases your depth by your aim multiplied by X.
            //     Again note that since you're on a submarine, down and up do the opposite of what you might expect:
            //  "down" means aiming in the positive direction.

            var finalLocation = input.SplitParse(Parse)
                .Aggregate(new SubmarineWithAim(), (sub, command) => sub.Move(command))
                .Location;

            var result = finalLocation.Coordinate.X * finalLocation.Depth;

            return new ValueChallengeResult<int>(result);
        }
    }
}