using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public sealed class PtrWrapperMarshaller<TClass> : ICustomMarshaler {

    private static readonly ConstructorInfo CtorInfo = typeof(TClass).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
      .First(ctor => {
        var ps = ctor.GetParameters();
        if (ps.Length > 1) return false;

        var p = ps[0];
        var pt = p.ParameterType;
        return pt.IsPointer || pt == typeof(IntPtr) || pt == typeof(UIntPtr);
      });

    private static readonly FieldInfo FieldInfo = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
      .First(fi => {
        var ft = fi.FieldType;
        return ft.IsPointer || ft == typeof(IntPtr) || ft == typeof(UIntPtr);
      });

    public unsafe object MarshalNativeToManaged(IntPtr p)
      => CtorInfo.Invoke(new object[] {p});

    public IntPtr MarshalManagedToNative(object o)
      => (IntPtr) FieldInfo.GetValue(o);

    public void CleanUpNativeData(IntPtr p) {
      // ...
    }

    public void CleanUpManagedData(object o) {
      // ...
    }

    public unsafe int GetNativeDataSize()
      => sizeof(void*);

  }

  public abstract class RefWrapperMarshaller<TClass> : ICustomMarshaler {

    protected static readonly ConstructorInfo CtorInfo = typeof(TClass).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
      .First(ctor => {
        var ps = ctor.GetParameters();
        if (ps.Length != 2) return false;

        var p0t = ps[0].ParameterType;
        var p1t = ps[1].ParameterType;
        return (p0t.IsPointer || p0t == typeof(IntPtr) || p0t == typeof(UIntPtr))
          && p1t == typeof(bool);
      });

    protected static readonly FieldInfo FieldInfo = typeof(TClass).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
      .First(fi => {
        var ft = fi.FieldType;
        return ft.IsPointer || ft == typeof(IntPtr) || ft == typeof(UIntPtr);
      });

    public unsafe int GetNativeDataSize()
      => sizeof(void*);

    public abstract void CleanUpManagedData(object ManagedObj);

    public abstract void CleanUpNativeData(IntPtr pNativeData);

    public abstract IntPtr MarshalManagedToNative(object ManagedObj);

    public abstract object MarshalNativeToManaged(IntPtr pNativeData);

  }

  public sealed class RefOnlyPtrWrapperMarshaller<TClass> : RefWrapperMarshaller<TClass> {

    public override object MarshalNativeToManaged(IntPtr p)
      => CtorInfo.Invoke(new object[] {p, true});

    public override IntPtr MarshalManagedToNative(object o)
      => (IntPtr) FieldInfo.GetValue(o);

    public override void CleanUpNativeData(IntPtr p) {
      // ...
    }

    public override void CleanUpManagedData(object o) {
      // ...
    }

  }

  public sealed class OwnedPtrWrapperMarshaller<TClass> : RefWrapperMarshaller<TClass> {

    public override object MarshalNativeToManaged(IntPtr p)
      => CtorInfo.Invoke(new object[] {p, false});

    public override IntPtr MarshalManagedToNative(object o)
      => (IntPtr) FieldInfo.GetValue(o);

    public override void CleanUpNativeData(IntPtr p) {
      // ...
    }

    public override void CleanUpManagedData(object o) {
      // ...
    }

  }

}