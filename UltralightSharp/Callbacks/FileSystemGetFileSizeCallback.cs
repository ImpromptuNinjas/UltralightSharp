using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemGetFileSizeCallback([NativeTypeName("ULFileHandle")] UIntPtr handle, [NativeTypeName("long long *")] long* result);

}