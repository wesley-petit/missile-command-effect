﻿using UnityEngine;

// Choose a random element in a array
// Avoid infinite loop and to have the same value twice in a row
public class RandomElement
{
	private const ushort MAX_RANDOM_ITERATION = 3;                     // Max number to iterate in a list to avoid a infinite loop

	public uint GetCurrentIndex => _currentIndex;

	private uint _currentIndex = 0;

	public RandomElement(uint currentIndex = 0) => _currentIndex = currentIndex;

	// Get an element int the list
	public T Choose<T>(T[] elements)
	{
		int i;
		int y = 0;

		do
		{
			i = Random.Range(0, elements.Length);
			y++;
		}
		// To obtain a different target that the current and to avoid infinite loop
		while (i == _currentIndex && y < MAX_RANDOM_ITERATION);

		_currentIndex = (uint)i;
		return elements[_currentIndex];
	}

	// Get the index
	public uint Choose(int sizeElements)
	{
		int i;
		int y = 0;

		do
		{
			i = Random.Range(0, sizeElements);
			y++;
		}
		// To obtain a different target that the current and to avoid infinite loop
		while (i == _currentIndex && y < MAX_RANDOM_ITERATION);

		_currentIndex = (uint)i;
		return _currentIndex;
	}
}
