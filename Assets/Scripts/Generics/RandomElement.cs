using UnityEngine;

// Choose a random element in a array
// Avoid infinite loop and to have the same value twice in a row
public class RandomElement
{
	private const int MAX_RANDOM_ITERATION = 3;                     // Max number to iterate in a list to avoid a infinite loop

	public int GetCurrentIndex => _currentIndex;

	private int _currentIndex = 0;

	public RandomElement(int currentIndex = 0) => _currentIndex = currentIndex;

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

		_currentIndex = i;

		return elements[_currentIndex];
	}

	// Get the index
	public int Choose(int sizeElements)
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

		_currentIndex = i;
		return _currentIndex;
	}
}
