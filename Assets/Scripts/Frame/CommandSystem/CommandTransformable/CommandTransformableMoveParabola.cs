﻿using UnityEngine;
using System.Collections;

public class CommandTransformableMoveParabola : Command
{
	public KeyFrameCallback mTremblingCallBack;
	public KeyFrameCallback mTrembleDoneCallBack;
	public Vector3 mStartPos;
	public Vector3 mTargetPos;
	public string mName;
	public float mOnceLength;
	public float mOffset;
	public float mAmplitude;
	public float mTopHeight;
	public bool mFullOnce;
	public bool mLoop;
	public override void init()
	{
		base.init();
		mTremblingCallBack = null;
		mTrembleDoneCallBack = null;
		mStartPos = Vector3.zero;
		mTargetPos = Vector3.zero;
		mName = EMPTY_STRING;
		mOnceLength = 1.0f;
		mOffset = 0.0f;
		mAmplitude = 1.0f;
		mTopHeight = 0.0f;
		mFullOnce = false;
		mLoop = false;
	}
	public override void execute()
	{
		ComponentOwner obj = mReceiver as ComponentOwner;
		TransformableComponentMoveParabola component = obj.getComponent(out component);
		// 停止其他移动组件
		obj.breakComponent<IComponentModifyPosition>(component.GetType());
		component.setTremblingCallback(mTremblingCallBack);
		component.setTrembleDoneCallback(mTrembleDoneCallBack);
		component.setActive(true);
		component.setTargetPos(mTargetPos);
		component.setStartPos(mStartPos);
		component.setTopHeight(mTopHeight);
		component.play(mName, mLoop, mOnceLength, mOffset, mFullOnce, mAmplitude);
	}
	public override string showDebugInfo()
	{
		return base.showDebugInfo() + ": mName:" + mName + ", mOnceLength:" + mOnceLength + ", mOffset:" + mOffset + 
			", mStartPos:" + mStartPos + ", mTargetPos:" + mTargetPos + ", mTopHeight:" + mTopHeight + 
			", mLoop:" + mLoop + ", mAmplitude:" + mAmplitude + ", mFullOnce:" + mFullOnce;
	}
}