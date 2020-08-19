using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void GpuDriverUpdateGeometryCallback([NativeTypeName("unsigned int")] uint geometryId, VertexBuffer vertices, IndexBuffer indices);

  namespace Safe {

    [PublicAPI]
    public delegate void GpuDriverUpdateGeometryCallback(uint geometryId, VertexBuffer safeVertices, IndexBuffer indices);

  }

}