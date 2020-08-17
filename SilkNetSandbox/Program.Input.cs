using Silk.NET.Input.Common;

partial class Program {

  private static void KeyDown(IKeyboard arg1, Key arg2, int arg3) {
    if (arg2 == Key.Escape)
      _snView.Close();
  }

}