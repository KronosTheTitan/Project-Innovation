using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class Path : MonoBehaviour {
        public List<Vector3> points;

        public event Action OnApply;

        public int NumPoints() {
            return points.Count;
        }

        public Vector3 GetPoint(int pointIndex) {
            if (pointIndex<0) {
                Debug.Log("test 1");
                return Vector3.zero;
            }

            if (pointIndex > points.Count)
            {
                Debug.Log("test 2");
                pointIndex -= NumPoints();
            }
            Debug.Log("test 3");
            
            return transform.TransformPoint(points[pointIndex]);
        }

        public void Apply() {
            Debug.Log("Applying curve");
            if (OnApply != null) OnApply();
        }
    }
}