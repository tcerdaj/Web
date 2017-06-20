using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace JehovaJireh.Logging
{
	public interface ILogger: IExceptionHandler
	{
		bool IsDebugEnabled { get; }
		bool IsErrorEnabled { get; }
		bool IsFatalEnabled { get; }
		bool IsInfoEnabled { get; }
		bool IsWarnEnabled { get; }

		void Debug(string message);
		void Debug(string fmt, params object[] vars);
		void Debug(string message, Exception exception);

		void Info(string message);
		void Info(string fmt, params object[] vars);
		void Info(string message, Exception exception);

		void Warn(string message);
		void Warn(string fmt, params object[] vars);
		void Warn(string message, Exception exception);

		void Error(string message);
		void Error(string fmt, params object[] vars);
		void Error(string message, Exception exception);
		void Error(Exception exception);

		void Fatal(string message);
		void Fatal(string fmt, params object[] vars);
		void Fatal(string message, Exception exception);
		void Fatal(Exception exception);

		void TraceApi(string componentName, string method, TimeSpan timespan);
		void TraceApi(string componentName, string method, TimeSpan timespan, string properties);
		void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars);

		#region Application
		void APIApplicationStarting();

		//[Event(101, Level = EventLevel.Informational, Keywords = Keywords.Application, Task = Tasks.Initialize, Opcode = Opcodes.Start, Version = 1)]
		void APIApplicationStarted();

		void WebApplicationStarting();

		void WebApplicationStarted();

		void WorkerStarted(string workerName);
		void WorkerCancelled(string workerName);
		void WorkerFinished(string workerName, TimeSpan? span = null);

		new Exception HandleException(Exception exception, Guid handlingInstanceId);
		#endregion

		#region Caching
		void CacheInitialized();
		void CacheCleared();
		#endregion

		#region Data Access

		#region Initialization

		//[Event(250, Level = EventLevel.Informational, Keywords = Keywords.DataAccess, Task = Tasks.Initialize, Version = 1)]
		void StopRepositoryInitialized();


		//[Event(251, Level = EventLevel.Informational, Keywords = Keywords.DataAccess, Task = Tasks.Initialize, Version = 1)]
		void UserRepositoryInitialized(string applicationName);


		//[Event(252, Level = EventLevel.Informational, Keywords = Keywords.DataAccess, Task = Tasks.Initialize, Version = 1)]
		void ProfileStoreInitialized();


		#endregion

		#region EntityCRUD operations

		void SaveStarted<T>(T entity);
		void SaveFinished<T>(T entity, TimeSpan? span = null);
		void SaveInsertStarted<T>(T entity);
		void SaveInsertFinished<T>(T entity, TimeSpan? span = null);
		void DeleteStarted<T>(T entity);
		void DeleteFinished<T>(T entity, TimeSpan? span = null);
		#endregion

		#endregion

		#region User Interface

		//[Event(330, Level = EventLevel.Informational, Keywords = Keywords.UserInterface, Task = Tasks.SaveExpense, Opcode = Opcodes.Start, Version = 1)]
		void UiSaveExpenseStarted();


		//[Event(331, Level = EventLevel.Informational, Keywords = Keywords.UserInterface, Task = Tasks.SaveExpense, Opcode = Opcodes.Finish, Version = 1)]
		void UiSaveExpenseFinished();


		//[Event(332, Level = EventLevel.Error, Keywords = Keywords.UserInterface, Task = Tasks.SaveExpense, Opcode = Opcodes.Error, Version = 1)]
		void UiSaveExpenseFailed(string exceptionMessage, string exceptionType);


		#endregion

		#region General

		//[Event(1000, Level = EventLevel.Error, Keywords = Keywords.General, Task = Tasks.Tracing, Opcode = Opcodes.Error, Version = 1)]
		void ExceptionHandlerLoggedException(string message, Guid id);


		//[Event(1001, Level = EventLevel.Informational, Keywords = Keywords.General, Task = Tasks.Tracing, Version = 1, Message = "Method {0}.{1} executed")]
		void TracingBehaviorVirtualMethodIntercepted(string declaringType, string methodBaseName, string arguments);
		#endregion
	}
}
