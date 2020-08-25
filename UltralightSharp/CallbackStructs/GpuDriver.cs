using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [StructLayout(LayoutKind.Sequential)]
  public struct GpuDriver {

    [NativeTypeName("ULGPUDriverBeginSynchronize")]
    public FnPtr<GpuDriverBeginSynchronizeCallback> BeginSynchronize;

    [NativeTypeName("ULGPUDriverEndSynchronize")]
    public FnPtr<GpuDriverEndSynchronizeCallback> EndSynchronize;

    [NativeTypeName("ULGPUDriverNextTextureId")]
    public FnPtr<GpuDriverNextTextureIdCallback> NextTextureId;

    [NativeTypeName("ULGPUDriverCreateTexture")]
    public FnPtr<GpuDriverCreateTextureCallback> CreateTexture;

    [NativeTypeName("ULGPUDriverUpdateTexture")]
    public FnPtr<GpuDriverUpdateTextureCallback> UpdateTexture;

    [NativeTypeName("ULGPUDriverDestroyTexture")]
    public FnPtr<GpuDriverDestroyTextureCallback> DestroyTexture;

    [NativeTypeName("ULGPUDriverNextRenderBufferId")]
    public FnPtr<GpuDriverNextRenderBufferIdCallback> NextRenderBufferId;

    [NativeTypeName("ULGPUDriverCreateRenderBuffer")]
    public FnPtr<GpuDriverCreateRenderBufferCallback> CreateRenderBuffer;

    [NativeTypeName("ULGPUDriverDestroyRenderBuffer")]
    public FnPtr<GpuDriverDestroyRenderBufferCallback> DestroyRenderBuffer;

    [NativeTypeName("ULGPUDriverNextGeometryId")]
    public FnPtr<GpuDriverNextGeometryIdCallback> NextGeometryId;

    [NativeTypeName("ULGPUDriverCreateGeometry")]
    public FnPtr<GpuDriverCreateGeometryCallback> CreateGeometry;

    [NativeTypeName("ULGPUDriverUpdateGeometry")]
    public FnPtr<GpuDriverUpdateGeometryCallback> UpdateGeometry;

    [NativeTypeName("ULGPUDriverDestroyGeometry")]
    public FnPtr<GpuDriverDestroyGeometryCallback> DestroyGeometry;

    [NativeTypeName("ULGPUDriverUpdateCommandList")]
    public FnPtr<GpuDriverUpdateCommandListCallback> UpdateCommandList;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    [StructLayout(LayoutKind.Sequential)]
    public struct GpuDriver {

      private UltralightSharp.GpuDriver _;

      public static implicit operator UltralightSharp.GpuDriver(in GpuDriver x)
        => x._;

      public GpuDriverBeginSynchronizeCallback BeginSynchronize {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverBeginSynchronizeCallback cb
            = () => value();
          _.BeginSynchronize = cb;
        }
      }

      public GpuDriverEndSynchronizeCallback EndSynchronize {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverEndSynchronizeCallback cb
            = () => value();
          _.EndSynchronize = cb;
        }
      }

      public GpuDriverNextTextureIdCallback NextTextureId {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverNextTextureIdCallback cb
            = () => value();
          _.NextTextureId = cb;
        }
      }

      public unsafe GpuDriverCreateTextureCallback CreateTexture {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverCreateTextureCallback cb
            = (id, bitmap) => value(id, new Bitmap(bitmap));
          _.CreateTexture = cb;
        }
      }

      public unsafe GpuDriverUpdateTextureCallback UpdateTexture {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverUpdateTextureCallback cb
            = (id, bitmap) => value(id, new Bitmap(bitmap));
          _.UpdateTexture = cb;
        }
      }

      public GpuDriverDestroyTextureCallback DestroyTexture {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverDestroyTextureCallback cb
            = id => value(id);
          _.DestroyTexture = cb;
        }
      }

      public GpuDriverNextRenderBufferIdCallback NextRenderBufferId {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverNextRenderBufferIdCallback cb
            = () => value();
          _.NextRenderBufferId = cb;
        }
      }

      public GpuDriverCreateRenderBufferCallback CreateRenderBuffer {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverCreateRenderBufferCallback cb
            = (id, buffer) => value(id, buffer.AsSafe());
          _.CreateRenderBuffer = cb;
        }
      }

      public GpuDriverDestroyRenderBufferCallback DestroyRenderBuffer {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverDestroyRenderBufferCallback cb
            = id => value(id);
          _.DestroyRenderBuffer = cb;
        }
      }

      public GpuDriverNextGeometryIdCallback NextGeometryId {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverNextGeometryIdCallback cb
            = () => value();
          _.NextGeometryId = cb;
        }
      }

      public GpuDriverCreateGeometryCallback CreateGeometry {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverCreateGeometryCallback cb
            = (id, vertices, indices) => value(id, vertices.AsSafe(), indices.AsSafe());
          _.CreateGeometry = cb;
        }
      }

      public GpuDriverUpdateGeometryCallback UpdateGeometry {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverUpdateGeometryCallback cb
            = (id, vertices, indices) => value(id, vertices.AsSafe(), indices.AsSafe());
          _.UpdateGeometry = cb;
        }
      }

      public GpuDriverDestroyGeometryCallback DestroyGeometry {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverDestroyGeometryCallback cb
            = id => value(id);
          _.DestroyGeometry = cb;
        }
      }

      public GpuDriverUpdateCommandListCallback UpdateCommandList {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set {
          UltralightSharp.GpuDriverUpdateCommandListCallback cb
            = list => value(list);
          _.UpdateCommandList = cb;
        }
      }

    }

  }

}