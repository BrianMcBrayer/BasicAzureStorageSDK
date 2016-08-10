﻿






// -----------------------------------------------------------------------------
// Autogenerated code. Do not modify.
// -----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Basic.Azure.Storage.Communications.ServiceExceptions
{

/// <summary>
/// Maps a WebException from Azure to the appropriate local exception type
/// </summary>
/// <remarks>
/// Uses the Queue Service Error Codes defined here: http://msdn.microsoft.com/en-us/library/windowsazure/dd179446.aspx
/// </remarks>
public static class QueueServiceAzureExceptions
{

	public static AzureException GetExceptionFor(string requestId, HttpStatusCode statusCode, string errorCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
	{
		switch(errorCode)
		{
		 
			case "MessageTooLarge":
				return new MessageTooLargeAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "InvalidMarker":
				return new InvalidMarkerAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "PopReceiptMismatch":
				return new PopReceiptMismatchAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "QueueNotFound":
				return new QueueNotFoundAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "MessageNotFound":
				return new MessageNotFoundAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "QueueDisabled":
				return new QueueDisabledAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "QueueAlreadyExists":
				return new QueueAlreadyExistsAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "QueueBeingDeleted":
				return new QueueBeingDeletedAzureException(requestId, statusCode, statusDescription, details, baseException);
			 
			case "QueueNotEmpty":
				return new QueueNotEmptyAzureException(requestId, statusCode, statusDescription, details, baseException);
			
		}

		var shortStatusDescription = statusDescription.Split('\n')[0];
		switch(shortStatusDescription)
		{
			 
				case "The message exceeds the maximum allowed size.":
					return new MessageTooLargeAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified marker is invalid.":
					return new InvalidMarkerAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified pop receipt did not match the pop receipt for a dequeued message.":
					return new PopReceiptMismatchAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified queue does not exist.":
					return new QueueNotFoundAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified message does not exist.":
					return new MessageNotFoundAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified queue has been disabled by the administrator.":
					return new QueueDisabledAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified queue already exists.":
					return new QueueAlreadyExistsAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified queue is being deleted.":
					return new QueueBeingDeletedAzureException(requestId, statusCode, statusDescription, details, baseException);
				 
				case "The specified queue is not empty.":
					return new QueueNotEmptyAzureException(requestId, statusCode, statusDescription, details, baseException);
				

			default:
				return CommonServiceAzureExceptions.GetExceptionFor(requestId, statusCode, errorCode, statusDescription, details, baseException);
		}
	}

}


	///
	///<summary>
	///Represents a 'MessageTooLarge' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The message exceeds the maximum allowed size.</remarks>
	public class MessageTooLargeAzureException : AzureException
    {
        public MessageTooLargeAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'InvalidMarker' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified marker is invalid.</remarks>
	public class InvalidMarkerAzureException : AzureException
    {
        public InvalidMarkerAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'PopReceiptMismatch' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified pop receipt did not match the pop receipt for a dequeued message.</remarks>
	public class PopReceiptMismatchAzureException : AzureException
    {
        public PopReceiptMismatchAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'QueueNotFound' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified queue does not exist.</remarks>
	public class QueueNotFoundAzureException : AzureException
    {
        public QueueNotFoundAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'MessageNotFound' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified message does not exist.</remarks>
	public class MessageNotFoundAzureException : AzureException
    {
        public MessageNotFoundAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'QueueDisabled' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified queue has been disabled by the administrator.</remarks>
	public class QueueDisabledAzureException : AzureException
    {
        public QueueDisabledAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'QueueAlreadyExists' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified queue already exists.</remarks>
	public class QueueAlreadyExistsAzureException : AzureException
    {
        public QueueAlreadyExistsAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'QueueBeingDeleted' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified queue is being deleted.</remarks>
	public class QueueBeingDeletedAzureException : AzureException
    {
        public QueueBeingDeletedAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	
	///
	///<summary>
	///Represents a 'QueueNotEmpty' error response from the Queue Service API 
	///</summary>
	///<remarks>Description: The specified queue is not empty.</remarks>
	public class QueueNotEmptyAzureException : AzureException
    {
        public QueueNotEmptyAzureException(string requestId, HttpStatusCode statusCode, string statusDescription, Dictionary<string, string> details, WebException baseException)
            : base(requestId, statusCode, statusDescription, details, baseException) { }
    }

	

}