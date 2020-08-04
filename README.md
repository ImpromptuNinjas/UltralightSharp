![UltralightSharp](https://raw.githubusercontent.com/ImpromptuNinjas/UltralightSharp/master/icon.png)

[![NuGet](https://img.shields.io/nuget/v/ImpromptuNinjas.UltralightSharp.svg)](https://www.nuget.org/packages/ImpromptuNinjas.UltralightSharp/) [![Build & Test](https://github.com/ImpromptuNinjas/UltralightSharp/workflows/Build%20&%20Test/badge.svg)](https://github.com/ImpromptuNinjas/UltralightSharp/actions?query=workflow%3A%22Build+%26+Test%22) [![Sponsor](https://img.shields.io/static/v1?label=Sponsor&message=%E2%9D%A4&logo=GitHub&link=https://github.com/sponsors/Tyler-IN)](https://github.com/sponsors/Tyler-IN)
 
# UltralightSharp

A multi-platform .NET binding of the **Ultralight** project.

## Supported platforms:
* Windows
  - x64
* Linux
  - GNU flavors (Debian, Ubuntu, ...)
  - AMD64 / Intel x86-64
* Apple OSX
  - 64-bit only

Work under Unity, currently testing under 2018.4 LTS.
Full support for all LTS versions and the latest version of Unity is planned.

### Known Issues:
* Currently the native dependencies are shipped with this NuGet package for all platforms.
  _Separate NuGet runtime packages should be created to provide each specific platform dependency._
* Demo but no tests, no WebCore bindings yet.


Acknowlegedments
----------------

* [Ultralight](https://utralig.ht)
* [Ultralight on GitHub](https://github.com/ultralight-ux/Ultralight)

This project includes binary distributions of Ultralight SDK libraries.

Examples
--------

## .NET Core Headless / Console Demo

See the [DemoProgram](https://github.com/ImpromptuNinjas/UltralightSharp/tree/master/UltralightSharp.Demo) and Safe [DemoProgram](https://github.com/ImpromptuNinjas/UltralightSharp/tree/master/UltralightSharp.SafeDemo) for headless functional examples.

![Demo Screenshot](https://cdn.discordapp.com/attachments/738836157923852368/739599229709844520/unknown.png)

The demo can produce PNGs or a scaled down low resolution 24-bit ANSI image to the console.
(ANSI image on Windows console seen above.)

## Unity Demo (2018.4 LTS)

![Unity Demo](https://cdn.discordapp.com/attachments/738836157923852368/739376040970944572/unknown.png)
![Unity Tests](https://cdn.discordapp.com/attachments/738836157923852368/739376118435414096/unknown.png)

A Unity demo and test has been added to this repo.

The CI will test against LTS branches of Unity.

Currently only 2018.4 LTS is tested.

It is forward compatible up to at least 2020.1, but may require some tweaking of dependency versions.
