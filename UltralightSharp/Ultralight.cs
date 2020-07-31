using System;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace Ultralight {

  #region Convenience and documentation mechanisms

  [PublicAPI]
  public struct FnPtr<TDelegate> where TDelegate : Delegate {

    public IntPtr Pointer;

    public FnPtr(TDelegate d)
      => Pointer = Marshal.GetFunctionPointerForDelegate(d);

    public static implicit operator IntPtr(FnPtr<TDelegate> fp)
      => fp.Pointer;

    public static implicit operator FnPtr<TDelegate>(TDelegate d)
      => new FnPtr<TDelegate>(d);

  }

  [PublicAPI]
  [AttributeUsage(AttributeTargets.All)]
  public class NativeTypeNameAttribute : Attribute {

    public string Name;

    public NativeTypeNameAttribute(string name) => Name = name;

  }

  #endregion

  #region Opaque by-ref only structures

  [PublicAPI]
  public readonly ref struct JsContext {

  }

  [PublicAPI]
  public readonly ref struct Config {

  }

  [PublicAPI]
  public readonly ref struct Renderer {

  }

  [PublicAPI]
  public readonly ref struct Session {

  }

  [PublicAPI]
  public readonly ref struct View {

  }

  [PublicAPI]
  public readonly ref struct Bitmap {

  }

  [PublicAPI]
  public readonly ref struct String {

  }

  [PublicAPI]
  public readonly ref struct Buffer {

  }

  [PublicAPI]
  public readonly ref struct KeyEvent {

  }

  [PublicAPI]
  public readonly ref struct MouseEvent {

  }

  [PublicAPI]
  public readonly ref struct ScrollEvent {

  }

  [PublicAPI]
  public readonly ref struct Surface {

  }

  #endregion

  #region Enumerations

  [PublicAPI]
  public enum MessageSource {

    Xml = 0,

    Js,

    Network,

    ConsoleApi,

    Storage,

    AppCache,

    Rendering,

    Css,

    Security,

    ContentBlocker,

    Other,

  }

  [PublicAPI]
  public enum MessageLevel {

    Log = 1,

    Warning = 2,

    Error = 3,

    Debug = 4,

    Info = 5,

  }

  [PublicAPI]
  public enum Cursor {

    Pointer = 0,

    Cross,

    Hand,

    // ReSharper disable once InconsistentNaming
    IBeam,

    Wait,

    Help,

    EastResize,

    NorthResize,

    NorthEastResize,

    NorthWestResize,

    SouthResize,

    SouthEastResize,

    SouthWestResize,

    WestResize,

    NorthSouthResize,

    EastWestResize,

    NorthEastSouthWestResize,

    NorthWestSouthEastResize,

    ColumnResize,

    RowResize,

    MiddlePanning,

    EastPanning,

    NorthPanning,

    NorthEastPanning,

    NorthWestPanning,

    SouthPanning,

    SouthEastPanning,

    SouthWestPanning,

    WestPanning,

    Move,

    VerticalText,

    Cell,

    ContextMenu,

    Alias,

    Progress,

    NoDrop,

    Copy,

    None,

    NotAllowed,

    ZoomIn,

    ZoomOut,

    Grab,

    Grabbing,

    Custom,

  }

  [PublicAPI]
  public enum BitmapFormat {

    A8UNorm,

    Bgra8UNormSrgb,

  }

  [PublicAPI]
  public enum KeyEventType {

    KeyDown,

    KeyUp,

    RawKeyDown,

    Char,

  }

  [PublicAPI]
  public enum MouseEventType {

    MouseMoved,

    MouseDown,

    MouseUp,

  }

  [PublicAPI]
  public enum MouseButton {

    None = 0,

    Left,

    Middle,

    Right,

  }

  [PublicAPI]
  public enum ScrollEventType {

    ScrollByPixel,

    ScrollByPage,

  }

  [PublicAPI]
  public enum FaceWinding {

    Clockwise,

    CounterClockwise,

  }

  [PublicAPI]
  public enum FontHinting {

    Smooth,

    Normal,

    Monochrome,

  }

  [PublicAPI]
  public enum LogLevel {

    Error = 0,

    Warning,

    Info,

  }

  #endregion

  #region Structures

  [PublicAPI]
  public struct Rect {

    public float Left;

    public float Top;

    public float Right;

    public float Bottom;

  }

  [PublicAPI]
  public struct IntRect {

    public int Left;

    public int Top;

    public int Right;

    public int Bottom;

  }

  [PublicAPI]
  public struct RenderTarget {

    public bool IsEmpty;

    [NativeTypeName("unsigned int")]
    public uint Width;

    [NativeTypeName("unsigned int")]
    public uint Height;

    [NativeTypeName("unsigned int")]
    public uint TextureId;

    [NativeTypeName("unsigned int")]
    public uint TextureWidth;

    [NativeTypeName("unsigned int")]
    public uint TextureHeight;

    public BitmapFormat TextureFormat;

    public Rect UvCoords;

    [NativeTypeName("unsigned int")]
    public uint RenderBufferId;

  }

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

  [PublicAPI]
  public struct Logger {

    [NativeTypeName("ULLoggerLogMessageCallback")]
    public FnPtr<LoggerLogMessageCallback> LogMessage;

  }

  #endregion

  #region Callbacks

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeTitleCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* title);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeUrlCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* url);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeTooltipCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* tooltip);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void ChangeCursorCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, Cursor cursor);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void AddConsoleMessageCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, MessageSource source, MessageLevel level, [NativeTypeName("ULString")] String* message,
    [NativeTypeName("unsigned int")] uint lineNumber, [NativeTypeName("unsigned int")] uint columnNumber, [NativeTypeName("ULString")] String* sourceId);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULView")]
  public unsafe delegate View* CreateChildViewCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("ULString")] String* openerUrl,
    [NativeTypeName("ULString")] String* targetUrl, bool isPopup, IntRect popupRect);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void BeginLoadingCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void FinishLoadingCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void FailLoadingCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url, [NativeTypeName("ULString")] String* description, [NativeTypeName("ULString")] String* errorDomain, int errorCode);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void WindowObjectReadyCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void DomReadyCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller, [NativeTypeName("unsigned long long")] ulong frameId, bool isMainFrame,
    [NativeTypeName("ULString")] String* url);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void UpdateHistoryCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("ULView")] View* caller);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("void *")]
  public unsafe delegate void* SurfaceDefinitionCreateCallback([NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void SurfaceDefinitionDestroyCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public unsafe delegate uint SurfaceDefinitionGetWidthCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public unsafe delegate uint SurfaceDefinitionGetHeightCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("unsigned int")]
  public unsafe delegate uint SurfaceDefinitionGetRowBytesCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("size_t")]
  public unsafe delegate UIntPtr SurfaceDefinitionGetSizeCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("void *")]
  public unsafe delegate void* SurfaceDefinitionLockPixelsCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void SurfaceDefinitionUnlockPixelsCallback([NativeTypeName("void *")] void* userData);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void SurfaceDefinitionResizeCallback([NativeTypeName("void *")] void* userData, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemFileExistsCallback([NativeTypeName("ULString")] String* path);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemGetFileSizeCallback([NativeTypeName("ULFileHandle")] UIntPtr handle, [NativeTypeName("long long *")] long* result);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate bool FileSystemGetFileMimeTypeCallback([NativeTypeName("ULString")] String* path, [NativeTypeName("ULString")] String* result);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("ULFileHandle")]
  public unsafe delegate UIntPtr FileSystemOpenFileCallback([NativeTypeName("ULString")] String* path, bool openForWriting);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public delegate void FileSystemCloseFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  [return: NativeTypeName("long long")]
  public unsafe delegate long FileSystemReadFromFileCallback([NativeTypeName("ULFileHandle")] UIntPtr handle, [NativeTypeName("char *")] sbyte* data, [NativeTypeName("long long")] long length);

  [PublicAPI]
  [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
  public unsafe delegate void LoggerLogMessageCallback(LogLevel logLevel, [NativeTypeName("ULString")] String* message);

  #endregion

  [PublicAPI]
  public static unsafe class Ultralight {

    [NativeTypeName("const ULFileHandle")]
    public static readonly UIntPtr InvalidFileHandle = (UIntPtr) (-1);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulVersionString", ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* VersionString();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulVersionMajor", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint VersionMajor();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulVersionMinor", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint VersionMinor();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulVersionPatch", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint VersionPatch();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateConfig", ExactSpelling = true)]
    [return: NativeTypeName("ULConfig")]
    public static extern Config* CreateConfig();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyConfig", ExactSpelling = true)]
    public static extern void DestroyConfig([NativeTypeName("ULConfig")] Config* config);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetResourcePath", ExactSpelling = true)]
    public static extern void ConfigSetResourcePath([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* resourcePath);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetCachePath", ExactSpelling = true)]
    public static extern void ConfigSetCachePath([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* cachePath);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetUseGPURenderer", ExactSpelling = true)]
    public static extern void ConfigSetUseGPURenderer([NativeTypeName("ULConfig")] Config* config, bool useGpu);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetDeviceScale", ExactSpelling = true)]
    public static extern void ConfigSetDeviceScale([NativeTypeName("ULConfig")] Config* config, double value);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFaceWinding", ExactSpelling = true)]
    public static extern void ConfigSetFaceWinding([NativeTypeName("ULConfig")] Config* config, FaceWinding winding);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetEnableImages", ExactSpelling = true)]
    public static extern void ConfigSetEnableImages([NativeTypeName("ULConfig")] Config* config, bool enabled);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetEnableJavaScript", ExactSpelling = true)]
    public static extern void ConfigSetEnableJavaScript([NativeTypeName("ULConfig")] Config* config, bool enabled);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontHinting", ExactSpelling = true)]
    public static extern void ConfigSetFontHinting([NativeTypeName("ULConfig")] Config* config, FontHinting fontHinting);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontGamma", ExactSpelling = true)]
    public static extern void ConfigSetFontGamma([NativeTypeName("ULConfig")] Config* config, double fontGamma);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontFamilyStandard", ExactSpelling = true)]
    public static extern void ConfigSetFontFamilyStandard([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* fontName);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontFamilyFixed", ExactSpelling = true)]
    public static extern void ConfigSetFontFamilyFixed([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* fontName);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontFamilySerif", ExactSpelling = true)]
    public static extern void ConfigSetFontFamilySerif([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* fontName);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetFontFamilySansSerif", ExactSpelling = true)]
    public static extern void ConfigSetFontFamilySansSerif([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* fontName);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetUserAgent", ExactSpelling = true)]
    public static extern void ConfigSetUserAgent([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* agentString);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetUserStylesheet", ExactSpelling = true)]
    public static extern void ConfigSetUserStylesheet([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("ULString")] String* cssString);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetForceRepaint", ExactSpelling = true)]
    public static extern void ConfigSetForceRepaint([NativeTypeName("ULConfig")] Config* config, bool enabled);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetAnimationTimerDelay", ExactSpelling = true)]
    public static extern void ConfigSetAnimationTimerDelay([NativeTypeName("ULConfig")] Config* config, double delay);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetScrollTimerDelay", ExactSpelling = true)]
    public static extern void ConfigSetScrollTimerDelay([NativeTypeName("ULConfig")] Config* config, double delay);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetRecycleDelay", ExactSpelling = true)]
    public static extern void ConfigSetRecycleDelay([NativeTypeName("ULConfig")] Config* config, double delay);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetMemoryCacheSize", ExactSpelling = true)]
    public static extern void ConfigSetMemoryCacheSize([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("unsigned int")] uint size);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetPageCacheSize", ExactSpelling = true)]
    public static extern void ConfigSetPageCacheSize([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("unsigned int")] uint size);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetOverrideRAMSize", ExactSpelling = true)]
    public static extern void ConfigSetOverrideRAMSize([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("unsigned int")] uint size);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetMinLargeHeapSize", ExactSpelling = true)]
    public static extern void ConfigSetMinLargeHeapSize([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("unsigned int")] uint size);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulConfigSetMinSmallHeapSize", ExactSpelling = true)]
    public static extern void ConfigSetMinSmallHeapSize([NativeTypeName("ULConfig")] Config* config, [NativeTypeName("unsigned int")] uint size);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateRenderer", ExactSpelling = true)]
    [return: NativeTypeName("ULRenderer")]
    public static extern Renderer* CreateRenderer([NativeTypeName("ULConfig")] Config* config);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyRenderer", ExactSpelling = true)]
    public static extern void DestroyRenderer([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulUpdate", ExactSpelling = true)]
    public static extern void Update([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulRender", ExactSpelling = true)]
    public static extern void Render([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulPurgeMemory", ExactSpelling = true)]
    public static extern void PurgeMemory([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulLogMemoryUsage", ExactSpelling = true)]
    public static extern void LogMemoryUsage([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateSession", ExactSpelling = true)]
    [return: NativeTypeName("ULSession")]
    public static extern Session* CreateSession([NativeTypeName("ULRenderer")] Renderer* renderer, bool isPersistent, [NativeTypeName("ULString")] String* name);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroySession", ExactSpelling = true)]
    public static extern void DestroySession([NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDefaultSession", ExactSpelling = true)]
    [return: NativeTypeName("ULSession")]
    public static extern Session* DefaultSession([NativeTypeName("ULRenderer")] Renderer* renderer);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSessionIsPersistent", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool SessionIsPersistent([NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSessionGetName", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* SessionGetName([NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSessionGetId", ExactSpelling = true)]
    [return: NativeTypeName("unsigned long long")]
    public static extern ulong SessionGetId([NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSessionGetDiskPath", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* SessionGetDiskPath([NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateView", ExactSpelling = true)]
    [return: NativeTypeName("ULView")]
    public static extern View* CreateView([NativeTypeName("ULRenderer")] Renderer* renderer, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height, bool transparent,
      [NativeTypeName("ULSession")] Session* session);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyView", ExactSpelling = true)]
    public static extern void DestroyView([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetURL", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* ViewGetURL([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetTitle", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* ViewGetTitle([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetWidth", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint ViewGetWidth([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetHeight", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint ViewGetHeight([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewIsLoading", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewIsLoading([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetRenderTarget", ExactSpelling = true)]
    public static extern RenderTarget ViewGetRenderTarget([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetSurface", ExactSpelling = true)]
    [return: NativeTypeName("ULSurface")]
    public static extern Surface* ViewGetSurface([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewLoadHTML", ExactSpelling = true)]
    public static extern void ViewLoadHTML([NativeTypeName("ULView")] View* view, [NativeTypeName("ULString")] String* htmlString);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewLoadURL", ExactSpelling = true)]
    public static extern void ViewLoadURL([NativeTypeName("ULView")] View* view, [NativeTypeName("ULString")] String* urlString);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewResize", ExactSpelling = true)]
    public static extern void ViewResize([NativeTypeName("ULView")] View* view, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewLockJSContext", ExactSpelling = true)]
    [return: NativeTypeName("JSContextRef")]
    public static extern JsContext* ViewLockJSContext([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewUnlockJSContext", ExactSpelling = true)]
    public static extern void ViewUnlockJSContext([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewEvaluateScript", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* ViewEvaluateScript([NativeTypeName("ULView")] View* view, [NativeTypeName("ULString")] String* jsString, [NativeTypeName("ULString *")] String** exception);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewCanGoBack", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewCanGoBack([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewCanGoForward", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewCanGoForward([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGoBack", ExactSpelling = true)]
    public static extern void ViewGoBack([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGoForward", ExactSpelling = true)]
    public static extern void ViewGoForward([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGoToHistoryOffset", ExactSpelling = true)]
    public static extern void ViewGoToHistoryOffset([NativeTypeName("ULView")] View* view, int offset);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewReload", ExactSpelling = true)]
    public static extern void ViewReload([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewStop", ExactSpelling = true)]
    public static extern void ViewStop([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewFocus", ExactSpelling = true)]
    public static extern void ViewFocus([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewUnfocus", ExactSpelling = true)]
    public static extern void ViewUnfocus([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewHasFocus", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewHasFocus([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewHasInputFocus", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewHasInputFocus([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewFireKeyEvent", ExactSpelling = true)]
    public static extern void ViewFireKeyEvent([NativeTypeName("ULView")] View* view, [NativeTypeName("ULKeyEvent")] KeyEvent* keyEvent);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewFireMouseEvent", ExactSpelling = true)]
    public static extern void ViewFireMouseEvent([NativeTypeName("ULView")] View* view, [NativeTypeName("ULMouseEvent")] MouseEvent* mouseEvent);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewFireScrollEvent", ExactSpelling = true)]
    public static extern void ViewFireScrollEvent([NativeTypeName("ULView")] View* view, [NativeTypeName("ULScrollEvent")] ScrollEvent* scrollEvent);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetChangeTitleCallback", ExactSpelling = true)]
    public static extern void ViewSetChangeTitleCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULChangeTitleCallback")]
      FnPtr<ChangeTitleCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetChangeURLCallback", ExactSpelling = true)]
    public static extern void ViewSetChangeURLCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULChangeURLCallback")]
      FnPtr<ChangeUrlCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetChangeTooltipCallback", ExactSpelling = true)]
    public static extern void ViewSetChangeTooltipCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULChangeTooltipCallback")]
      FnPtr<ChangeTooltipCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetChangeCursorCallback", ExactSpelling = true)]
    public static extern void ViewSetChangeCursorCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULChangeCursorCallback")]
      FnPtr<ChangeCursorCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetAddConsoleMessageCallback", ExactSpelling = true)]
    public static extern void ViewSetAddConsoleMessageCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULAddConsoleMessageCallback")]
      FnPtr<AddConsoleMessageCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetCreateChildViewCallback", ExactSpelling = true)]
    public static extern void ViewSetCreateChildViewCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULCreateChildViewCallback")]
      FnPtr<CreateChildViewCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetBeginLoadingCallback", ExactSpelling = true)]
    public static extern void ViewSetBeginLoadingCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULBeginLoadingCallback")]
      FnPtr<BeginLoadingCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetFinishLoadingCallback", ExactSpelling = true)]
    public static extern void ViewSetFinishLoadingCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULFinishLoadingCallback")]
      FnPtr<FinishLoadingCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetFailLoadingCallback", ExactSpelling = true)]
    public static extern void ViewSetFailLoadingCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULFailLoadingCallback")]
      FnPtr<FailLoadingCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetWindowObjectReadyCallback", ExactSpelling = true)]
    public static extern void ViewSetWindowObjectReadyCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULWindowObjectReadyCallback")]
      FnPtr<WindowObjectReadyCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetDOMReadyCallback", ExactSpelling = true)]
    public static extern void ViewSetDOMReadyCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULDOMReadyCallback")] FnPtr<DomReadyCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetUpdateHistoryCallback", ExactSpelling = true)]
    public static extern void ViewSetUpdateHistoryCallback([NativeTypeName("ULView")] View* view, [NativeTypeName("ULUpdateHistoryCallback")]
      FnPtr<UpdateHistoryCallback> callback, [NativeTypeName("void *")] void* userData);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewSetNeedsPaint", ExactSpelling = true)]
    public static extern void ViewSetNeedsPaint([NativeTypeName("ULView")] View* view, bool needsPaint);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewGetNeedsPaint", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool ViewGetNeedsPaint([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulViewCreateInspectorView", ExactSpelling = true)]
    [return: NativeTypeName("ULView")]
    public static extern View* ViewCreateInspectorView([NativeTypeName("ULView")] View* view);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateString", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* CreateString([NativeTypeName("const char *")] sbyte* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateStringUTF8", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* CreateStringUTF8([NativeTypeName("const char *")] sbyte* str, [NativeTypeName("size_t")] UIntPtr len);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateStringUTF16", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* CreateStringUTF16([NativeTypeName("ULChar16 *")] ushort* str, [NativeTypeName("size_t")] UIntPtr len);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateStringFromCopy", ExactSpelling = true)]
    [return: NativeTypeName("ULString")]
    public static extern String* CreateStringFromCopy([NativeTypeName("ULString")] String* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyString", ExactSpelling = true)]
    public static extern void DestroyString([NativeTypeName("ULString")] String* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulStringGetData", ExactSpelling = true)]
    [return: NativeTypeName("ULChar16 *")]
    public static extern ushort* StringGetData([NativeTypeName("ULString")] String* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulStringGetLength", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr StringGetLength([NativeTypeName("ULString")] String* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulStringIsEmpty", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool StringIsEmpty([NativeTypeName("ULString")] String* str);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulStringAssignString", ExactSpelling = true)]
    public static extern void StringAssignString([NativeTypeName("ULString")] String* str, [NativeTypeName("ULString")] String* newStr);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulStringAssignCString", ExactSpelling = true)]
    public static extern void StringAssignCString([NativeTypeName("ULString")] String* str, [NativeTypeName("const char *")] sbyte* cStr);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateEmptyBitmap", ExactSpelling = true)]
    [return: NativeTypeName("ULBitmap")]
    public static extern Bitmap* CreateEmptyBitmap();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateBitmap", ExactSpelling = true)]
    [return: NativeTypeName("ULBitmap")]
    public static extern Bitmap* CreateBitmap([NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height, BitmapFormat format);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateBitmapFromPixels", ExactSpelling = true)]
    [return: NativeTypeName("ULBitmap")]
    public static extern Bitmap* CreateBitmapFromPixels([NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height, BitmapFormat format, [NativeTypeName("unsigned int")] uint rowBytes,
      [NativeTypeName("const void *")] void* pixels, [NativeTypeName("size_t")] UIntPtr size, bool shouldCopy);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateBitmapFromCopy", ExactSpelling = true)]
    [return: NativeTypeName("ULBitmap")]
    public static extern Bitmap* CreateBitmapFromCopy([NativeTypeName("ULBitmap")] Bitmap* existingBitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyBitmap", ExactSpelling = true)]
    public static extern void DestroyBitmap([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetWidth", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint BitmapGetWidth([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetHeight", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint BitmapGetHeight([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetFormat", ExactSpelling = true)]
    public static extern BitmapFormat BitmapGetFormat([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetBpp", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint BitmapGetBpp([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetRowBytes", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint BitmapGetRowBytes([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapGetSize", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr BitmapGetSize([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapOwnsPixels", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool BitmapOwnsPixels([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapLockPixels", ExactSpelling = true)]
    [return: NativeTypeName("void *")]
    public static extern void* BitmapLockPixels([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapUnlockPixels", ExactSpelling = true)]
    public static extern void BitmapUnlockPixels([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapRawPixels", ExactSpelling = true)]
    [return: NativeTypeName("void *")]
    public static extern void* BitmapRawPixels([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapIsEmpty", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool BitmapIsEmpty([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapErase", ExactSpelling = true)]
    public static extern void BitmapErase([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapWritePNG", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool BitmapWritePNG([NativeTypeName("ULBitmap")] Bitmap* bitmap, [NativeTypeName("const char *")] sbyte* path);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapSwapRedBlueChannels", ExactSpelling = true)]
    public static extern void BitmapSwapRedBlueChannels([NativeTypeName("ULBitmap")] Bitmap* bitmap);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateKeyEvent", ExactSpelling = true)]
    [return: NativeTypeName("ULKeyEvent")]
    public static extern KeyEvent* CreateKeyEvent(KeyEventType type, [NativeTypeName("unsigned int")] uint modifiers, int virtualKeyCode, int nativeKeyCode, [NativeTypeName("ULString")] String* text,
      [NativeTypeName("ULString")] String* unmodifiedText, bool isKeypad, bool isAutoRepeat, bool isSystemKey);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateKeyEventWindows", ExactSpelling = true)]
    [return: NativeTypeName("ULKeyEvent")]
    public static extern KeyEvent* CreateKeyEventWindows(KeyEventType type, [NativeTypeName("uintptr_t")] UIntPtr wParam, [NativeTypeName("intptr_t")] IntPtr lParam, bool isSystemKey);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyKeyEvent", ExactSpelling = true)]
    public static extern void DestroyKeyEvent([NativeTypeName("ULKeyEvent")] KeyEvent* evt);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateMouseEvent", ExactSpelling = true)]
    [return: NativeTypeName("ULMouseEvent")]
    public static extern MouseEvent* CreateMouseEvent(MouseEventType type, int x, int y, MouseButton button);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyMouseEvent", ExactSpelling = true)]
    public static extern void DestroyMouseEvent([NativeTypeName("ULMouseEvent")] MouseEvent* evt);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulCreateScrollEvent", ExactSpelling = true)]
    [return: NativeTypeName("ULScrollEvent")]
    public static extern ScrollEvent* CreateScrollEvent(ScrollEventType type, int deltaX, int deltaY);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulDestroyScrollEvent", ExactSpelling = true)]
    public static extern void DestroyScrollEvent([NativeTypeName("ULScrollEvent")] ScrollEvent* evt);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulRectIsEmpty", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool RectIsEmpty(Rect rect);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulRectMakeEmpty", ExactSpelling = true)]
    public static extern Rect RectMakeEmpty();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulIntRectIsEmpty", ExactSpelling = true)]
    [return: NativeTypeName("bool")]
    public static extern bool IntRectIsEmpty(IntRect rect);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulIntRectMakeEmpty", ExactSpelling = true)]
    public static extern IntRect IntRectMakeEmpty();

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetWidth", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint SurfaceGetWidth([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetHeight", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint SurfaceGetHeight([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetRowBytes", ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint SurfaceGetRowBytes([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetSize", ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern UIntPtr SurfaceGetSize([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceLockPixels", ExactSpelling = true)]
    [return: NativeTypeName("void *")]
    public static extern void* SurfaceLockPixels([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceUnlockPixels", ExactSpelling = true)]
    public static extern void SurfaceUnlockPixels([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceResize", ExactSpelling = true)]
    public static extern void SurfaceResize([NativeTypeName("ULSurface")] Surface* surface, [NativeTypeName("unsigned int")] uint width, [NativeTypeName("unsigned int")] uint height);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceSetDirtyBounds", ExactSpelling = true)]
    public static extern void SurfaceSetDirtyBounds([NativeTypeName("ULSurface")] Surface* surface, IntRect bounds);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetDirtyBounds", ExactSpelling = true)]
    public static extern IntRect SurfaceGetDirtyBounds([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceClearDirtyBounds", ExactSpelling = true)]
    public static extern void SurfaceClearDirtyBounds([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulSurfaceGetUserData", ExactSpelling = true)]
    [return: NativeTypeName("void *")]
    public static extern void* SurfaceGetUserData([NativeTypeName("ULSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulBitmapSurfaceGetBitmap", ExactSpelling = true)]
    [return: NativeTypeName("ULBitmap")]
    public static extern Bitmap* BitmapSurfaceGetBitmap([NativeTypeName("ULBitmapSurface")] Surface* surface);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulPlatformSetLogger", ExactSpelling = true)]
    public static extern void PlatformSetLogger(Logger logger);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulPlatformSetFileSystem", ExactSpelling = true)]
    public static extern void PlatformSetFileSystem(FileSystem fileSystem);

    [DllImport("Ultralight", CallingConvention = CallingConvention.Cdecl, EntryPoint = "ulPlatformSetSurfaceDefinition", ExactSpelling = true)]
    public static extern void PlatformSetSurfaceDefinition(SurfaceDefinition surfaceDefinition);

  }

}