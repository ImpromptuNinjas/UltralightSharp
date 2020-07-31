using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public unsafe delegate uint SurfaceDefinitionGetRowBytesCallback([NativeTypeName("void *")] void* userData);

}