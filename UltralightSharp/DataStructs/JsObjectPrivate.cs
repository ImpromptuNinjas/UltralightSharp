using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using InlineIL;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public ref struct JsObjectPrivate {

    public GCHandle<LinkedList<IntPtr>> Classes;

  }

  [PublicAPI]
  public static class JsObjectPrivateExtensions {

    public static unsafe JsContext* CreateContext(in this JsObjectPrivate _) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ptr);
      var p = (JsObjectPrivate*) ptr;
      if (!p->Classes.IsAllocated)
        return null;

      var classes = p->Classes.Target!;

      lock (classes) {
        if (classes.Count == 0)
          return null;

        var rJsClass = classes.Last!.Value;
        var pJsClass = (JsClass*) rJsClass;
        //var jsClass = new JsClass(pJsClass);
        return pJsClass->CreateGlobalContext();
      }
    }

    public static unsafe void PushClass(in this JsObjectPrivate _, JsClass* @class) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ptr);
      var p = (JsObjectPrivate*) ptr;
      LinkedList<IntPtr> classes;
      if (!p->Classes.IsAllocated) {
        classes = new LinkedList<IntPtr>();
        classes.AddLast((IntPtr) @class);
        p->Classes = new GCHandle<LinkedList<IntPtr>>(
          classes
        );
        return;
      }

      classes = p->Classes.Target!;
      lock (classes)
        classes.AddLast((IntPtr) @class);
    }

    public static unsafe bool RemoveClass(in this JsObjectPrivate _, JsClass* @class) {
      IL.Emit.Ldarg_0();
      IL.Pop(out var ptr);
      var p = (JsObjectPrivate*) ptr;
      if (!p->Classes.IsAllocated)
        return false;

      var classes = p->Classes.Target!;
      lock (classes) {
        if (classes.Count == 0)
          return false;

        var node = classes.FindLast((IntPtr) @class);
        if (node == null)
          return false;

        classes.Remove(node);
        return true;
      }
    }

  }

  namespace Safe {

    [PublicAPI]
    [StructLayout(LayoutKind.Sequential)]
    public sealed unsafe class JsObjectPrivate {

      public readonly UltralightSharp.JsObjectPrivate* _;

      public static implicit operator UltralightSharp.JsObjectPrivate*(in JsObjectPrivate x)
        => x._;

      public JsContext? CreateContext() {
        var ctx = _->CreateContext();
        return ctx == null ? null : new JsGlobalContext(ctx);
      }

      public void PushClass(JsClass @class)
        => _->PushClass(@class._);

      public bool RemoveClass(JsClass @class)
        => _->RemoveClass(@class._);

      public IReadOnlyList<JsClass?> Classes {
        get {
          if (!_->Classes.IsAllocated)
#if NETFRAMEWORK
            return new JsClass?[0];
#else
            return Array.Empty<JsClass?>();
#endif

          var classes = _->Classes.Target!;
          lock (classes) {
            var count = classes.Count;
            if (count == 0)
#if NETFRAMEWORK
              return new JsClass?[0];
#else
              return Array.Empty<JsClass?>();
#endif

            var safeClasses = new JsClass[count];
            var node = classes.First!;
            for (var i = 0; i < count; ++i) {
              var @class = (UltralightSharp.JsClass*) node.Value;
              if (@class != null)
                safeClasses[i] = new JsClass(@class);
              node = node.Next!;
            }

            return safeClasses;
          }
        }
      }

    }

  }

}