using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : MonoBehaviour
{
	abstract public string GetManagerName();

	protected static T m_instance;
	public static T Instance { get { return m_instance; } }

	public static T CreateInstance()
	{
		if (m_instance == null)
		{
			m_instance = FindObjectOfType<T>();
			if (m_instance == null)
			{
				GameObject obj = new GameObject();
				m_instance = obj.AddComponent<T>();
				obj.name = (m_instance as Manager<T>).GetManagerName();
				(m_instance as Manager<T>).InitializeManager();
			}
		}
		return m_instance;
	}

	abstract protected void InitializeManager();

	public static bool IsSet
	{
		get
		{
			return m_instance != null;
		}
	}

	protected void OnDestroy()
	{
		m_instance = null;
		DestroyManager();
	}

	protected void Log(string _log)
	{
		Debug.Log("[" + typeof(T).ToString() + "] " + _log);
	}

	abstract protected void DestroyManager();
}
