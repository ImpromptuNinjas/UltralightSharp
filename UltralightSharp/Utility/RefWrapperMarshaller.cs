using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public abstract class RefWrapperMarshaller<TClass> : ICustomMarshaler {

    internal RefWrapperMarshaller() {
    }

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

}