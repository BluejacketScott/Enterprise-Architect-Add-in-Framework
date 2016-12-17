﻿using System.Collections.Generic;
using System.Linq;
using System;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAAddinFramework.Utilities
{
	/// <summary>
	/// Description of EAOutputLogger.
	/// </summary>
	public class EAOutputLogger
	{
		Model model;
		string name;
		static Dictionary<string, EAOutputLogger> outputLogs = new Dictionary<string, EAOutputLogger>();
		static EAOutputLogger getOutputLogger(Model model, string outputName)
		{
			var logKey = model.projectGUID+outputName;
			if (!outputLogs.ContainsKey(logKey))
			{
				outputLogs.Add(logKey,new EAOutputLogger(model, outputName));
			}
			return outputLogs[logKey];
		}
		/// <summary>
		/// private constructor
		/// </summary>
		/// <param name="model">the model this output applies to</param>
		/// <param name="outputName"></param>
		private EAOutputLogger(Model model, string outputName)
		{
			this.model = model;
			this.name = outputName;
			//make sure the log exists and is visible and cleared
			this.model.wrappedModel.CreateOutputTab(this.name);
			this.model.wrappedModel.EnsureOutputVisible(this.name);
			this.model.wrappedModel.ClearOutput(this.name);
		}
		private void logToOutput(string message, int elementID = 0)
		{
			this.model.wrappedModel.EnsureOutputVisible(this.name);
			this.model.wrappedModel.WriteOutput(this.name,message,elementID);
		}
		private void clear()
		{
			this.model.wrappedModel.ClearOutput(this.name);
		}
		/// <summary>
		/// log a message to the EA output window. If requested the message will also be logged to the logfile
		/// </summary>
		/// <param name="model">the model on which to show the output</param>
		/// <param name="outputName">the name of the output window</param>
		/// <param name="message">the message to show</param>
		/// <param name="elementID">the element ID to associate with the message. Can be used by add-ins when they implement EA_OnOutput...</param>
		/// <param name="logType">the type of logging to the logfile</param>
		public static void log(Model model,string outputName, string message, int elementID = 0,LogTypeEnum logType = LogTypeEnum.none)
		{
			var logger = getOutputLogger(model, outputName);
			logger.logToOutput(message,elementID);
			//log to logfile if needed
			switch (logType) 
			{
				case LogTypeEnum.log:
					Logger.log(message);
					break;
				case LogTypeEnum.warning:
					Logger.logWarning(message);
					break;
				case LogTypeEnum.error:
					Logger.logError(message);
					break;
			}
		}
		public static void clearLog(Model model,string outputName)
		{
			var logger = getOutputLogger(model, outputName);
			logger.clear();
		}                       

	}
}
