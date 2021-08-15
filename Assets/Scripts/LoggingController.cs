using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLogHandler : ILogHandler
{
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        Debug.unityLogger.logHandler.LogException(exception, context);
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
    }
}
