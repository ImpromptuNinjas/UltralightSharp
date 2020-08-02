using System;
using System.Runtime.InteropServices;

namespace ImpromptuNinjas.UltralightSharp.Safe {

  public sealed class StringMarshaller : ICustomMarshaler {

    public unsafe object MarshalNativeToManaged(IntPtr p)
      => (p == default ? null : ((String*) p)->Read())!;

    public unsafe IntPtr MarshalManagedToNative(object o) {
      var s = (string) o;
      var p = String.Create(s);
      return (IntPtr) p;
    }

    public unsafe void CleanUpNativeData(IntPtr p)
      => ((String*) p)->Destroy();

    public void CleanUpManagedData(object o) {
      // ...
    }

    public unsafe int GetNativeDataSize()
      => sizeof(void*);

  }

}