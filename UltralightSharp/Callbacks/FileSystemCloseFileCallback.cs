using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FileSystemCloseFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle);

  namespace Safe {

    [PublicAPI]
    public delegate void FileSystemCloseFileCallback(UIntPtr handle);

  }

}