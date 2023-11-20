using System;

namespace Almond
{
	public static class EnumExtention
	{
		public static T Next<T>( this T _enum ) where T : struct, Enum
		{
			T[] values = (T[])Enum.GetValues( _enum.GetType() );
			var index = Array.IndexOf<T>( values, _enum ) + 1;
			return index == values.Length ? values[ 0 ] : values[ index ];
		}
		public static T Prev<T>( this T _enum ) where T : struct, Enum
		{
			T[] values = (T[])Enum.GetValues( _enum.GetType() );
			var index = Array.IndexOf<T>( values, _enum ) - 1;
			return index >= 0 ? values[ index ] : values[ ^1 ];
		}
		public static int Lenght<T>(this T _enum) where T : Type
		{
			if( _enum.IsEnum == false ) return 0;
			return _enum.GetEnumValues()?.Length ?? 0;
		}
	}
}