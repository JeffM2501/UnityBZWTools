﻿using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class BoxBuilder
{
	public static float BoxFloorUVScale = 0.25f;
	public static float BoxWallUVScale = 0.25f;

	public static void BuildWalls(GameObject obj, BZWBox box)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		Vector3 scale = box.transform.localScale;

		int offset = 0;

		// Z+ wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-scale.x * BoxWallUVScale, 0));

		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-scale.x * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(scale.x * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(scale.x * BoxWallUVScale, 0));

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
		uvs.Add(new Vector2(-scale.x * BoxWallUVScale, 0));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-scale.x * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(scale.x * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(scale.x * BoxWallUVScale, 0));

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
		uvs.Add(new Vector2(-scale.z * BoxWallUVScale, 0));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(-scale.z * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.right);
		uvs.Add(new Vector2(scale.z * BoxWallUVScale, 0));


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
		uvs.Add(new Vector2(-scale.z * BoxWallUVScale, 0));

		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-scale.z * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(scale.z * BoxWallUVScale, scale.y * BoxWallUVScale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(scale.z * BoxWallUVScale, 0));


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

		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/boxwall.mat", typeof(Material));

		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;
		obj.isStatic = true;
	}

	public static void BuildRoof(GameObject obj, BZWBox box)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();

		Vector3 scale = box.transform.localScale;

		int offset = 0;

		// floor
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-scale.x * BoxFloorUVScale, scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(scale.x * BoxFloorUVScale, scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(scale.x * BoxFloorUVScale, -scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-scale.x * BoxFloorUVScale, -scale.z * BoxFloorUVScale));

		List<int> tris = new List<int>();

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);

		offset = verts.Count;

		// roof
		verts.Add(new Vector3(-1, 1, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-scale.x * BoxFloorUVScale, scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(1, 1, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(scale.x * BoxFloorUVScale, scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(1, 1, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(scale.x * BoxFloorUVScale, -scale.z * BoxFloorUVScale));

		verts.Add(new Vector3(-1, 1, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-scale.x * BoxFloorUVScale, -scale.z * BoxFloorUVScale));

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

		mesh.name = "Box";

		filter.sharedMesh = mesh;

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;

		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/roof.mat", typeof(Material));

		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;
		obj.isStatic = true;
	}
}
