namespace KindaGoodPrivacy.Source.Core
{
    public class Error
    {
        public struct ErrorInfo
        {
            public string CustomMessage { get; set; }
            public string ErrorType { get; set; }
            public string ErrorMessage { get; set; }
            public string? StackTrace { get; set; }

            public override string ToString()
            {
                return !string.IsNullOrEmpty(CustomMessage)
                    ? CustomMessage
                    : ErrorMessage;
            }

            public string ToDebugString()
            {
                return
                    $"[custom message]: {CustomMessage}\n" +
                    $"[error type]: {ErrorType}\n" +
                    $"[error message]: {ErrorMessage}\n" +
                    $"[stack trace]:\n{StackTrace}";
            }
        }
    }
}
