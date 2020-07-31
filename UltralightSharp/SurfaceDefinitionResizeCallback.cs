using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void SurfaceDefinitionResizeCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

}