using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemGetFileMimeTypeCallback([NativeTypeName("ULString")] String* path, [NativeTypeName("ULString")] String* result);

}