using UnityEngine;

public  interface IPool
{
    void Show();

    void Hide();

    void SetParent(Transform parent);

    bool Active { get; }

    string Name { get; }
}
