using Silk.NET.OpenGLES;

partial class Program {

  private static bool _useBlitting = false;

  private static double _scaleX = 1;

  private static double _scaleY = 1;

  private static unsafe void OnRender(double delta) //Method needs to be unsafe due to draw elements.
  {
    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
    if (_gl == null)
      return;

    _ulRenderer.Render();
    _gpuDriverSite.Render(_snView);

    {
      var wndSize = _snView.Size;
      var wndWidth = (uint) wndSize.Width;
      var wndHeight = (uint) wndSize.Height;
      var width = (uint) (_scaleX * wndWidth);
      var height = (uint) (_scaleY * wndHeight);

      _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      _gl.Viewport(0, 0, width, height);
      _gl.ClearColor(0, 0, 0, 0);
      _gl.Clear((uint) ClearBufferMask.ColorBufferBit);

      _gpuDriverSite.TryGetFrameBufferAndTexture(_ulView, out var rb, out var tex, out var texWidth, out var texHeight);

      if (_useBlitting) {
        // /*
        //_gl.Disable(EnableCap.FramebufferSrgb);
        _gl.Disable(EnableCap.ScissorTest);
        _gl.Disable(EnableCap.Blend);
        //Bind the primary framebuffer, quad geometry and shader.
        _gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, rb);
        _gl.ReadBuffer(ReadBufferMode.ColorAttachment0);
        _gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
        //_gl.ClearColor(0, 0, 0, 0);
        //_gl.Clear((uint) ClearBufferMask.ColorBufferBit);

        _gl.BlitFramebuffer(
          0, 0, (int) texWidth, (int) texHeight,
          0, 0, (int) width, (int) height,
          (uint) AttribMask.ColorBufferBit,
          BlitFramebufferFilter.Linear);
      }
      else {
        //_gl.Disable(EnableCap.FramebufferSrgb);
        _gl.Disable(EnableCap.ScissorTest);
        _gl.Disable(EnableCap.Blend);
        //Bind the primary framebuffer, quad geometry and shader.
        _gl.BindVertexArray(_qva);
        _gl.UseProgram(_qpg);
        _gl.ActiveTexture(TextureUnit.Texture0);
        _gl.BindTexture(TextureTarget.Texture2D, tex);
        _gl.Uniform1(_gl.GetUniformLocation(_qpg, "iTex"), 0);
        CheckGl();

        //Draw the quad.
        _gl.DrawElements(PrimitiveType.Triangles,
          (uint) _quadIndices.Length, DrawElementsType.UnsignedInt, null);
      }
    }
  }

}