# UnityBZWTools
Tools to build maps for BZFlag in the Unity 3D Editor

## Goals
The goals of this project are a set of scripts/default projects that will allow
users to build maps for the open source game BZFlag (https://www.bzflag.org)
using the Unity3D Editor (http://www.Unity3D.com).

# Getting started
Read the help wiki
https://github.com/JeffM2501/UnityBZWTools/wiki

# Codebase
The UnityBZWTools contains two main sub projects

## LibBZFlag
This project uses LibBZFlag [https://github.com/JeffM2501/LibBZFlag] to handle bzw loading and saving. It's code is no longer included in this project, just premade libs that unity can use.

## Unity Project
This is a unity project that contains a number of unity editor scripts that allow the IDE to know about BZW objects from BzwIO. These scripts provide menu options, custom data, and custom inspectors inside unity to allow for all the special bzflag data to be edited.

