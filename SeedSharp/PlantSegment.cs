using System;
using System.Collections.Generic;
using System.Numerics;

namespace SeedSharp
{
    public class PlantSegment
    {
        private float _localPosition;
        private float _angle;
        private float _length;
        private float _startWidth;
        private float _endWidth;
        private bool _isDirty;
        private Matrix3x2 _localToParentMatrix;

        public float LocalPosition {
            get => _localPosition;
            set
            {
                _localPosition = value;
                _isDirty = true;
            }
        }
        public float Angle {
            get => _angle;
            set
            {
                var angle = value;
                for (; -MathF.PI >= angle || angle > MathF.PI;
                    angle -= 2 * MathF.PI * MathF.Sign(angle))
                { }    

                _angle = angle;
                _isDirty = true;
            }
        }
        public float Length {
            get => _length;
            set
            {
                _length = value;
                _isDirty = true;
            }
        }
        public float StartWidth { get => _startWidth; set => _startWidth = value; }
        public float EndWidth { get => _endWidth; set => _endWidth = value; }
        public PlantSegment Next { get; set; }
        public PlantSegment Parent { get; private set; }
        public List<PlantSegment> Children { get; set; }
        public int ChildrenCount
        {
            get
            {
                var nextCount = Next is null ? 0 : 1;
                if (Children is null)
                    return nextCount;
                return Children.Count + nextCount;
            }
            private set { }
        }
        public int SegmentCount
        {
            get
            {
                var count = 1;
                count += Next is null ? 0 : Next.SegmentCount;
                if (Children is not null)
                {
                    foreach(var child in Children)
                    {
                        count += child.SegmentCount;
                    }
                }

                return count;
            }
        }

        public virtual Matrix3x2 LocalToWorldMatrix {
            get
            {
                if (_isDirty)
                {
                    _isDirty = false;
                    var rotation = Matrix3x2.CreateRotation(-Angle);
                    var translation = Matrix3x2.CreateTranslation(0, Parent.Length * LocalPosition);

                    _localToParentMatrix = rotation * translation;
                }

                if (Parent is null)
                    throw new ArgumentNullException("Parent", "Segment does not have a parent");

                var parentToWorldMatrix = Parent.LocalToWorldMatrix;
                return _localToParentMatrix * parentToWorldMatrix;
            }
            private set { }
        }

        public PlantSegment()
        {
            _isDirty = true;
            Children = new List<PlantSegment>();
        }

        public void AddChild(PlantSegment child)
        {
            Children.Add(child);
            child.Parent = this;
        }

        public void SetNext(PlantSegment next)
        {
            Next = next;
            next.Parent = this;
        }
    }
}
