using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("void *")]
  public unsafe delegate void* SurfaceDefinitionCreateCallback([NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

}