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
      Assert.IsNotNull(browser);

      Assert.AreNotEqual("UltralightSharp Demo", browser.Title);

      browser.Url = "file:///index.html";

      Assert.AreNotEqual("UltralightSharp Demo", browser.Title);
      yield return null;

      Assert.IsFalse(browser.IsLoading);
      Assert.IsFalse(browser.Failed);
      Assert.IsTrue(browser.IsLoaded);
      Assert.IsTrue(browser.IsDomReady);

      Assert.AreEqual("UltralightSharp Demo", browser.Title);
    }

  }

}