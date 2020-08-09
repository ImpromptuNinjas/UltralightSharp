using ImpromptuNinjas.UltralightSharp.Safe;

partial class Program {

  private class GeometryEntry {

    public uint VertexArray { get; set; }

    public uint Vertices { get; set; }

    public uint Indices { get; set; }

  }

  private class RenderBufferEntry {

    public uint FrameBuffer { get; set; }

    public TextureEntry TextureEntry { get; set; }

  }

  private class TextureEntry {

    public uint Texure { get; set; }

    public uint Width { get; set; }

    public uint Height { get; set; }

  }

}