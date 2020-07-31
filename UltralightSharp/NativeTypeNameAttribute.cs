using System;
using JetBrains.Annotations;

namespace ImpromptuNinjas.UltralightSharp {

  [PublicAPI]
  [AttributeUsage(AttributeTargets.All)]
  public class NativeTypeNameAttribute : Attribute {

    public string Name;

    public NativeTypeNameAttribute(string name) => Name = name;

  }

}