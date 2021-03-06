﻿using System;
using System.Collections;
using System.Collections.Generic;

public class BOOL : OBJECT
{
	protected const int TYPE_SIZE = sizeof(bool);
	public bool mValue;
	public BOOL()
	{
		mType = typeof(bool);
		mSize = TYPE_SIZE;
	}
	public BOOL(bool value)
	{
		mValue = value;
		mType = typeof(bool);
		mSize = TYPE_SIZE;
	}
	public override void zero() { mValue = false; }
	public void set(bool value) { mValue = value; }
	public override bool readFromBuffer(byte[] buffer, ref int index)
	{
		bool success;
		mValue = readBool(buffer, ref index, out success);
		return success;
	}
	public override bool writeToBuffer(byte[] buffer, ref int index)
	{
		return writeBool(buffer, ref index, mValue);
	}
}