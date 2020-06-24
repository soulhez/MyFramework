﻿using UnityEngine;
using System;
using System.Collections;

public class GameSceneComponentVolume : ComponentKeyFrameNormal
{
	protected float mStartVolume;
	protected float mTargetVolume;
	public void setStartVolume(float volume) { mStartVolume = volume; }
	public void setTargetVolume(float volume) { mTargetVolume = volume; }
	//------------------------------------------------------------------------------------------------------------
	protected override void applyTrembling(float offset)
	{
		GameScene gameScene = mComponentOwner as GameScene;
		float newVolume = lerpSimple(mStartVolume, mTargetVolume, offset);
		gameScene.getComponent<GameSceneComponentAudio>().setVolume(newVolume);
	}
}