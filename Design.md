# Design

Main code is a set of unity 3d C# scripts

Main Editor script
	Installs menu functions
	Loads and saves maps into the current sceene
	

Object Type Scripts
	Adds BZFlag Specific metadata to objects to allow export
	Default object without a type script will be saved as mesh

Prefabs
	Default map objects will be saved as prefabs to allow ease of use
	
		Box
		Pyrmaid
		Teleporter
		Link
		World
		WorldWeapon

Special Objects
	These objects are not exported, but are used to help the exporter
		Remote Texture Manager
		
		
