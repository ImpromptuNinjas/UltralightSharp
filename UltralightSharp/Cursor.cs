using JetBrains.Annotations;

namespace Ultralight {

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

}