using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests {

  public class TestsSceneTests {

    [UnityTest]
    public IEnumerator CheckUltralightBrowserDemo() {
      if (SceneManager.GetActiveScene().name != "TestsScene")
        SceneManager.LoadScene("TestsScene");
      yield return null;

      var browser = Object.FindObjectOfType<UltralightBrowserDemo>();
      Assert.IsNotNull(browser, "Browser should exist.");

      Assert.AreNotEqual("UltralightSharp Demo", browser.Title, "Browser should not be pre-loaded.");

      browser.Url = "file:///index.html";

      Assert.AreNotEqual("UltralightSharp Demo", browser.Title, "Browser should not be load immediately.");
      yield return null;

      Assert.IsFalse(browser.IsLoading, "Should not be loading.");
      Assert.IsFalse(browser.Failed, "Should not fail to load.");
      Assert.IsTrue(browser.IsLoaded, "Should be loaded.");
      Assert.IsTrue(browser.IsDomReady, "DOM should be ready.");

      Assert.AreEqual("UltralightSharp Demo", browser.Title);
    }

  }

}