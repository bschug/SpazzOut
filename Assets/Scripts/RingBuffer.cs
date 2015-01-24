using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RingBuffer<T> 
{
    int capacity;
    List<T> items;
    int start = 0;
    int count = 0;

    public RingBuffer (int capacity)
    {
        this.capacity = capacity;
        this.items = new List<T> (capacity);
        for (int i = 0; i < capacity; i++) {
            this.items.Add (default(T));
        }
    }

    public int Count { get {
		return count;
    }}

    public void Add (T item)
    {
		items[TranslateIndex(count)] = item;

		if (count < capacity) {
			count++;
		} 
		else {
			start++;
		}
	}

	public T this[int index]
	{
		get { return items[TranslateIndex(index)]; }
		set { items[TranslateIndex (index)] = value; }
	}

	int TranslateIndex(int index) {
		return (start + index) % capacity;
	}

	public static void Test ()
	{
		var buffer = new RingBuffer<int> (3);
		Assert (buffer.Count == 0);

		buffer.Add (3);
		Assert (buffer.Count == 1);
		Assert (buffer[0] == 3);

		buffer.Add (2);
		Assert (buffer.Count == 2);
		Assert (buffer[0] == 3);
		Assert (buffer[1] == 2);

		buffer.Add (6);
		Assert (buffer.Count == 3);
		Assert (buffer[0] == 3);
		Assert (buffer[1] == 2);
		Assert (buffer[2] == 6);

		buffer.Add (9);
		Assert (buffer.Count == 3);
		Assert (buffer[0] == 2);
		Assert (buffer[1] == 6);
		Assert (buffer[2] == 9);
	}

	static void Assert (bool expr, string msg = "Test Failed")
	{
		if (!expr) {
			UnityEngine.Debug.LogError (msg);
		}
	}
}
