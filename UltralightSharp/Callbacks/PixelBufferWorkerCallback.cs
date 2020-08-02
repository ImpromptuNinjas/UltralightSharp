using System;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public unsafe delegate void PixelBufferWorkerCallback(void* pixels);

  namespace Safe {

    [PublicAPI]
    public delegate void PixelBufferWorkerCallback(IntPtr pixels);

  }

}