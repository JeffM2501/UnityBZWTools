using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class TeleporterBuilder
{

	public static float TeleporterFrameUVScale = 1.0f;
	public static float TeleporterFieldUVScale = 0.025f;

	public static void BuildField(GameObject obj, BZWTeleporter tp)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		Vector3 scale = tp.transform.localScale;

		int offset = 0;

		// Z+ wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-scale.x * TeleporterFieldUVScale, 0));

		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-scale.x * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(scale.x * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(scale.x * TeleporterFieldUVScale, 0));

 		tris.Add(2 + offset);
 		tris.Add(1 + offset);
 		tris.Add(0 + offset);
 
 		tris.Add(0 + offset);
 		tris.Add(3 + offset);
 		tris.Add(2 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-scale.x * TeleporterFieldUVScale, 0));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-scale.x * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(scale.x * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(scale.x * TeleporterFieldUVScale, 0));

 		tris.Add(0 + offset);
 		tris.Add(1 + offset);
 		tris.Add(2 + offset);
 
 		tris.Add(2 + offset);
 		tris.Add(3 + offset);
 		tris.Add(0 + offset);

		offset = verts.Count;

		// X+ wall
		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-scale.z * TeleporterFieldUVScale, 0));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-scale.z * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * TeleporterFieldUVScale, 0));


		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// X- wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-scale.z * TeleporterFieldUVScale, 0));

		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-scale.z * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(scale.z * TeleporterFieldUVScale, scale.y * TeleporterFieldUVScale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(scale.z * TeleporterFieldUVScale, 0));


		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;


		// add our hard work
		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = norms.ToArray();
		mesh.triangles = tris.ToArray();

		filter.sharedMesh = mesh;

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;

		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/telelink.mat", typeof(Material));

		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;
		obj.isStatic = true;
	}

	public static void BuildFrame(GameObject obj, BZWTeleporter tp)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		Vector3 scale = tp.transform.localScale;

		float borderFactorZ = tp.Border / scale.z;
		float borderFactorY = tp.Border / scale.y;
		float borderFactorX = tp.Border / scale.x;

		int offset = 0;

		// left side
		// Z+ wall
		verts.Add(new Vector3(-borderFactorX, 0, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(-borderFactorX, 1, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, scale.y * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, scale.y * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 0, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-borderFactorX, 0, 1 + borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, (scale.y + tp.Border) * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, (scale.y + tp.Border) * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 0, 1 + borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// X+ wall
		verts.Add(new Vector3(borderFactorX, 0, 1 + borderFactorZ)); // bottom right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(borderFactorZ * TeleporterFrameUVScale, 0.5f));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));	// top right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(borderFactorZ * TeleporterFrameUVScale, ((scale.y + tp.Border) * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(borderFactorX, 1, 1));	// top left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(0, (scale.y * TeleporterFrameUVScale) + 0.5f));	

		verts.Add(new Vector3(borderFactorX, 0, 1)); // bottom left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(0, 0.5f));


		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// X- wall
		verts.Add(new Vector3(-borderFactorX, 0, 1 + borderFactorZ)); // bottom right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-borderFactorZ * TeleporterFrameUVScale, 0.5f));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));    // top right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-borderFactorZ * TeleporterFrameUVScale, ((scale.y + tp.Border) * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(-borderFactorX, 1, 1));    // top left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(0, (scale.y * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(-borderFactorX, 0, 1)); // bottom left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(0, 0.5f));


		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;



		// right side
		// Z+ wall
		verts.Add(new Vector3(-borderFactorX, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(-borderFactorX, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, scale.y * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, scale.y * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-borderFactorX, 0, -1 - borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-borderFactorX * TeleporterFrameUVScale, (scale.y + tp.Border) * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, (scale.y + tp.Border) * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 0, -1 - borderFactorZ));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(borderFactorX * TeleporterFrameUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// X+ wall
		verts.Add(new Vector3(borderFactorX, 0, -1 - borderFactorZ)); // bottom right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(borderFactorZ * TeleporterFrameUVScale, 0.5f));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));    // top right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(borderFactorZ * TeleporterFrameUVScale, ((scale.y + tp.Border) * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(borderFactorX, 1, -1));    // top left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(0, (scale.y * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(borderFactorX, 0, -1)); // bottom left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(0, 0.5f));


		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// X- wall
		verts.Add(new Vector3(-borderFactorX, 0, -1 - borderFactorZ)); // bottom right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-borderFactorZ * TeleporterFrameUVScale, 0.5f));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));    // top right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-borderFactorZ * TeleporterFrameUVScale, ((scale.y + tp.Border) * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(-borderFactorX, 1, -1));    // top left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(0, (scale.y * TeleporterFrameUVScale) + 0.5f));

		verts.Add(new Vector3(-borderFactorX, 0, -1)); // bottom left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(0, 0.5f));


		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// top bar

		// roof 
		verts.Add(new Vector3(-borderFactorX, 1  +borderFactorY, 1 + borderFactorZ));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2((-scale.x - tp.Border) * TeleporterFrameUVScale, scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2((scale.x + tp.Border) * TeleporterFrameUVScale, scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2((scale.x + tp.Border) * TeleporterFrameUVScale, -scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2((-scale.x - tp.Border) * TeleporterFrameUVScale, -scale.z * TeleporterFrameUVScale));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// under hang 
		verts.Add(new Vector3(-borderFactorX, 1, 1));
		norms.Add(Vector3.down);
		uvs.Add(new Vector2((-scale.x ) * TeleporterFrameUVScale, scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1, 1));
		norms.Add(Vector3.down);
		uvs.Add(new Vector2((scale.x) * TeleporterFrameUVScale, scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(borderFactorX, 1, -1));
		norms.Add(Vector3.down);
		uvs.Add(new Vector2((scale.x) * TeleporterFrameUVScale, -scale.z * TeleporterFrameUVScale));

		verts.Add(new Vector3(-borderFactorX, 1, -1 ));
		norms.Add(Vector3.down);
		uvs.Add(new Vector2((-scale.x) * TeleporterFrameUVScale, -scale.z * TeleporterFrameUVScale));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;


		// X+ wall
		verts.Add(new Vector3(borderFactorX, 1, 1)); // bottom right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));    // top right
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * TeleporterFrameUVScale, (tp.Border * TeleporterFrameUVScale)));

		verts.Add(new Vector3(borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));    // top left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-scale.z * TeleporterFrameUVScale, (tp.Border * TeleporterFrameUVScale)));

		verts.Add(new Vector3(borderFactorX, 1, -1)); // bottom left
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-scale.z * TeleporterFrameUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;


		// X- wall
		verts.Add(new Vector3(-borderFactorX, 1, 1)); // bottom right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(scale.z * TeleporterFrameUVScale, 0));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, 1 + borderFactorZ));    // top right
		norms.Add(Vector3.left);
		uvs.Add(new Vector2((scale.z + tp.Border) * TeleporterFrameUVScale, (tp.Border * TeleporterFrameUVScale)));

		verts.Add(new Vector3(-borderFactorX, 1 + borderFactorY, -1 - borderFactorZ));    // top left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2((-scale.z - tp.Border) * TeleporterFrameUVScale, (tp.Border * TeleporterFrameUVScale)));

		verts.Add(new Vector3(-borderFactorX, 1, -1)); // bottom left
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-scale.z * TeleporterFrameUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;


		// add our hard work
		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = norms.ToArray();
		mesh.triangles = tris.ToArray();

		filter.sharedMesh = mesh;

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;

		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/caution.mat", typeof(Material));

		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;
		obj.isStatic = true;
	}
}
