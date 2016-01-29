using System;

/// <summary>
/// Poolable.
/// </summary>
public interface Poolable<T>
{
	/// <summary>
	/// Determines whether this instance is ready.
	/// </summary>
	/// <returns><c>true</c> if this instance is ready; otherwise, <c>false</c>.</returns>
	bool IsReady();

	/// <summary>
	/// Copy the specified t.
	/// </summary>
	/// <param name="t">T.</param>
	void Copy(T t);

	/// <summary>
	/// Pick this instance.
	/// </summary>
	void Pick();
}

