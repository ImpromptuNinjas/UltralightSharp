using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  public readonly ref struct JsPropertyNameArray {

  }

  public static unsafe class JsPropertyNameArrayExtensions {

    public static JsPropertyNameArray* Retain(in this JsPropertyNameArray _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var array);
      return JavaScriptCore.PropertyNameArrayRetain((JsPropertyNameArray*) array);
    }

    public static void Release(in this JsPropertyNameArray _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var array);
      JavaScriptCore.PropertyNameArrayRelease((JsPropertyNameArray*) array);
    }

    public static UIntPtr GetCount(in this JsPropertyNameArray _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var array);
      return JavaScriptCore.PropertyNameArrayGetCount((JsPropertyNameArray*) array);
    }

    public static JsString* GetNameAtIndex(in this JsPropertyNameArray _, UIntPtr index) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var array);
      return JavaScriptCore.PropertyNameArrayGetNameAtIndex((JsPropertyNameArray*) array, index);
    }

  }

  namespace Safe {

    [PublicAPI]
    public sealed unsafe class JsPropertyNameArray : IReadOnlyCollection<JsString?> {

      [EditorBrowsable(EditorBrowsableState.Advanced)]
      public UltralightSharp.JsPropertyNameArray* Unsafe => _;

      internal UltralightSharp.JsPropertyNameArray* _;

      public JsPropertyNameArray(UltralightSharp.JsPropertyNameArray* p) {
        if (p == null) throw new ArgumentNullException(nameof(p));

        _ = p->Retain();
      }

      public UIntPtr GetCount()
        => _->GetCount();

      public JsString? GetNameAtIndex(UIntPtr index) {
        var s = _->GetNameAtIndex(index);
        return s == null ? null : new JsString(s);
      }

      public IEnumerator<JsString?> GetEnumerator() {
        var count = GetCount().ToUInt32();
        for (var i = 0u; i < count; ++i)
          yield return GetNameAtIndex((UIntPtr) i);
      }

      IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

      int IReadOnlyCollection<JsString?>.Count => (int) GetCount();

    }

  }

}