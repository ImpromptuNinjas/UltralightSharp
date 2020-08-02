using System;
using System.Diagnostics.CodeAnalysis;
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

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    public struct SurfaceDefinition {

      private UltralightSharp.SurfaceDefinition _;

      public static implicit operator UltralightSharp.SurfaceDefinition(in SurfaceDefinition x)
        => x._;

      public unsafe SurfaceDefinitionCreateCallback Create {
        set {
          UltralightSharp.SurfaceDefinitionCreateCallback cb
            = (width, height) => (void*) value(width, height);
          _.Create = cb;
        }
      }

      public unsafe SurfaceDefinitionDestroyCallback Destroy {
        set {
          UltralightSharp.SurfaceDefinitionDestroyCallback cb
            = ud => value((IntPtr) ud);
          _.Destroy = cb;
        }
      }

      public unsafe SurfaceDefinitionGetWidthCallback GetWidth {
        set {
          UltralightSharp.SurfaceDefinitionGetWidthCallback cb
            = ud => value((IntPtr) ud);
          _.GetWidth = cb;
        }
      }

      public unsafe SurfaceDefinitionGetHeightCallback GetHeight {
        set {
          UltralightSharp.SurfaceDefinitionGetHeightCallback cb
            = ud => value((IntPtr) ud);
          _.GetHeight = cb;
        }
      }

      public unsafe SurfaceDefinitionGetRowBytesCallback GetRowBytes {
        set {
          UltralightSharp.SurfaceDefinitionGetRowBytesCallback cb
            = ud => value((IntPtr) ud);
          _.GetRowBytes = cb;
        }
      }

      public unsafe SurfaceDefinitionGetSizeCallback GetSize {
        set {
          UltralightSharp.SurfaceDefinitionGetSizeCallback cb
            = ud => value((IntPtr) ud);
          _.GetSize = cb;
        }
      }

      public unsafe SurfaceDefinitionLockPixelsCallback LockPixels {
        set {
          UltralightSharp.SurfaceDefinitionLockPixelsCallback cb
            = ud => (void*) value((IntPtr) ud);
          _.LockPixels = cb;
        }
      }

      public unsafe SurfaceDefinitionUnlockPixelsCallback UnlockPixels {
        set {
          UltralightSharp.SurfaceDefinitionUnlockPixelsCallback cb
            = ud => value((IntPtr) ud);
          _.UnlockPixels = cb;
        }
      }

      public unsafe SurfaceDefinitionResizeCallback Resize {
        set {
          UltralightSharp.SurfaceDefinitionResizeCallback cb
            = (ud, width, height) => value((IntPtr) ud, width, height);
          _.Resize = cb;
        }
      }

    }

  }

}