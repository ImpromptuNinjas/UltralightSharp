using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [NativeTypeName("ULCommandList")]
  [StructLayout(LayoutKind.Sequential)]
  public struct CommandList : IReadOnlyList<Ptr<Command>> {

    [NativeTypeName("unsigned int")]
    public uint Size;

    [NativeTypeName("ULCommand *")]
    public unsafe Command* Commands;

    public IEnumerator<Ptr<Command>> GetEnumerator() {
      for (var i = 0; i < Count; ++i)
        yield return ((IReadOnlyList<Ptr<Command>>) this)[i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
      => GetEnumerator();

    public int Count {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => (int) Size;
    }

    unsafe Ptr<Command> IReadOnlyList<Ptr<Command>>.this[int i] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => this[i];
    }

    public unsafe Command* this[int i] {
      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      get => (Command*) Unsafe.Add<Command>(Commands, i);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe implicit operator Span<Ptr<Command>>(CommandList l)
      => new Span<Ptr<Command>>(l.Commands, l.Count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe implicit operator ReadOnlySpan<Ptr<Command>>(CommandList l)
      => new ReadOnlySpan<Ptr<Command>>(l.Commands, l.Count);

  }

  namespace Safe {

    [PublicAPI]
    public unsafe struct CommandList : IReadOnlyList<Command> {

      private UltralightSharp.CommandList _;

      [NativeTypeName("unsigned int")]
      public uint Size => _.Size;

      [NativeTypeName("ULCommand *")]
      public IEnumerable<Command> Commands {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this;
      }

      public IEnumerator<Command> GetEnumerator() {
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var p in _)
          yield return p.Dereference();
      }

      [MethodImpl(MethodImplOptions.AggressiveInlining)]
      IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

      public int Count {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => _.Count;
      }

      public ref Command this[int i] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ref Unsafe.AsRef<Command>(_[i]);
      }

      Command IReadOnlyList<Command>.this[int i] {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => *_[i];
      }

      public static implicit operator UltralightSharp.CommandList(CommandList l)
        => Unsafe.As<CommandList, UltralightSharp.CommandList>(ref l);

      public static implicit operator CommandList(UltralightSharp.CommandList l)
        => Unsafe.As<UltralightSharp.CommandList, CommandList>(ref l);

    }

  }

}