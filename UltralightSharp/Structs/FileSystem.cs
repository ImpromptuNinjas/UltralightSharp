using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  public struct FileSystem {

    [NativeTypeName("ULFileSystemFileExistsCallback")]
    public FnPtr<FileSystemFileExistsCallback> FileExists;

    [NativeTypeName("ULFileSystemGetFileSizeCallback")]
    public FnPtr<FileSystemGetFileSizeCallback> GetFileSize;

    [NativeTypeName("ULFileSystemGetFileMimeTypeCallback")]
    public FnPtr<FileSystemGetFileMimeTypeCallback> GetFileMimeType;

    [NativeTypeName("ULFileSystemOpenFileCallback")]
    public FnPtr<FileSystemOpenFileCallback> OpenFile;

    [NativeTypeName("ULFileSystemCloseFileCallback")]
    public FnPtr<FileSystemCloseFileCallback> CloseFile;

    [NativeTypeName("ULFileSystemReadFromFileCallback")]
    public FnPtr<FileSystemReadFromFileCallback> ReadFromFile;

  }

  namespace Safe {

    [PublicAPI]
    [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
    public struct FileSystem {

      private UltralightSharp.FileSystem _;

      public static implicit operator UltralightSharp.FileSystem(in FileSystem x)
        => x._;

      public unsafe FileSystemFileExistsCallback FileExists {
        set {
          UltralightSharp.FileSystemFileExistsCallback cb
            = path => value(path->Read());
          _.FileExists = cb;
        }
      }

      public unsafe FileSystemGetFileSizeCallback GetFileSize {
        set {
          UltralightSharp.FileSystemGetFileSizeCallback cb
            = (handle, result) => {
              var success = value(handle, out var size);
              *result = size;
              return success;
            };
          _.GetFileSize = cb;
        }
      }

      public unsafe FileSystemGetFileMimeTypeCallback GetFileMimeType {
        set {
          UltralightSharp.FileSystemGetFileMimeTypeCallback cb
            = (path, result) => value(path->Read(), result->Read());
          _.GetFileMimeType = cb;
        }
      }

      public unsafe FileSystemOpenFileCallback OpenFile {
        set {
          UltralightSharp.FileSystemOpenFileCallback cb
            = (path, openForWriting) => value(path->Read(), openForWriting);
          _.OpenFile = cb;
        }
      }

      public unsafe FileSystemCloseFileCallback CloseFile {
        set {
          UltralightSharp.FileSystemCloseFileCallback cb
            = handle => value(handle);
          _.CloseFile = cb;
        }
      }

      public unsafe FileSystemReadFromFileCallback ReadFromFile {
        set {
          UltralightSharp.FileSystemReadFromFileCallback cb
            = (handle, data, length) => value(handle, new ReadOnlySpan<byte>(data, (int) length));
          _.ReadFromFile = cb;
        }
      }

    }

  }

}