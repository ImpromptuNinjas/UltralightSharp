using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULFileHandle")]
  public unsafe delegate UIntPtr FileSystemOpenFileCallback([NativeTypeName("ULString")] String* path, bool openForWriting);

}