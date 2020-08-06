using System;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public sealed class RefOnlyPtrWrapperMarshaller<TClass> : RefWrapperMarshaller<TClass> {

    public override object MarshalNativeToManaged(IntPtr p)
      => CtorInfo.Invoke(new object[] {p, true});

    public override IntPtr MarshalManagedToNative(object o)
      => (IntPtr) FieldInfo.GetValue(o)!;

    public override void CleanUpNativeData(IntPtr p) {
      // ...
    }

    public override void CleanUpManagedData(object o) {
      // ...
    }

  }

}