using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct SurfaceDefinition {

    [NativeTypeName("ULSurfaceDefinitionCreateCallback")]
    public FnPtr<SurfaceDefinitionCreateCallback> Create;

    [NativeTypeName("ULSurfaceDefinitionDestroyCallback")]
    public FnPtr<SurfaceDefinitionDestroyCallback> Destroy;

    [NativeTypeName("ULSurfaceDefinitionGetWidthCallback")]
    public FnPtr<SurfaceDefinitionGetWidthCallback> GetWidth;

    [NativeTypeName("ULSurfaceDefinitionGetHeightCallback")]
    public FnPtr<SurfaceDefinitionGetHeightCallback> GetHeight;

    [NativeTypeName("ULSurfaceDefinitionGetRowBytesCallback")]
    public FnPtr<SurfaceDefinitionGetRowBytesCallback> GetRowBytes;

    [NativeTypeName("ULSurfaceDefinitionGetSizeCallback")]
    public FnPtr<SurfaceDefinitionGetSizeCallback> GetSize;

    [NativeTypeName("ULSurfaceDefinitionLockPixelsCallback")]
    public FnPtr<SurfaceDefinitionLockPixelsCallback> LockPixels;

    [NativeTypeName("ULSurfaceDefinitionUnlockPixelsCallback")]
    public FnPtr<SurfaceDefinitionUnlockPixelsCallback> UnlockPixels;

    [NativeTypeName("ULSurfaceDefinitionResizeCallback")]
    public FnPtr<SurfaceDefinitionResizeCallback> Resize;

  }

}