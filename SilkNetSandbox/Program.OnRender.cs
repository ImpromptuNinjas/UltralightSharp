using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Silk.NET.OpenGLES;
using ImpromptuNinjas.UltralightSharp.Safe;
using ImpromptuNinjas.UltralightSharp.Enums;
using ShaderType = ImpromptuNinjas.UltralightSharp.Enums.ShaderType;

partial class Program {

  private static int _haveRendered;

  private static unsafe void OnRender(double delta) //Method needs to be unsafe due to draw elements.
  {
    if (_haveRendered++ < 2) {
      //Debugger.Break();
      //Bind the primary framebuffer, quad geometry and shader.
      var wndSize = _wnd.Size;
      var wndWidth = (uint) wndSize.Width;
      var wndHeight = (uint) wndSize.Height;
      _gl.Viewport(0, 0, wndWidth, wndHeight);
      _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      _gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
      _gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
      _gl.ClearColor(0, 0, 0, 0);
      _gl.Clear((uint) ClearBufferMask.ColorBufferBit);
      return;
    }

    _renderer.Render();
    var commands = _commands;
    if (commands.Count > 0) {
      Console.WriteLine($"Running {commands.Count} Commands");
      _commands = new List<Command>();
      _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

      // render view
      _gl.Enable(EnableCap.Blend);
      _gl.Enable(EnableCap.FramebufferSrgb);
      _gl.Disable(EnableCap.ScissorTest);
      _gl.Disable(EnableCap.DepthTest);
      _gl.DepthFunc(DepthFunction.Never);
      _gl.BlendFunc(BlendingFactor.One, BlendingFactor.OneMinusSrcAlpha);

      foreach (var command in commands) {
        switch (command.CommandType) {
          case CommandType.ClearRenderBuffer: {
            //Console.WriteLine("Clearing Render Buffer");
            var index = (int) command.GpuState.RenderBufferId - 1;
            var rb = RenderBufferEntries[index];

            // bind render buffer
            _gl.BindFramebuffer(FramebufferTarget.Framebuffer, rb.FrameBuffer);

            // disable scissor state
            _gl.Disable(GLEnum.ScissorTest);

            // set clear color
            _gl.ClearColor(0, 0, 0, 0);

            // clear
            _gl.Clear((uint) GLEnum.ColorBufferBit);
            break;
          }
          case CommandType.DrawGeometry: {
            //Console.WriteLine("Drawing Geometry");
            ref readonly var state = ref command.GpuState;
            var index = (int) command.GpuState.RenderBufferId - 1;
            var rb = RenderBufferEntries[index];

            // bind render buffer
            _gl.BindFramebuffer(FramebufferTarget.Framebuffer, rb.FrameBuffer);

            // set viewport
            _gl.Viewport(0, 0, state.ViewportWidth, state.ViewportHeight);
            var entry = GeometryEntries[(int) command.GeometryId - 1];

            // select program
            var shaderType = state.ShaderType;

            uint pg;
            switch (shaderType) {
              default: throw new ArgumentOutOfRangeException(nameof(ShaderType), shaderType, "Unexpected value.");
              case ShaderType.Fill: {
                pg = _fillShader;
                break;
              }
              case ShaderType.FillPath: {
                pg = _fillPathShader;
                break;
              }
            }

            _gl.UseProgram(pg);

            CheckGl();

            // update uniforms
            _gl.Uniform4(
              _gl.GetUniformLocation(pg, "State"),
              (float) _wnd.Time,
              state.ViewportWidth,
              state.ViewportHeight,
              1
            );
            var txf =
              Ultralight.ApplyProjection(
                state.Transform,
                state.ViewportWidth,
                state.ViewportHeight,
                false
              );
            _gl.UniformMatrix4(
              _gl.GetUniformLocation(pg, "Transform"), 1, false,
              (float*) Unsafe.AsPointer(ref Unsafe.AsRef(txf))
            );
            _gl.Uniform4(
              _gl.GetUniformLocation(pg, "Scalar4"), 2,
              state.UniformScalars
            );
            _gl.Uniform4(
              _gl.GetUniformLocation(pg, "Vector"), 8,
              state.UniformVectors
            );
            _gl.Uniform1(
              _gl.GetUniformLocation(pg, "fClipSize"),
              (float) state.ClipSize
            );
            _gl.UniformMatrix4(
              _gl.GetUniformLocation(pg, "Clip"), 8, false,
              (float*) Unsafe.AsPointer(ref Unsafe.AsRef(state.Clip))
            );

            CheckGl();

            // bind vertex array
            _gl.BindVertexArray(entry.VertexArray);

            CheckGl();

            // bind textures
            var texIndex1 = (int) state.Texture1Id - 1;
            if (texIndex1 > 0) {
              var texEntry1 = TextureEntries[texIndex1];
              _gl.ActiveTexture(TextureUnit.Texture0);
              _gl.BindTexture(GLEnum.Texture2D, texEntry1.Texure);
              CheckGl();
            }
            else {
              //Console.WriteLine($"Texture1 Invalid: {texIndex1 + 1}");
              _gl.ActiveTexture(TextureUnit.Texture0);
              _gl.BindTexture(GLEnum.Texture2D, 0);
            }

            var texIndex2 = (int) state.Texture2Id - 1;
            if (texIndex2 > 0) {
              var texEntry2 = TextureEntries[texIndex2];
              _gl.ActiveTexture(TextureUnit.Texture1);
              _gl.BindTexture(GLEnum.Texture2D, texEntry2.Texure);
              CheckGl();
            }
            else {
              //Console.WriteLine($"Texture2 Invalid: {texIndex2 + 1}");
              _gl.ActiveTexture(TextureUnit.Texture1);
              _gl.BindTexture(GLEnum.Texture2D, 0);
            }

            var texIndex3 = (int) state.Texture3Id - 1;
            if (texIndex3 > 0) {
              var texEntry3 = TextureEntries[texIndex3];
              _gl.ActiveTexture(TextureUnit.Texture2);
              _gl.BindTexture(GLEnum.Texture2D, texEntry3.Texure);
              CheckGl();
            }
            else {
              //Console.WriteLine($"Texture3 Invalid: {texIndex3 + 1}");
              _gl.ActiveTexture(TextureUnit.Texture2);
              _gl.BindTexture(GLEnum.Texture2D, 0);
            }

            // scissor state
            if (state.EnableScissor) {
              _gl.Enable(EnableCap.ScissorTest);
              CheckGl();

              ref readonly var r = ref state.ScissorRect;
              _gl.Scissor(r.Left, r.Top,
                (uint) (r.Right - r.Left),
                (uint) (r.Bottom - r.Top));
            }
            else
              _gl.Disable(EnableCap.ScissorTest);

            CheckGl();

            // blend state
            if (state.EnableBlend)
              _gl.Enable(EnableCap.Blend);
            else
              _gl.Disable(EnableCap.Blend);

            CheckGl();

            // draw elements
            _gl.DrawElements(PrimitiveType.Triangles,
              command.IndicesCount, DrawElementsType.UnsignedInt,
              (void*) (command.IndicesOffset * sizeof(uint)));
            CheckGl();

            // reset vertex array
            _gl.BindVertexArray(0);

            break;
          }
          default: throw new ArgumentOutOfRangeException(nameof(CommandType));
        }
      }
    }

    {
      var wndSize = _wnd.Size;
      var wndWidth = (uint) wndSize.Width;
      var wndHeight = (uint) wndSize.Height;

      var texId = (int)_view.GetRenderTarget().TextureId - 1;
      if (!TextureEntries.TryGet(texId, out var texEntry))
        return;

      //var rb = rbEntry.FrameBuffer;
      //var texEntry = rbEntry.TextureEntry;
      var tex = texEntry.Texure;

      _gl.Disable(EnableCap.FramebufferSrgb);
      _gl.Disable(EnableCap.ScissorTest);
      _gl.Disable(EnableCap.Blend);
      //Bind the primary framebuffer, quad geometry and shader.
      _gl.Viewport(0, 0, wndWidth, wndHeight);
      //_gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, rb);
      //_gl.ReadBuffer(ReadBufferMode.ColorAttachment0);
      //_gl.BindFramebuffer(FramebufferTarget.DrawFramebuffer, 0);
      //_gl.ClearColor(0, 0, 0, 0);
      //_gl.Clear((uint) ClearBufferMask.ColorBufferBit);
      /*
        _gl.BlitFramebuffer(
          0, 0, (int) texEntry.Width, (int) texEntry.Height,
          0, 0, (int) wndWidth, (int) wndHeight,
          (uint) AttribMask.ColorBufferBit,
          BlitFramebufferFilter.Linear);
        // */
      // /*
      //_gl.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
      _gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
      _gl.BindVertexArray(_qva);
      _gl.UseProgram(_qpg);
      _gl.ActiveTexture(TextureUnit.Texture0);
      _gl.BindTexture(TextureTarget.Texture2D, tex);
      _gl.Uniform1(_gl.GetUniformLocation(_qpg, "iTex"), 0);
      CheckGl();

      //Draw the geometry.
      _gl.DrawElements(PrimitiveType.Triangles,
        (uint) _indicesSize, DrawElementsType.UnsignedInt, null);
      //*/
    }
  }

}