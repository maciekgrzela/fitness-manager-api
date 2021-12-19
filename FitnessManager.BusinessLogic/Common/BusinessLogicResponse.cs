namespace FitnessManager.BusinessLogic.Common
{
    public class BusinessLogicResponse<T>
    {
        public bool Succeed { get; private init; }
        public BusinessLogicResponseResult Result { get; private init; }
        public string ErrorMessage { get; private init; }
        public T Value { get; private init; }

        public static BusinessLogicResponse<T> Success(BusinessLogicResponseResult result, T value) =>
            new() {Succeed = true, Result = result, Value = value};

        public static BusinessLogicResponse<T> Success(BusinessLogicResponseResult result) => 
            new() {Succeed = true, Result = result};

        public static BusinessLogicResponse<T> Failure(BusinessLogicResponseResult result, string message) =>
            new() {Succeed = false, Result = result, ErrorMessage = message};
    }
}