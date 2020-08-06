using System;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public sealed class OwnedPtrWrapperMarshaller<TClass> : RefWrapperMarshaller<TClass> {

    public override object MarshalNativeToManaged(IntPtr p)
      => CtorInfo.Invoke(new object[] {p, false});

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