﻿using System;

namespace Sukt.Module.Core.Helpers
{
    public static class ArrayHelper
    {
        public static T[] Empty<T>() =>
#if NET45

            EmptyArray<T>.Value
#else
            Array.Empty<T>()
#endif
        ;

#if NET45
        private static class EmptyArray<T>
        {
            public static readonly T[] Value = new T[0];
        }
#endif
    }
}