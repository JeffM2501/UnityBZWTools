using UnityEngine;
using UnityEditor;

using System.Collections.Generic;

public class GroundBuilder
{
	public static float GrassUVSCale = 0.125f;
	public static float WallUVScale = 0.25f;
	public static float WallHeight = 20;

	public static void BuildWalls(GameObject obj, BZWWorld world)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if(filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		int offset = 0;

		// Z+ wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, 0));

		verts.Add(new Vector3(-1, WallHeight, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, WallHeight, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.back);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, 0));


		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;

		// Z- wall
		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, 0));

		verts.Add(new Vector3(-1, WallHeight, -1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, WallHeight, -1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.forward);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, 0));

		tris.Add(2 + offset);
		tris.Add(1 + offset);
		tris.Add(0 + offset);

		tris.Add(0 + offset);
		tris.Add(3 + offset);
		tris.Add(2 + offset);
		offset = verts.Count;


		// X+ wall
		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, 0));

		verts.Add(new Vector3(1, WallHeight, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, WallHeight, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, 0));


		tris.Add(0 + offset);
		tris.Add(1 + offset);
		tris.Add(2 + offset);

		tris.Add(2 + offset);
		tris.Add(3 + offset);
		tris.Add(0 + offset);

		offset = verts.Count;


		// X- wall
		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, 0));

		verts.Add(new Vector3(-1, WallHeight, 1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(-world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(-1, WallHeight, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, WallHeight * GrassUVSCale));

		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.left);
		uvs.Add(new Vector2(world.Size * GrassUVSCale, 0));


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

		mesh.name = "GroundWallsMesh";
		filter.sharedMesh = mesh;

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;

		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath("Assets/BZWTools/StandardAssets/Textures/wall.mat", typeof(Material));

		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;

		obj.isStatic = true;
	}

    public static void BuildGrass(GameObject obj, BZWWorld world)
    {
        BuildGroundPlane(obj, world.Size, "Assets/BZWTools/StandardAssets/Textures/std_ground.mat");
    }


    public static void BuildGroundPlane(GameObject obj, float size, string material)
	{
		Mesh mesh = new Mesh();

		MeshFilter filter = obj.GetComponent<MeshFilter>();
		if (filter == null)
			filter = obj.AddComponent<MeshFilter>() as MeshFilter;

		List<Vector3> verts = new List<Vector3>();
		List<Vector3> norms = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();

		verts.Add(new Vector3(-1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-size * GrassUVSCale, size * GrassUVSCale));

		verts.Add(new Vector3(1, 0, 1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(size * GrassUVSCale, size * GrassUVSCale));

		verts.Add(new Vector3(1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(size * GrassUVSCale, -size * GrassUVSCale));
		
		verts.Add(new Vector3(-1, 0, -1));
		norms.Add(Vector3.up);
		uvs.Add(new Vector2(-size * GrassUVSCale, -size * GrassUVSCale));

		List<int> tris = new List<int>();

		tris.Add(0);
		tris.Add(1);
		tris.Add(2);

		tris.Add(2);
		tris.Add(3);
		tris.Add(0);

		mesh.vertices = verts.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.normals = norms.ToArray();
		mesh.triangles = tris.ToArray();

		mesh.name = "GroundMesh";
		filter.sharedMesh = mesh;

		MeshRenderer render = obj.AddComponent<MeshRenderer>() as MeshRenderer;
#if !UNITY_WEBGL
		render.sharedMaterial = (Material)AssetDatabase.LoadAssetAtPath(material, typeof(Material));
#endif
		MeshCollider collider = obj.AddComponent<MeshCollider>() as MeshCollider;
		collider.sharedMesh = mesh;

		obj.transform.localScale = new Vector3(size, 1, size);
		obj.isStatic = true;
	}
}
