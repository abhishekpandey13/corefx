// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

namespace System.SpanTests
{
    public static partial class SpanTests
    {
        [Fact]
        public static void ZeroLengthIndexOf_Byte()
        {
            Span<byte> sp = new Span<byte>(Array.Empty<byte>());
            int idx = sp.IndexOf(0);
            Assert.Equal(-1, idx);
        }

        [Fact]
        public static void TestMatch_Byte()
        {
            for (int length = 0; length < 32; length++)
            {
                byte[] a = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    a[i] = (byte)(i + 1);
                }
                Span<byte> span = new Span<byte>(a);
                
                for (int targetIndex = 0; targetIndex < length; targetIndex++)
                {
                    byte target = a[targetIndex];
                    int idx = span.IndexOf(target);
                    Assert.Equal(targetIndex, idx);
                }
            }
        }

        [Fact]
        public static void TestMultipleMatch_Byte()
        {
            for (int length = 2; length < 32; length++)
            {
                byte[] a = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    a[i] = (byte)(i + 1);
                }

                a[length - 1] = 200;
                a[length - 2] = 200;

                Span<byte> span = new Span<byte>(a);
                int idx = span.IndexOf(200);
                Assert.Equal(length - 2, idx);
            }
        }

        [Fact]
        public static void MakeSureNoChecksGoOutOfRange_Byte()
        {
            for (int length = 0; length < 100; length++)
            {
                byte[] a = new byte[length + 2];
                a[0] = 99;
                a[length + 1] = 99;
                Span<byte> span = new Span<byte>(a, 1, length);
                int index = span.IndexOf(99);
                Assert.Equal(-1, index);
            }
        }
    }
}
