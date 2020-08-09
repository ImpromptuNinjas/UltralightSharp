using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp.Enums {

  [PublicAPI]
  [NativeTypeName("ULCommandType")]
  public enum CommandType : byte {

    ClearRenderBuffer = 0,

    DrawGeometry,

  }

}