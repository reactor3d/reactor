using System;

namespace Reactor.Math
{
	public enum CurveTangent
	{
		Flat	, 	// A Flat tangent always has a value equal to zero. 
		Linear,	// A Linear tangent at a CurveKey is equal to the difference between its Value and the Value of the preceding or succeeding CurveKey.
		Smooth,	// A Smooth tangent smooths the inflection between a TangentIn and TangentOut by taking into account the values of both neighbors of the CurveKey.
	}
}
