using AI;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class CurveEditor : Editor {
	private Path curve;

	private void OnEnable() {
		curve = (Path)target;
	}

    // TODO (1.1): Add a button to the Curve's inspector

    public override void OnInspectorGUI()
    {
	    if (GUILayout.Button("Apply"))
	    {
		    curve.Apply();
	    }
	    
	    base.OnInspectorGUI();
    }

    // This method is called by Unity whenever it renders the scene view.
    // We use it to draw gizmos, and deal with changes (dragging objects)
    void OnSceneGUI() {
	    
		if (curve.points == null)
			return;

		//DrawAndMoveCurve();

		// Add new points if needed:
		Event e = Event.current;
		if (e.type == EventType.KeyDown && e.keyCode == KeyCode.Space) {
			Debug.Log("Space pressed - trying to add point to curve");
			
			Undo.RecordObject(curve, "Added point");
			EditorUtility.SetDirty(curve);
			
			AddPoint();
			
			e.Use(); // To prevent the event from being handled by other editor functionality
		}

		ShowAndMovePoints();
	}

	// Example: here's how to draw a handle:
	void DrawAndMoveCurve() {
		Transform handleTransform = curve.transform;
		Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ?
			handleTransform.rotation : Quaternion.identity;

		EditorGUI.BeginChangeCheck();
		Vector3 newPosition = Handles.PositionHandle(handleTransform.position, handleRotation);
		if (EditorGUI.EndChangeCheck()) {
			Undo.RecordObject(curve.transform, "Move curve"); 
			EditorUtility.SetDirty(curve);
			curve.transform.position = newPosition;
		}
	}

	// Tries to add a point to the curve, where the mouse is in the scene view.
	// Returns true if a change was made.
	void AddPoint() {
		Transform handleTransform = curve.transform;

		Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			Debug.Log("Adding spline point at mouse position: " + hit.point);
			// TODO (1.2): Add this action to the undo list and mark the scene dirty
			curve.points.Add(handleTransform.InverseTransformPoint(hit.point));
		}
	}

	// Show points in scene view, and check if they're changed:
	void ShowAndMovePoints() {
		Transform handleTransform = curve.transform;

		Vector3 previousPoint = Vector3.zero;
		for (int i = 0; i < curve.points.Count; i++) {
			Vector3 currentPoint = curve.GetPoint(i);

			// TODO (1.2): Draw a line from previous point to current point, in white
			
			Handles.color = Color.white;
			Handles.DrawLine(previousPoint,currentPoint);
			
			previousPoint = currentPoint;

			// TODO (1.2): 
			// Draw position handles (see the above example code)
			// Record in the undo list and mark the scene dirty when the handle is moved.
			Quaternion handleRotation = Quaternion.identity;

			EditorGUI.BeginChangeCheck();
			Vector3 newPosition = Handles.PositionHandle(currentPoint, handleRotation);
			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(curve, "Move Point"); 
				EditorUtility.SetDirty(curve);
				curve.points[i] = newPosition;
				curve.Apply();
			}
		}
	}
}
