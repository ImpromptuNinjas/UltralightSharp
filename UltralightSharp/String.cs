using System;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct String {

    public static unsafe String* Create(string str) {
#if NETFRAMEWORK || NETSTANDARD2_0
      fixed (char* p = str)
        return Ultralight.CreateStringUTF16(p, (UIntPtr) (uint) str.Length);
#else
        return Create((ReadOnlySpan<char>) str);
#endif
    }

    public static unsafe String* Create(ReadOnlySpan<char> utf16) {
      fixed (char* p = utf16)
        return Ultralight.CreateStringUTF16(p, (UIntPtr) (uint) utf16.Length);
    }

    public static unsafe String* Create(ReadOnlySpan<byte> utf8) {
      fixed (byte* p = utf8)
        return Ultralight.CreateStringUTF8((sbyte*) p, (UIntPtr) (uint) utf8.Length);
    }

  }

  [PublicAPI]
  public static class StringExtensions {

    public static unsafe void Destroy(in this String _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      Ultralight.DestroyString((String*) p);
    }

    public static unsafe string? Read(in this String _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var p);
      if (p == null)
        return null;

      var chLen = Ultralight.StringGetLength((String*) p);
      var strLen = checked((int) chLen.ToUInt64());

      if (strLen < 0)
        throw new NotImplementedException($"String length is {strLen}");

      if (strLen == 0)
        return "";

      var pCh = Ultralight.StringGetData((String*) p);
      return new string(pCh, 0, strLen);
    }

  }

}