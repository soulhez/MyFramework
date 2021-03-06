﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 用于管理变换的关键帧文件,主要是用于给Path类的组件提供参数
// 文件可使用PathRecorder进行录制生成
public class PathKeyframeManager : FrameComponent
{
    protected Dictionary<string, Dictionary<float, Vector3>> mTranslatePathList;    // key是文件名,value是对应的位置关键帧列表
	protected Dictionary<string, Dictionary<float, Vector3>> mRotatePathList;		// key是文件名,value是对应的位置关键帧列表
	protected Dictionary<string, Dictionary<float, Vector3>> mScalePathList;		// key是文件名,value是对应的位置关键帧列表
	protected Dictionary<string, Dictionary<float, float>> mAlphaPathList;			// key是文件名,value是对应的位置关键帧列表
	public PathKeyframeManager(string name)
		:base(name)
	{
		mTranslatePathList = new Dictionary<string, Dictionary<float, Vector3>>();
		mRotatePathList = new Dictionary<string, Dictionary<float, Vector3>>();
		mScalePathList = new Dictionary<string, Dictionary<float, Vector3>>();
		mAlphaPathList = new Dictionary<string, Dictionary<float, float>>();
	}
	public override void destroy()
	{
		mTranslatePathList.Clear();
		mRotatePathList.Clear();
		mScalePathList.Clear();
		mAlphaPathList.Clear();
		base.destroy();
	}
	public override void init()
	{
		base.init();
		// 加载所有关键帧
		// 平移
		readAllFile(mTranslatePathList, ".translate");
		// 旋转
		readAllFile(mRotatePathList, ".rotate");
		// 缩放
		readAllFile(mScalePathList, ".scale");
		// 透明度
		readAllFile(mAlphaPathList, ".alpha");
	}
	public Dictionary<float, Vector3> getTranslatePath(string fileName)
	{
		return mTranslatePathList.ContainsKey(fileName) ? mTranslatePathList[fileName] : null;
	}
	public Dictionary<float, Vector3> getRotatePath(string fileName)
	{
		return mRotatePathList.ContainsKey(fileName) ? mRotatePathList[fileName] : null;
	}
	public Dictionary<float, Vector3> getScalePath(string fileName)
	{
		return mScalePathList.ContainsKey(fileName) ? mScalePathList[fileName] : null;
	}
	public Dictionary<float, float> getAlphaPath(string fileName)
	{
		return mAlphaPathList.ContainsKey(fileName) ? mAlphaPathList[fileName] : null;
	}
	//----------------------------------------------------------------------------------------------------------------------------
	protected void readAllFile(Dictionary<string, Dictionary<float, Vector3>> list, string suffix)
	{
		List<string> fileList = mListPool.newList(out fileList);
		findStreamingAssetsFiles(CommonDefine.F_PATH_KEYFRAME_PATH, fileList, suffix, true, true);
		int fileCount = fileList.Count;
		for (int i = 0; i < fileCount; ++i)
		{
			Dictionary<float, Vector3> pathList = new Dictionary<float, Vector3>();
			readPathFile(fileList[i], pathList);
			list.Add(getFileNameNoSuffix(fileList[i], true), pathList);
		}
		mListPool.destroyList(fileList);
	}
	protected void readAllFile(Dictionary<string, Dictionary<float, float>> list, string suffix)
	{
		List<string> fileList = mListPool.newList(out fileList);
		findStreamingAssetsFiles(CommonDefine.F_PATH_KEYFRAME_PATH, fileList, suffix, true, true);
		int fileCount = fileList.Count;
		for (int i = 0; i < fileCount; ++i)
		{
			Dictionary<float, float> pathList = new Dictionary<float, float>();
			readPathFile(fileList[i], pathList);
			list.Add(getFileNameNoSuffix(fileList[i], true), pathList);
		}
		mListPool.destroyList(fileList);
	}
	protected void readPathFile(string filePath, Dictionary<float, Vector3> path)
	{
		string str = openTxtFile(filePath, true);
		string[] lines = split(str, true, "\n");
		int lineCount = lines.Length;
		for(int i = 0; i < lineCount; ++i)
		{
			string[] elems = split(lines[i], true, ":");
			if (elems.Length == 2)
			{
				path.Add(stringToFloat(elems[0]), stringToVector3(elems[1]));
			}
			else
			{
				logError(filePath + "第" + i + "行错误:" + lines[i]);
			}
		}
	}
	protected void readPathFile(string filePath, Dictionary<float, float> path)
	{
		string str = openTxtFile(filePath, true);
		string[] lines = split(str, true, "\n");
		int lineCount = lines.Length;
		for (int i = 0; i < lineCount; ++i)
		{
			string[] elems = split(lines[i], true, ":");
			if (elems.Length == 2)
			{
				path.Add(stringToFloat(elems[0]), stringToFloat(elems[1]));
			}
			else
			{
				logError(filePath + "第" + i + "行错误:" + lines[i]);
			}
		}
	}
}
