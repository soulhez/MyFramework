﻿using UnityEngine;
using System.Collections;

public class CommandTransformableLerpRotation : Command
{
	public LerpCallback mLerpingCallBack;
	public LerpCallback mLerpDoneCallBack;
	public Vector3 mTargetRotation;
	public float mLerpSpeed;
	public override void init()
	{
		base.init();
		mLerpingCallBack = null;
		mLerpDoneCallBack = null;
		mTargetRotation = Vector3.zero;
		mLerpSpeed = 0.0f;
	}
	public override void execute()
	{
		Transformable obj = mReceiver as Transformable;
		TransformableComponentLerpRotation component = obj.getComponent(out component);
		// 停止其他旋转组件
		obj.breakComponent<IComponentModifyRotation>(component.GetType());
		component.setLerpingCallback(mLerpingCallBack);
		component.setLerpDoneCallback(mLerpDoneCallBack);
		component.setActive(true);
		component.setTargetRotation(mTargetRotation);
		component.setLerpSpeed(mLerpSpeed);
		component.play();
		if (component.getState() == PLAY_STATE.PS_PLAY)
		{
			// 需要启用组件更新时,则开启组件拥有者的更新,后续也不会再关闭
			obj.setEnable(true);
		}
	}
	public override string showDebugInfo()
	{
		return base.showDebugInfo() + ": mLerpSpeed:" + mLerpSpeed + ", mTargetRotation:" + mTargetRotation;
	}
}