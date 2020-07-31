using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("void *")]
  public unsafe delegate void* SurfaceDefinitionLockPixelsCallback([NativeTypeName("void *")] void* userData);

}