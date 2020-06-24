﻿using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDestroyWindow(txUIObject window);

public class WindowPool<T> where T : txUIObject, new()
{
	protected List<T> mInusedList;
	protected List<T> mUnusedList;
	protected T mTemplate;
	protected LayoutScript mScript;
	protected OnDestroyWindow mDestroyCallback;
	public WindowPool(LayoutScript script)
	{
		mScript = script;
		mInusedList = new List<T>();
		mUnusedList = new List<T>();
	}
	public void destroy(){}
	public void setTemplate(T template) { mTemplate = template; }
	public T newWindow(string name, txUIObject parent)
	{
		T window = null;
		// 从未使用列表中获取
		if (mUnusedList.Count > 0)
		{
			window = mUnusedList[mUnusedList.Count - 1];
			mUnusedList.RemoveAt(mUnusedList.Count - 1);
		}
		// 未使用列表中没有就创建新窗口
		if (window == null)
		{
			if(mTemplate != null)
			{
				window = mScript.cloneObject(parent, mTemplate, name);
			}
			else
			{
				window = mScript.createObject<T>(name);
			}
		}
		mInusedList.Add(window);
		window.setActive(true);
		window.setName(name);
		window.setParent(parent);
		return window as T;
	}
	public void unuseAll()
	{
		foreach(var item in mInusedList)
		{
			if (mDestroyCallback != null)
			{
				mDestroyCallback(item);
			}
			else
			{
				item.setActive(false);
			}
		}
		mInusedList.Clear();
		mUnusedList.AddRange(mInusedList);
	}
	public void unuseWindow(T window)
	{
		if (window == null)
		{
			return;
		}
		if(!mInusedList.Contains(window))
		{
			return;
		}
		mInusedList.Remove(window);
		mUnusedList.Add(window);
		if (mDestroyCallback != null)
		{
			mDestroyCallback(window);
		}
		else
		{
			window.setActive(false);
		}
	}
	public List<T> getWindowList() { return mInusedList; }
	public void setDestroyCallback(OnDestroyWindow callback) { mDestroyCallback = callback; }
}