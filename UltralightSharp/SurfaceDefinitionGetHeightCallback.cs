using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public unsafe delegate uint SurfaceDefinitionGetHeightCallback([NativeTypeName("void *")] void* userData);

}