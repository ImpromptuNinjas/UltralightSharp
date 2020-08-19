using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public readonly ref struct JsString {

    public static unsafe JsString* Create(char* chars, UIntPtr numChars)
      => JavaScriptCore.StringCreateWithCharacters(chars, numChars);

    public static unsafe JsString* Create(ReadOnlySpan<char> @string) {
      fixed (char* chars = @string)
        return Create(chars, (UIntPtr) @string.Length);
    }

    public static unsafe JsString* CreateFromUtf8(sbyte* chars)
      => JavaScriptCore.StringCreateWithUtf8CString(chars);

  }

  [PublicAPI]
  public static unsafe class JsStringExtensions {

    public static JsString* Retain(in this JsString _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringRetain((JsString*) @string);
    }

    public static void Release(in this JsString _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      JavaScriptCore.StringRelease((JsString*) @string);
    }

    public static UIntPtr GetLength(in this JsString _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringGetLength((JsString*) @string);
    }

    public static char* GetCharactersPtr(in this JsString _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringGetCharactersPtr((JsString*) @string);
    }

    public static UIntPtr GetMaximumUtf8CStringSize(in this JsString _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringGetMaximumUtf8CStringSize((JsString*) @string);
    }

    public static UIntPtr GetUtf8CString(in this JsString _, sbyte* buffer, UIntPtr bufferSize) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringGetUtf8CString((JsString*) @string, buffer, bufferSize);
    }

    public static UIntPtr GetUtf8CString(in this JsString _, Span<sbyte> buffer) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      fixed (sbyte* pBuffer = buffer)
        return JavaScriptCore.StringGetUtf8CString((JsString*) @string, pBuffer, (UIntPtr) buffer.Length);
    }

    public static OneByteBoolean IsEqual(in this JsString _, JsString* b) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringIsEqual((JsString*) @string, b);
    }

    public static OneByteBoolean IsEqualToUtf8CString(in this JsString _, sbyte* b) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var @string);
      return JavaScriptCore.StringIsEqualToUtf8CString((JsString*) @string, b);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsString : IEquatable<JsString>, IReadOnlyList<char>, IDisposable {

      private static ConditionalWeakTable<string, JsString> _mappedJsStrings
        = new ConditionalWeakTable<string, JsString>();

      private static ConditionalWeakTable<JsString, string> _mappedNetStrings
        = new ConditionalWeakTable<JsString, string>();

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsString* Unsafe => _;

      internal readonly UltralightSharp.JsString* _;

      public JsString(UltralightSharp.JsString* p) {
        if (p == null) throw new ArgumentNullException(nameof(p));

        _ = p->Retain();
      }

      public JsString(ReadOnlySpan<char> chars)
        : this(UltralightSharp.JsString.Create(chars)) {
      }

      public void Dispose() {
        if (_ != null)
          _->Release();
      }

      public UIntPtr GetLength()
        => _->GetLength();

      public UIntPtr GetMaximumUtf8CStringSize()
        => _->GetMaximumUtf8CStringSize();

      public UIntPtr GetUtf8CString(Span<sbyte> buffer)
        => _->GetUtf8CString(buffer);

      public bool Equals(JsString? b)
        => b != null && (_ == b._ || _->IsEqual(b._));

      public static implicit operator ReadOnlySpan<char>(JsString str)
        => new ReadOnlySpan<char>(str._->GetCharactersPtr(), (int) str.GetLength());

      public static implicit operator string(JsString str)
        => _mappedNetStrings.GetValue(str, k => {
#if NETFRAMEWORK || NETSTANDARD2_0
          var chars = (ReadOnlySpan<char>) k;
          fixed (char* pChars = chars)
            return new string(pChars, 0, chars.Length);
#else
          return new string((ReadOnlySpan<char>) k);
#endif
        });

      public static implicit operator JsString(string s)
        => _mappedJsStrings.GetValue(s, k => {
#if NETFRAMEWORK || NETSTANDARD2_0
          fixed (char* pChars = k)
            return new string(pChars, 0, k.Length);
#else
          return new JsString((ReadOnlySpan<char>) k);
#endif
        });

      public static implicit operator JsString(ReadOnlySpan<char> chars)
        => new JsString(chars);

      public ReadOnlySpan<char>.Enumerator GetEnumerator() {
        var span = (ReadOnlySpan<char>) this;
        return span.GetEnumerator();
      }

      IEnumerator<char> IEnumerable<char>.GetEnumerator() {
        var len = GetLength().ToUInt64();
        var chars = GetCharactersPtrUnsafe();
        for (var i = 0uL; i < len; ++i)
          yield return GetCharFromPointerUnsafe(chars, i);
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private char GetCharFromPointerUnsafe(IntPtr ptr, ulong index)
        => ((char*) ptr)[index];

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      private IntPtr GetCharactersPtrUnsafe()
        => (IntPtr) _->GetCharactersPtr();

      IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable<char>) this).GetEnumerator();

      int IReadOnlyCollection<char>.Count => (int) GetLength();

      char IReadOnlyList<char>.this[int index]
        => index > 0 && index < GetLength().ToUInt32()
          ? _->GetCharactersPtr()[index]
          : throw new ArgumentOutOfRangeException(nameof(index));

    }

  }

}