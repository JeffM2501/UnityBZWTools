using UnityEngine;

using System.Collections.Generic;

public class PyramidBuilder
{
	public static float PyrmaidUVScale = 0.25f;
	public static float CenterUV = 0;

	private static Mesh BuildNormalMesh(GameObject obj, BZWPyramid pyr)
	{
		Mesh mesh = new Mesh();
		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		int offset = 0;

		Vector3 pyrScale = pyr.transform.localScale;

		// Z+ wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 1, 0));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 1, 0));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;


		// X+ wall
		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-pyrScale.z * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 1, 0));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(pyrScale.z * PyrmaidUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// X- wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-pyrScale.z * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 1, 0));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(pyrScale.z * PyrmaidUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// floor
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, -pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, -pyrScale.z * PyrmaidUVScale));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);
		offset = verts.Count;

		// add our hard work

		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = norms.ToArray();
		mesh.triangles = tris.ToArray();

		return mesh;
	}

	private static Mesh BuildInvertedMesh(GameObject obj, BZWPyramid pyr)
	{
		Mesh mesh = new Mesh();
		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		int offset = 0;

		Vector3 pyrScale = pyr.transform.localScale;

		// Z+ wall
		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 0, 0));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 0, 0));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;


		// X+ wall
		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-pyrScale.z * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 0, 0));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(pyrScale.z * PyrmaidUVScale, 0));

		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// X- wall
		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-pyrScale.z * PyrmaidUVScale, 0));

		verts.Add(new Vector3(0, 0, 0));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(CenterUV, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(pyrScale.z * PyrmaidUVScale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// floor
		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(pyrScale.x * PyrmaidUVScale, -pyrScale.z * PyrmaidUVScale));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-pyrScale.x * PyrmaidUVScale, -pyrScale.z * PyrmaidUVScale));

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

		return mesh;
	}

	public static void Build(GameObject obj, BZWPyramid pyr)
	{
		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		

		if (pyr.FlipZ)
			filter.sharedMesh = BuildInvertedMesh(obj, pyr);
		else
			filter.sharedMesh = BuildNormalMesh(obj,pyr);

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;
#if !UNITY_WEBGL
		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/pyrwall.mat", typeof(Material));
#endif
		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = filter.sharedMesh;

		obj.isStatic = true;
	}
}
