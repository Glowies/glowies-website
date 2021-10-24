using System;
namespace SeedSharp
{
    public class Plant
    {
        public RootSegment Root { get; set; }

        public Plant()
        {
            Root = new RootSegment()
            {
                LocalPosition = 0,
                Angle = (float) Math.PI / 2,
                Length = 1,
                StartWidth = .1f,
                EndWidth = .1f
            };

            var branch = new PlantSegment()
            {
                LocalPosition = 1f,
                Angle = (float)Math.PI / 8,
                Length = 2,
                StartWidth = .1f,
                EndWidth = .1f
            };

            var subbranch = new PlantSegment()
            {
                LocalPosition = 1f,
                Angle = (float)Math.PI / 8,
                Length = 1,
                StartWidth = .1f,
                EndWidth = .1f
            };

            Root.SetNext(branch);
            branch.AddChild(subbranch);

        }
    }
}
