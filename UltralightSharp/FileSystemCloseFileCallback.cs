using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FileSystemCloseFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle);

}