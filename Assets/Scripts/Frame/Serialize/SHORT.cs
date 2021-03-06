﻿using System;
using System.Collections;
using System.Collections.Generic;

public class SHORT : OBJECT
{
	protected const int TYPE_SIZE = sizeof(short);
	public short mValue;
	public SHORT()
	{
		mType = typeof(short);
		mSize = TYPE_SIZE;
	}
	public SHORT(short value)
	{
		mValue = value;
		mType = typeof(short);
		mSize = TYPE_SIZE;
	}
	public override void zero() { mValue = 0; }
	public void set(short value) { mValue = value; }
	public override bool readFromBuffer(byte[] buffer, ref int index)
	{
		bool success;
		mValue = readShort(buffer, ref index, out success);
		return success;
	}
	public override bool writeToBuffer(byte[] buffer, ref int index)
	{
		return writeShort(buffer, ref index, mValue);
	}
}