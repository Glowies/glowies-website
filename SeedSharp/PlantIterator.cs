using System;

namespace SeedSharp
{
    public class PlantIterator
    {
        private const float _newSegmentLength = 0f;
        public Plant TargetPlant { get; set; }

        public PlantIterator(Plant targetPlant)
        {
            TargetPlant = targetPlant;
            Initialize();
        }

        public void Initialize()
        {
            if (TargetPlant.SegmentCount == 1)
            {
                var branch = new PlantSegment()
                {
                    LocalPosition = 1f,
                    Angle = 2 * MathF.PI,
                    Length = _newSegmentLength
                };

                for (int i = 0; i < 5; i++)
                {
                    if (i == 2)
                        continue;

                    var subbranch = new PlantSegment()
                    {
                        LocalPosition = 1f,
                        Angle = -MathF.PI / 2 + MathF.PI / 4 * i,
                        Length = _newSegmentLength,
                    };
                    TargetPlant.Root.AddChild(subbranch);
                }

                TargetPlant.Root.SetNext(branch);
            }
        }

        public void Iterate(float timeStep)
        {
            Iterate(timeStep, TargetPlant.Root, .1f);
        }

        public void Iterate(float timeStep, PlantSegment segment, float parentSpeed)
        {
            if (segment is null)
                return;

            float stepFactor = timeStep / 1000;

            const float branchInterval = 1.618f;
            const float branchAngle = MathF.PI / 5;

            var angle = segment.Angle;
            float growthSpeed = parentSpeed / 2 * MathF.Cos(angle / 2);

            var prevLength = segment.Length;
            if(segment != TargetPlant.Root)
                segment.Length += stepFactor * growthSpeed;

            // Recursive calls
            Iterate(timeStep, segment.Next, growthSpeed);
            foreach(var child in segment.Children)
            {
                Iterate(timeStep, child, growthSpeed);
            }


            // Try create new branches
            var exponent = MathF.Ceiling(MathF.Log2(prevLength / branchInterval));
            var pow = (int) MathF.Pow(2, exponent);

            if (exponent > -1 && segment.Length > pow * branchInterval)
            {
                for(float i = 0; i < pow; i++)
                {
                    var position = 1f / pow / 2f + i / pow;
                    Console.WriteLine(position);
                    var leftBranch = new PlantSegment()
                    {
                        Angle = -branchAngle,
                        LocalPosition = position,
                        Length = _newSegmentLength
                    };
                    var rightBranch = new PlantSegment()
                    {
                        Angle = branchAngle,
                        LocalPosition = position,
                        Length = _newSegmentLength
                    };

                    segment.AddChild(leftBranch);
                    segment.AddChild(rightBranch);
                }
            }
        }
    }
}
