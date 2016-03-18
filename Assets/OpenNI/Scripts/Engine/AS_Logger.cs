using UnityEngine;
using System.Collections;

public class AS_Logger: UNIEventLogger {

	public override void Awake()
    {
        InitCategoriesList();
        InitSourcesList();
        m_Initialized = true;
    }
	
	public override void Log (string str, UNIEventLogger.Categories category, UNIEventLogger.Sources sources, UNIEventLogger.VerboseLevel logLevel)
	{
		base.Log (str, category, sources, logLevel);
	}
}
