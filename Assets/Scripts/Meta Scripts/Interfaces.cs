/// <summary>
/// Any actions that are to occur in a simulation panel uses this interface.
/// </summary>
public interface CommonInterface
{
	/// <summary>
	/// Used when a panel is re-enabled but requires some sort of reset/setup to function correctly.
	/// </summary>
	void Reset();
}
