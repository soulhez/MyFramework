﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneInstance : GameBase
{
	public string mName;
	public LOAD_STATE mState;
	public Scene mScene;
	public AsyncOperation mOperation;
	public GameObject mRoot;
	public bool mInited;
	public bool mActiveLoaded;      // 加载完毕后是否立即显示
	public SceneLoadCallback mLoadCallback;
	public SceneInstance(string name)
	{
		mName = name;
	}
	public virtual void init()
	{
		if(mInited)
		{
			return;
		}
		findShaders(mRoot);
		findGameObject();
		initGameObject();
		mInited = true;
	}
	public virtual void destroy()
	{
		mInited = false;
	}
	public virtual void update(float elapsedTime){}
	public void setActive(bool active)
	{
		if (mRoot != null && mRoot.activeSelf != active)
		{
			mRoot.SetActive(active);
		}
	}
	public bool getActive()
	{
		return mRoot != null && mRoot.activeSelf;
	}
	public GameObject getRoot() { return mRoot; }
	public virtual void onShow() { }
	public virtual void onHide() { }
	//-------------------------------------------------------------------------------------------
	protected virtual void findGameObject() { }
	protected virtual void initGameObject() { }
}