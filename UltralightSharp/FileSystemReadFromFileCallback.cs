using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("long long")]
  public unsafe delegate long FileSystemReadFromFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle, [NativeTypeName("char *")] sbyte* data, [NativeTypeName("long long")] long length);

}