using System;
using UnityEngine.SceneManagement;

public interface IMod
{

    string Name { get; }
    string Author { get; }
    Version Version { get; }

    void Awake();

    void ApplicationQuit();

    void SceneLoaded(Scene loaded);

    void Start();

    void Update();

    void FixedUpdate();

    void LateUpdate();

    void OnDisable();

    void OnEnable();
}

