using JetBrains.Annotations;

namespace Ultralight {

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

}