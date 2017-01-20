using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
	using UnityEditor;
#endif

public static class GizmosExt
{
	// Gizmos
	public static void DrawCircle(Vector3 center, float radius)
	{
#if UNITY_EDITOR
		Vector3 rot = Vector3.back;

		var view = SceneView.currentDrawingSceneView;
		if (view != null)
		{
			rot = view.rotation * Vector3.back;
		}

		Handles.color = Gizmos.color;
		Handles.DrawWireDisc(center, rot, radius);
#endif
	}

	// Diamond
	public static void DrawDiamond(Vector3 position)
	{ Gizmos.DrawMesh(GetGizmoMesh("Diamond"), position); }
	public static void DrawDiamond(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawMesh(GetGizmoMesh("Diamond"), position, rotation);	}
	public static void DrawDiamond(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawMesh(GetGizmoMesh("Diamond"), position, rotation, scale); }

	// Diamond Wire
	public static void DrawWireDiamond(Vector3 position)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Diamond"), position); }
	public static void DrawWireDiamond(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Diamond"), position, rotation);	}
	public static void DrawWireDiamond(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Diamond"), position, rotation, scale); }

	// IcoSphere
	public static void DrawIcoSphere(Vector3 position, float scale = 1)
	{ Gizmos.DrawMesh(GetGizmoMesh("IcoSphere"), position, Quaternion.identity, Vector3.one * scale); }

	// IcoSphere Wire
	public static void DrawWireIcoSphere(Vector3 position, float scale = 1)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("IcoSphere"), position, Quaternion.identity, Vector3.one * scale); }

	// Cylinder
	public static void DrawCylinder(Vector3 position)
	{ Gizmos.DrawMesh(GetGizmoMesh("Cylinder"), position); }
	public static void DrawCylinder(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawMesh(GetGizmoMesh("Cylinder"), position, rotation); }
	public static void DrawCylinder(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawMesh(GetGizmoMesh("Cylinder"), position, rotation, scale); }

	// Cylinder Wire
	public static void DrawWireCylinder(Vector3 position)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Cylinder"), position); }
	public static void DrawWireCylinder(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Cylinder"), position, rotation); }
	public static void DrawWireCylinder(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Cylinder"), position, rotation, scale); }

	// Arrow
	public static void DrawArrow(Vector3 position)
	{ Gizmos.DrawMesh(GetGizmoMesh("Arrow"), position); }
	public static void DrawArrow(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawMesh(GetGizmoMesh("Arrow"), position, rotation); }
	public static void DrawArrow(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawMesh(GetGizmoMesh("Arrow"), position, rotation, scale); }

	// Arrow Wire
	public static void DrawWireArrow(Vector3 position)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Arrow"), position); }
	public static void DrawWireArrow(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Arrow"), position, rotation); }
	public static void DrawWireArrow(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("Arrow"), position, rotation, scale); }
	
	// Circle Arrow
	public static void DrawCircleArrow(Vector3 position)
	{ Gizmos.DrawMesh(GetGizmoMesh("CircleArrow"), position); }
	public static void DrawCircleArrow(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawMesh(GetGizmoMesh("CircleArrow"), position, rotation); }
	public static void DrawCircleArrow(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawMesh(GetGizmoMesh("CircleArrow"), position, rotation, scale); }

	// Circle Arrow Wire
	public static void DrawWireCircleArrow(Vector3 position)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("CircleArrow"), position); }
	public static void DrawWireCircleArrow(Vector3 position, Quaternion rotation)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("CircleArrow"), position, rotation); }
	public static void DrawWireCircleArrow(Vector3 position, Quaternion rotation, Vector3 scale)
	{ Gizmos.DrawWireMesh(GetGizmoMesh("CircleArrow"), position, rotation, scale); }

	// Load a mesh from the assets
	public static Mesh GetGizmoMesh(string fname)
	{
		Mesh outMesh = null;

#if UNITY_EDITOR
		outMesh = (Mesh)AssetDatabase.LoadAssetAtPath("Assets/Gizmos/Meshes/" + fname + ".fbx", typeof(Mesh));
#endif

		return outMesh;
	}
}